Shader "Custom/Spiral" 
{
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			float4 frag(v2f_img i) : COLOR
			{
				float t = _Time.y;
				float2 middle = float2(0.5f, 0.5f);
				float2 UVWithOffset = middle - i.uv.xy;

				float l = length(UVWithOffset);
				float a = atan2(UVWithOffset.x, UVWithOffset.y);
				float c = sin(100 * (sqrt(l) - 0.02*a - 0.3*t));
				c = clamp(c, 0, 1);

				return float4(c, c, c, 1);
			}
			ENDCG
		}
	}

	Fallback off
}