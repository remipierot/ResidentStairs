Shader "Hidden/BWEffect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Ramp("Ramp", Range(0, 1)) = 0
	_Invert("Invert", Range(0,1)) = 0
}

SubShader {
	Pass {				
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex; 
		float _Ramp;
		float _Invert;

		float4 frag (v2f_img i) : COLOR
		{
			float4 col = tex2D(_MainTex, i.uv);
			float lum = col.r*.3 + col.g*.59 + col.b*.11;
			float3 nb = float3(lum, lum, lum);

			float4 result = col;
			result.rgb = lerp(col.rgb, nb, _Ramp);

			if (_Invert < 0.5f) {
				if (result.r <= 0.3f) result.rgb = 0.0f;
				else result.rgb = 1.0f;
			}
			else {
				if (result.r >= 0.3f) result.rgb = 0.0f;
				else result.rgb = 1.0f;
			}

			return result;
		}
		ENDCG

	}
}

Fallback off

}
