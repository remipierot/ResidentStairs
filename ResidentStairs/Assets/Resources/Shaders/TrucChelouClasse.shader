// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TrucChelou01" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Displacement("Displacement", Range(0, 0.3)) = 0.3

		_Value1("val1", Range(0,10)) = 0.5
		_Value2("val2", Range(0,10)) = 0.5
		_Value3("val3", Range(0,10)) = 0.5
	}
		
	SubShader{
			
		Pass{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			float _Value1;
			float _Value2;
			float _Value3;
			float _Displacement;

			struct appdata {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};

			v2f vert(appdata_base v) {

				v2f o;
				
				// v.vertex.x += sin((v.vertex.y * _Time * _Value3) * _Value2) * _Value1;
				v.vertex.xyz += sin(v.normal * _Displacement * _Time);

				o.color = float4(v.normal, 1)*0.5 + 0.5;
				o.pos = UnityObjectToClipPos(v.vertex);

				return o;

			}

			float4 frag(v2f i) : COLOR
			{
				return i.color;

			}
			ENDCG
		}
	}
	Fallback off
}
