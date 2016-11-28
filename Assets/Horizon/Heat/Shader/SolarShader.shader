Shader "Horizon/Solar" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AtmoColor("Solar Outline", Color) = (0.5, 0.5, 1.0, 1)
		_Transparency("Outline Transparency", Float) = 1.0
		_Falloff("Outline Falloff", Float) = 1.0
		_Size("Size", Float) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half4 _AtmoColor;
		half _Falloff;
		half _Transparency;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);

			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			o.Emission = 0.9 * c.rgb + _AtmoColor.rgb * pow (rim, _Falloff);

			o.Albedo = c.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
