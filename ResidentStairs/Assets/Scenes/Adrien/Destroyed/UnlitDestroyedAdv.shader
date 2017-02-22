Shader "Custom/UnlitDestroyedAdv" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Displacement("Displacement", Range(0,10)) = 0.0
	}
		SubShader{
				Pass {
			Tags { "RenderType" = "Opaque" }
			LOD 200
			Cull Off
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma fragment frag vertex:vert 

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			float _Displacement;

			struct Input {
				float2 uv_MainTex;
			};

			void vert(inout appdata_full v) {

				v.vertex.xyz += _Displacement * v.normal;

			}

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			void frag(Input IN) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
			}
			ENDCG
		}
		}
	FallBack "Diffuse"
			
}
