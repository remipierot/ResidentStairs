Shader "Hidden/MyMovingStripes" {
Properties{
	_MainTex("Base (RGB)", 2D) = "white" {}
	_Col1("First Color", Color) = (0, 0, 0, 1)
	_Col2("Second Color", Color) = (1, 1, 1, 1)
}

SubShader{
	Pass{
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform float4 _Col1;
		uniform float4 _Col2;

		float4 frag(v2f_img i) : COLOR
		{	

			float t = (_Time.y*10.0f % 100.0) / 100.f;

			float4 col = float4(0, 0, 0, 1);

			if (i.uv.y > t - 1.0 && i.uv.y < t - 0.9) {
				col = _Col1;
			}
			else if (i.uv.y > t - 0.8 && i.uv.y < t - 0.7) {
				col = _Col2;
			}
			else if (i.uv.y > t - 0.6 && i.uv.y < t - 0.5) {
				col = _Col1;
			}
			else if (i.uv.y > t - 0.4 && i.uv.y < t - 0.3) {
				col = _Col2;
			}
			else if (i.uv.y > t - 0.2 && i.uv.y < t - 0.1) {
				col = _Col1;
			}
			else if (i.uv.y > t && i.uv.y < 0.1 + t) {
				col = _Col2;
			}
			else if (i.uv.y > 0.2 + t && i.uv.y < 0.3 + t) {
				col = _Col1;
			}
			else if (i.uv.y > 0.4 + t && i.uv.y < 0.5 + t) {
				col = _Col2;
			}
			else if (i.uv.y > 0.6 + t && i.uv.y < 0.7 + t) {
				col = _Col1;
			}
			else if (i.uv.y > 0.8 + t && i.uv.y < 0.9 + t) {
				col = _Col2;
			}
			return col;

		}
		ENDCG

	}
}

Fallback off

}
