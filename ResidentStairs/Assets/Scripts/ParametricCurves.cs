using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCurves {
	public static Vector2 Sinus(float t)
	{
		Vector2 p = Vector2.zero;

		p.x = t;
		p.y = Mathf.Sin(t * 2*Mathf.PI);

		return p;
	}

	public static Vector2 Lissajous(float a, float b, float kx, float ky, float t)
	{
		Vector2 p = Vector2.zero;
		float angle = t * 360.0f;

		p.x = a * Mathf.Cos(kx * angle);
		p.y = b * Mathf.Sin(ky * angle);

		return p;
	}

	public static Vector2 Hypotrochoid(float R, float r, float d, float t)
	{
		Vector2 p = Vector2.zero;
		float angle = t * 360.0f;

		p.x = (R - r) * Mathf.Cos(angle) + d * Mathf.Cos(angle * (R - r) / r);
		p.y = (R - r) * Mathf.Sin(angle) - d * Mathf.Sin(angle * (R - r) / r);

		return p;
	}

	public static Vector2 PatternQueen(float a, float b, float c, float d, float j, float k, float t)
	{
		Vector2 p = Vector2.zero;

		p.x = Mathf.Cos(a * t) - Mathf.Pow(Mathf.Cos(b * t), j);
		p.y = Mathf.Sin(c * t) - Mathf.Pow(Mathf.Sin(d * t), k);

		return p;
	}

	public static Vector2 Plus(float t)
	{
		Vector2 p = Vector2.zero;

		p.x = 0;
		p.y = 0;

		return p;
	}

	public static Vector2 Arc(float t)
	{
		Vector2 p = Vector2.zero;

		p.x = 0;
		p.y = 0;

		return p;
	}
}
