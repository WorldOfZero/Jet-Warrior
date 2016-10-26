Shader "Custom/WorldBeaconShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)

		_Speed("Speed", Range(0,100)) = 1
		_Frequency("Frequency", Range(0, 100)) = 10
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_WorldPos ("Beacon Origin", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		CULL OFF
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Speed;
		half _Frequency;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _WorldPos;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float3 differencePosition = _WorldPos.xyz - IN.worldPos.xyz;
			float radius = length(differencePosition);

			float amplitude = 0.5 + sin(radius * _Frequency - _Time.x * _Speed) * 0.5;
			amplitude = pow(amplitude, 8);
			clip(amplitude - 0.75);

			fixed4 c = _Color * amplitude;
			o.Albedo = c.rgb;
			o.Emission = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
