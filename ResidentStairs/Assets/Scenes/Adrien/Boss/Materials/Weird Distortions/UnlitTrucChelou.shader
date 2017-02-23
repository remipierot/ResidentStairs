Shader "Custom/UnlitTrucChelou02" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}

		_Value1("val1", Range(0,10)) = 0.5
		_Value2("val2", Range(0,10)) = 0.5
		_Value3("val3", Range(0,10)) = 0.5
	}
		
	SubShader{
			
			Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }
			LOD 100
			CGPROGRAM
			#pragma surface surf NoLighting  noambient vertex:vert 

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


			fixed4 _Color;

			fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				fixed4 c;
				c.rgb = s.Albedo;
				c.a = s.Alpha;
				return c;
			}

			void surf(Input IN, inout SurfaceOutputStandard o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			}
			ENDCG
	}
	Fallback off
}
