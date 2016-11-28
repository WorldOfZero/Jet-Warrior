// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Horizon/Advanced Planet" {
	Properties{
		_HotTex("Hot (RGB)", 2D) = "white" {}
		_TempTex("Temperate (RGB)", 2D) = "white" {}
		_ColdTex("Cold (RGB)", 2D) = "white" {}
		_LandHeight("Terrain Height (RGB)", 2D) = "gray" {}
		_OceanHeightTemp("Ocean Height Temperature (RGB)", 2D) = "gray" {}
		_OceanHotTex("Ocean Hot (RGB)", 2D) = "blue" {}
		_OceanTempTex("Ocean Temperate (RGB)", 2D) = "blue" {}
		_OceanColdTex("Ocean Cold (RGB)", 2D) = "blue" {}
		_Temperature("Temperature", Range(0,1)) = 0.5
		_PoleDifference("Pole Difference", Range(-1,1)) = 0.1
		_AtmoColor("Atmosphere Color", Color) = (0.5, 0.5, 1.0, 1)
        _Size("Size", Float) = 0.1
        _Falloff("Falloff", Float) = 5
        _Transparency("Transparency", Float) = 15
		_OceanSpecularBias("Ocean Specularity Bias", Color) = (1.0, 0.9, 0.8, 1)
		_OceanSpecular("Ocean Specularity", Float) = 1
	}
	SubShader{


			Tags{ "RenderType" = "Opaque"}
			LOD 200

			CGPROGRAM
			#pragma target 4.0
			#pragma surface surf PlanetSpecular fullforwardshadows
			#pragma vertex vert
			#include "AutoLight.cginc"

			sampler2D _HotTex;
			sampler2D _TempTex;
			sampler2D _ColdTex;
			sampler2D _LandHeight;
			sampler2D _OceanHeightTemp;
			sampler2D _OceanHotTex;
			sampler2D _OceanTempTex;
			sampler2D _OceanColdTex;
			float _Temperature;
			float _PoleDifference;
			uniform float4 _AtmoColor;
			uniform float _Falloff;
			uniform float _Transparency;
			uniform float4 _OceanSpecularBias;
			uniform float _OceanSpecular;

			half4 LightingPlanetSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
				half3 h = normalize (lightDir + viewDir);

				half diff = max (0, dot (s.Normal, lightDir));

				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, 48.0);
				spec = spec * s.Specular * (_OceanSpecular * 0.9 + 0.1);

				half4 c;
				c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * _OceanSpecularBias) * (atten * 2);
				c.a = s.Alpha;

				return c;
			}

			struct Input {
				float2 uv_HotTex;
				float2 uv_OceanHotTex;
				float3 worldNormal;
				float3 normal;
                float3 worldvertpos;
			};

			void vert(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);
				o.worldNormal = mul(unity_ObjectToWorld, float4(v.normal, 0.0)).xyz;
                o.worldvertpos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.normal = mul((float3x3)unity_ObjectToWorld, v.normal);
			}

			void surf(Input IN, inout SurfaceOutput o) {
				half4 hot = tex2D(_HotTex, IN.uv_HotTex);
				half4 temp = tex2D(_TempTex, IN.uv_HotTex);
				half4 cold = tex2D(_ColdTex, IN.uv_HotTex);
				half4 height = tex2D(_LandHeight, IN.uv_HotTex);
				half4 oceanHot = tex2D(_OceanHotTex, IN.uv_OceanHotTex);
				half4 oceanTemp = tex2D(_OceanTempTex, IN.uv_OceanHotTex);
				half4 oceanCold = tex2D(_OceanColdTex, IN.uv_OceanHotTex);
				half3 rgb;
				half temperature = _Temperature;

				temperature = temperature + (abs(IN.worldNormal.y) * _PoleDifference);

				if (temperature < 0) temperature = 0;
				else if (temperature > 1) temperature = 1;
				
				half3 ocean;
				half targetOceanHeight = tex2D(_OceanHeightTemp, float2(temperature * 0.8 + 0.1, 0)).r;
				half oceanHeight = targetOceanHeight - height.r;

				if (temperature == 0.5)
				{
					rgb = temp.rgb;
					if (oceanHeight > 0)
					{
						ocean = oceanTemp.rgb;
					}
				}
				else if (temperature > 0.5)
				{
					float val = temperature * 2 - 1;
					rgb = val * hot.rgb + (1 - val) * temp.rgb;
					if (oceanHeight > 0)
					{
						ocean = val * oceanHot.rgb + (1 - val) * oceanTemp.rgb;
					}
				}
				else
				{
					float val = temperature * 2;
					rgb = val * temp.rgb + (1 - val) * cold.rgb;
					if (oceanHeight > 0)
					{
						ocean = val * oceanTemp.rgb + (1 - val) * oceanCold.rgb;
					}
				}

				if (oceanHeight >= 0)
				{
					if (oceanHeight > 0.1)
					{
						rgb = ocean;
						o.Specular = 1;
					}
					else
					{
						half val = oceanHeight * 10;
						o.Specular = val;
						rgb = ocean * val + (1-val) * rgb;
					}
				}

				IN.normal = normalize(IN.normal);
                float3 viewdir = normalize(_WorldSpaceCameraPos-IN.worldvertpos);
 
                float4 atmo = _AtmoColor;
                atmo.a = pow(1.0-saturate(dot(viewdir, IN.normal)), _Falloff);
                atmo.a *= _Transparency * _AtmoColor;
 
                rgb = lerp(rgb, atmo.rgb, atmo.a);

				o.Albedo = rgb;
				o.Emission = atmo.rgb * pow(atmo.a, 4) * dot(viewdir, IN.normal);
			}
			ENDCG

		Tags {"LightMode" = "ForwardBase" "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Pass
        {
            Name "FORWARD"
            Cull Front
            Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
 
            CGPROGRAM
                #pragma vertex vert
				#pragma fragment frag

                #pragma fragmentoption ARB_fog_exp2
                #pragma fragmentoption ARB_precision_hint_fastest
 
                #include "UnityCG.cginc"

                uniform float4 _AtmoColor;
                uniform float _Size;
                uniform float _Falloff;
                uniform float _Transparency;
 
                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float3 normal : TEXCOORD0;
                    float3 worldvertpos : TEXCOORD1;
                };
 
                v2f vert(appdata_base v)
                {
                    v2f o;
 
                    v.vertex.xyz += v.normal*_Size;
                    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                    o.normal = mul((float3x3)unity_ObjectToWorld, v.normal);
                    o.worldvertpos = mul(unity_ObjectToWorld, v.vertex);
 
                    return o;
                }
 
                float4 frag(v2f i) : COLOR
                {
                    i.normal = normalize(i.normal);
                    float3 viewdir = normalize(-i.worldvertpos);
 
                    float4 color = _AtmoColor;
					color.a = dot(viewdir, i.normal);
					color.a = saturate(color.a);
					color.a = pow(color.a, _Falloff);
					color.a *= _Transparency;
                    return color;
                }
            ENDCG
			}
		}
		FallBack "Legacy Shaders/VertexLit"
}
