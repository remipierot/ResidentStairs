// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UnlitOutlineAdv" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}
	
	SubShader {
		Pass  {
			Tags { "RenderType" = "Opaque" }
			LOD 200
			Cull Front
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma fragment frag
			#pragma vertex vert 

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _Color;
			
			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertOutput {
				float4 pos : SV_POSITION;
				float3 vertex : NORMAL;
			};

			vertOutput vert(appdata v) {

				vertOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}


			fixed4 frag(vertOutput i) : COLOR
			{
				fixed4 col = _Color;
				return col;
			}

			ENDCG
		}

	}
}
