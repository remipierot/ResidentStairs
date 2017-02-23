Shader "Custom/PointsGrowing" {
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

			float t = _Time.y / 10.0f;

			float squareRadius = -i.uv.x * i.uv.x - i.uv.y* i.uv.y;
			float l = length(float2(0.5f, 0.5f) - i.uv.xy);
			float c = 0.5f + sin(15.0f * 3.14f * squareRadius * t * l) / (2.0f + 100.0f * squareRadius);
			// 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius)

			if (c < 0.5f) c = 0.0f;
			else c = 1.0f;

			float4 col = float4(c, c, c, 1);

			return col;

		}
		ENDCG

	}
}

Fallback off

}
