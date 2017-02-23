Shader "Custom/UnlitDestroyedAdv01" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Displacement("Displacement", Range(0,300000)) = 0.0
	}
		SubShader {
		Pass {
			Tags { "RenderType" = "Opaque" }
			LOD 100

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct v2f {
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				float3 color : COLOR0;
				float3 vertex : VERTEX;
			};

			fixed4 _Color;
			float _Displacement;

			v2f vert(appdata_full v)
			{
				v2f o; 
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float3 offset = TransformViewToProjection(norm.xyz);

				o.pos.xyz += _Displacement * offset.xyz * 5.0f;

				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				return _Color;
			}
			ENDCG
		}
	}
}
