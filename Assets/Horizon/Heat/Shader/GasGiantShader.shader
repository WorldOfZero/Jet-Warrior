//This is an implementation of the Gas Giant Shader detailed in this reddit post:
//http://www.reddit.com/r/gamedev/comments/32ews1/procedural_gas_giant_rendering_with_gpu_noise/
//Unity conversion by Sam Wronski (runewake2)

Shader "Horizon/GasGiantShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex ("Albedo (RGB)", 2D) = "white" {}
		_Wavelength ("Wavelength", Range(1, 10000)) = 1
		_Detail ("Detail", Range(1, 16)) = 6
		_StormFrequency ("Storm Frequency", Range(0, 1)) = 0.5
		_Speed ("Speed", Range(-1,1)) = 0.02
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#include "SNoise/noiseSimplex.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NoiseTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NoiseTex;
			float4 _Time;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _Wavelength;
		int _Detail;
		half _StormFrequency;
		half _Speed;

		float noise(float3 position, int octaves, float frequency, float persistence) {
			float total = 0.0; // Total value so far
			float maxAmplitude = 0.0; // Accumulates highest theoretical amplitude
			float amplitude = 1.0;
			for (int i = 0; i < octaves; i++) {

				// Get the noise sample
				total += snoise(position * frequency) * amplitude;

				// Make the wavelength twice as small
				frequency *= 2.0;

				// Add to our maximum possible amplitude
				maxAmplitude += amplitude;

				// Reduce amplitude according to persistence for the next octave
				amplitude *= persistence;
			}

			// Scale the result by the maximum amplitude
			return total / maxAmplitude;
		}

		float ridgedNoise(float3 position, int octaves, float frequency, float persistence) {
			float total = 0.0; // Total value so far
			float maxAmplitude = 0.0; // Accumulates highest theoretical amplitude
			float amplitude = 1.0;
			for (int i = 0; i < octaves; i++) {

				// Get the noise sample
				total += ((1.0 - abs(snoise(position * frequency))) * 2.0 - 1.0) * amplitude;

				// Make the wavelength twice as small
				frequency *= 2.0;

				// Add to our maximum possible amplitude
				maxAmplitude += amplitude;

				// Reduce amplitude according to persistence for the next octave
				amplitude *= persistence;
			}

			// Scale the result by the maximum amplitude
			return total / maxAmplitude;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float3 fNormal = tex2D(_NoiseTex, IN.uv_NoiseTex) / _Wavelength;
			fNormal += float3(_Time.x, 0, _Time.x) * _Speed;
			float n1 = noise(fNormal, _Detail, 10.0, 0.8) * 0.01;
			float n2 = ridgedNoise(fNormal, _Detail, 5.8, 0.75) * 0.015 - 0.01;

			//Storms:
			// Get the three threshold samples
			float s = _StormFrequency;
			float t1 = snoise(fNormal * 2) - s;
			float t2 = snoise((fNormal + 800.0) * 2) - s;
			float t3 = snoise((fNormal + 1600.0) * 2) - s;

			// Intersect them and get rid of negatives
			float threshold = max(t1 * t2 * t3, 0.0);
			float n3 = snoise(fNormal * 0.3) * threshold;

			float n = n1 + n2 + n3;
			fixed2 uv = IN.uv_MainTex.yx;

			uv.x += n;

			fixed4 c = tex2D (_MainTex, uv) * _Color;
			//c += float4(threshold * 3, 0, 0, 0); //This is debugging code that will color all storms red.
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
