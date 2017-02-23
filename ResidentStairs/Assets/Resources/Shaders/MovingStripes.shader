Shader "Hidden/MovingStripes" {
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

			// float t = ((_Time.y*1000.0f) % 1000.0f) / 1000.f;
			// float t = fmod(_Time.y/5.0f, 1.0f);

			/*
			float4 col = float4(0, 0, 0, 1);
			float t = _Time.y * 10.0; 
			float y = i.uv.y * 6.0f;
			col = lerp(col, _Col1, floor(abs(sin(t + y) - .05)));
			col = lerp(col, _Col2, floor(abs(sin(1.25 + t + y) - .05)));
			col = lerp(col, _Col1, floor(abs(sin(2.5 + t + y) - .05)));
			col = lerp(col, _Col2, floor(abs(sin(3.75 + t + y) - .05)));
			col = lerp(col, _Col1, floor(abs(sin(5.0 + t + y) - .05)));

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
			*/

			float3 bg = float3(0.0, 0.0, 0.0);
			float3 color1 = float3(0.25, 0.25, 0.25);
			float3 color2 = float3(0.5, 0.5, 0.5);
			float3 color3 = float3(0.75, 0.75, 0.75);
			float3 color4 = float3(1.0, 1.0, 1.0);
			float3 color5 = float3(0.75, 0.75, 0.75);

			float3 pixel = bg;

			float t = (_Time.y*10.0f % 100.0) / 100.f;

			if (i.uv.y > t - 1.0 && i.uv.y < t - 0.9) {
				pixel = color1;
			}
			else if (i.uv.y > t - 0.8 && i.uv.y < t - 0.7) {
				pixel = color2;
			}
			else if (i.uv.y > t - 0.6 && i.uv.y < t - 0.5) {
				pixel = color3;
			}
			else if (i.uv.y > t - 0.4 && i.uv.y < t - 0.3) {
				pixel = color4;
			}
			else if (i.uv.y > t - 0.2 && i.uv.y < t - 0.1) {
				pixel = color5;
			}
			else if (i.uv.y > t && i.uv.y < 0.1 + t) {
				pixel = color1;
			}
			else if (i.uv.y > 0.2 + t && i.uv.y < 0.3 + t) {
				pixel = color2;
			}
			else if (i.uv.y > 0.4 + t && i.uv.y < 0.5 + t) {
				pixel = color3;
			}
			else if (i.uv.y > 0.6 + t && i.uv.y < 0.7 + t) {
				pixel = color4;
			}
			else if (i.uv.y > 0.8 + t && i.uv.y < 0.9 + t) {
				pixel = color5;
			}

			float4 fragColor = float4(pixel, 1);
			return fragColor;

		}
		ENDCG

	}
}

Fallback off

}
