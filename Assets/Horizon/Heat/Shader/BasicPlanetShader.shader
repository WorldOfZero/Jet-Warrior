// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Horizon/Basic Planet" {
	Properties {
		_HotTex ("Hot Base (RGB)", 2D) = "white" {}
		_TempTex ("Temperate Base (RGB)", 2D) = "white" {}
		_ColdTex ("Cold Base (RGB)", 2D) = "white" {}
		_Temperature ("Temperature", Range(0,1)) = 0.5
		_PoleDifference ("Pole Difference", Range(0,1)) = 0.1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _HotTex;
		sampler2D _TempTex;
		sampler2D _ColdTex;
		float _Temperature;
		float _PoleDifference;

		struct Input {
			float2 uv_HotTex;
			float3 worldNormal;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.worldNormal = mul(unity_ObjectToWorld, float4(v.normal, 0.0)).xyz;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 hot = tex2D (_HotTex, IN.uv_HotTex);
			half4 temp = tex2D (_TempTex, IN.uv_HotTex);
			half4 cold = tex2D (_ColdTex, IN.uv_HotTex);
			half3 rgb;
			half temperature = _Temperature;

			temperature = temperature - abs(IN.worldNormal.y) * _PoleDifference;

			if (temperature < 0) temperature = 0;
			else if (temperature > 1) temperature = 1;

			if (temperature == 0.5)
			{
				rgb = temp.rgb;
			}
			else if (temperature > 0.5)
			{
				float val = temperature * 2 - 1;
				rgb = val * hot.rgb + (1-val) * temp.rgb;
			}
			else
			{
				float val = temperature * 2;
				rgb = val * temp.rgb + (1-val) * cold.rgb;
			}
			o.Albedo = rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
