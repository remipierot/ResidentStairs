Shader "Custom/TrucChelou02" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_Value1("val1", Range(0,10)) = 0.5
		_Value2("val2", Range(0,10)) = 0.5
		_Value3("val3", Range(0,10)) = 0.5
	}
		
	SubShader{
			
			Tags{ "RenderType" = "Opaque" }
			LOD 100
			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows vertex:vert 

			sampler2D _MainTex;
			float _Value1;
			float _Value2;
			float _Value3;

			struct Input {
				float2 uv_MainTex;
				float2 uv_Noise;
			};

			void vert(inout appdata_full v) {
				
				v.vertex.xyz += sin((v.vertex.y * _Time * _Value3) * _Value2) * _Value1;

			}

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
	}
	Fallback off
}
