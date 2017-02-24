Shader "Custom/VoronoiSpiral" 
{
	Properties
	{
		_MinResolution("Minimum Resolution", Range(0,10)) = 1.0
		_MaxResolution("Maximum Resolution", Range(0,10)) = 7.0
		_MinThickness("Minimum Thickness", Range(0,0.5)) = 0.01
		_MaxThickness("Maximum Thickness", Range(0,0.5)) = 0.03
		_ResolutionSpeed("Speed of Resolution variation", Range(0,10)) = 0.1
		_ThicknessSpeed("Speed of Thickness variation", Range(0, 10)) = 1.0
	}

		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			float _MinResolution;
			float _MaxResolution;
			float _MinThickness;
			float _MaxThickness;
			float _ResolutionSpeed;
			float _ThicknessSpeed;

			float2 hash(float2 p)
			{
				return frac(sin(float2(p.x * p.y, p.x + p.y)) * float2(234342.1459123, 373445.3490423));
			}

			float4 voronoi(float2 x)
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

				float2 centeredUV = i.uv - float2(0.5f, 0.5f);
				float2 resolution = ((abs(sin(_Time.y * _ResolutionSpeed)) * (_MaxResolution - _MinResolution)) + _MinResolution) * centeredUV;
				float4 v = voronoi(resolution);
				float c = (v.x <= ((abs(sin(_Time.y * _ThicknessSpeed)) * (_MaxThickness - _MinThickness)) + _MinThickness)) ? 1.0f : 0.0f;
				color = float4(c, c, c, 1);

				return color;
			}
			ENDCG
		}
	}
}
