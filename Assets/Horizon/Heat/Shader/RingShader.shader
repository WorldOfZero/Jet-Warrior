Shader "Horizon/Planet Ring" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalTex ("Normal (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_MinRing("Minimum", Range(0,1)) = 0.5
		_MaxRing("Maximum", Range(0,1)) = 1
	}
	SubShader {
		Tags {  "Queue"="AlphaTest"
				"IgnoreProjector"="true"
				"RenderType"="Geometry"
		}
		Cull Off
		LOD 200
		//ZWrite Off
		////Blend One OneMinusDstColor

		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow alpha
		#include "SNoise/noiseSimplex.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		sampler2D _MainTex;
		sampler2D _NormalTex;

		struct Input {
			float2 uv_MainTex;
			float4 _Time;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _MinRing;
		half _MaxRing;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color

			float2 xy = float2(IN.uv_MainTex.x - 0.5, IN.uv_MainTex.y - 0.5);
			xy *= xy;

			half min = _MinRing * _MinRing / 2;
			half max = _MaxRing * _MaxRing /2;

			float distance = sqrt(((xy.x + xy.y) - min))  * (_MinRing / _MaxRing);

			float2 p = float2(distance, 0);
			fixed4 c = tex2D (_MainTex, p) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = (c.r + c.g + c.b) / 3 * (distance > 0 && distance < 1 ? 1 : 0);
			//o.Emission = c.rgb / (distance * 2) * o.Alpha;
			// Metallic and smoothness come from slider variables  
			
			xy.x = IN.uv_MainTex.x - 0.5;
			xy.y = IN.uv_MainTex.y - 0.5;
			float angle = atan2(xy.y * 2, xy.x * 2) + _Time.x * 0.5; //original angle + time offset
			xy = float2(cos(angle) * xy.x - sin(angle) * xy.y, 
					    sin(angle) * xy.x + cos(angle) * xy.y);
			half bump = snoise(xy * 1000) / 10 + 0.9;
			o.Metallic = _Metallic * c.a * bump;
			o.Smoothness = _Glossiness * c.a * bump;
			o.Normal = bump * (tex2D(_NormalTex, xy * 10) / 10 + float3(0,0.9,0));
			clip(distance > 0 && distance < 1 ? 1 : -1);
			o.Occlusion = 1;

		}
		ENDCG
	} 
	FallBack "Standard/Cutout"
}
