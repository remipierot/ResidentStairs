Shader "Custom/VoronoiSpiral" 
{
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			float2 hash(float2 p)
			{
				return frac(sin(float2(p.x * p.y, p.x + p.y)) * float2(234342.1459123, 373445.3490423));
			}

			float4 voronoi(in float2 x)
			{
				float2 n = floor(x);
				float2 f = frac(x);

				float2 mg, mr, mo;

				float md = 8.0;
				for (int j = -1; j <= 1; j++)
				{
					for (int i = -1; i <= 1; i++)
					{
						float2 g = float2(float(i), float(j));
						float2 o = hash( n + g );
						o = 0.5 + 0.3*sin( _Time.y + 6.2831*o );

						float2 r = g + o - f;
						float d = dot(r, r);

						if (d<md)
						{
							md = d;
							mr = r;
							mg = g;
							mo = o;
						}
					}
				}
				
				md = 8.0;
				for (int j = -2; j <= 2; j++)
				{
					for (int i = -2; i <= 2; i++)
					{
						float2 g = mg + float2(float(i), float(j));
						float2 o = hash( n + g );
						o = 0.5 + 0.3*sin( _Time.y + 6.2831*o );
						float2 r = g + o - f;

						if (dot(mr - r, mr - r) > 0.00001)
						{
							md = min(md, dot(0.5*(mr + r), normalize(r - mr)));
						}
					}
				}
				
				return float4(md, mr, mo.x + mo.y);
			}

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = float4(0, 0, 0, 0);

				float2 resolution = 7.0f * i.uv;
				float4 v = voronoi(resolution);

				/*
				float2 q = v.yz;
				//float a = _Time.y + atan2(q.x, sign(v.w - 1.0) * q.y);
				float l = length(q * 5.0 / (sqrt(v.x))); //+ 0.319 * a;
				float m = fmod(l, 2.0);
				float w = min(fwidth(fmod(l + 1.5, 2.0)), fwidth(fmod(l + 0.5, 2.0))) / 2.0;
				float o = (1.0 - smoothstep(1.85 - w, 1.85 + w, m)) * smoothstep(1.15 - w, 1.15 + w, m);
				o = lerp(0.0, o, smoothstep(0.04, 0.07, v.x));
				o = clamp(o, 0.3f, 0.7f);
				*/

				float c = (v.x <= 0.01f * (2.0f * abs(sin(_Time.y)) + 1.0f)) ? 1.0f : 0.0f;
				color = float4(c, c, c, 1);

				return color;
			}
			ENDCG
		}
	}
}
