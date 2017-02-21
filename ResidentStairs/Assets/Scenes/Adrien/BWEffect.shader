Shader "Hidden/BWEffect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Color("Color", Color) = (0, 0, 0, 1)
	_bwBlend("Black & White blend", Range(0, 1)) = 0
}

SubShader {
	Pass {				
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex; 
		uniform float _bwBlend;
		uniform float4 _Color;

		float4 frag (v2f_img i) : COLOR
		{
			float4 col = tex2D(_MainTex, i.uv);
			float lum = col.r*.3 + col.g*.59 + col.b*.11;
			float3 nb = float3(lum, lum, lum);

			float4 result = col;
			result.rgb = lerp(col.rgb, nb, _bwBlend);
			result.r *= _Color.r;
			result.g *= _Color.g;
			result.b *= _Color.b;
			return result;
		}
		ENDCG

	}
}

Fallback off

}
