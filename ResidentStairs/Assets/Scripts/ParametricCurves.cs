using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCurves {
	public static Vector2 Sinus(float t)
	{
		Vector2 p = Vector2.zero;
		float angle = t * 2 * Mathf.PI;

		p.x = t;
		p.y = Mathf.Sin(angle);

		return p;
	}

	public static Vector2 Circle(float t)
	{
		Vector2 p = Vector2.zero;
		float angle = t * 2 * Mathf.PI;

		p.x = Mathf.Cos(angle);
		p.y = Mathf.Sin(angle);

		return p;
	}

	public static Vector2 Lissajous(float a, float b, float kx, float ky, float t)
	{
		Vector2 p = Vector2.zero;
		float angle = t * 2 * Mathf.PI;

		p.x = a * Mathf.Cos(kx * angle);
		p.y = b * Mathf.Sin(ky * angle);

		return p;
	}

	public static Vector2 Hypotrochoid(float R, float r, float d, float t)
	{
		Vector2 p = Vector2.zero;
		float angle = t * 2 * Mathf.PI;

		p.x = (R - r) * Mathf.Cos(angle) + d * Mathf.Cos(angle * (R - r) / r);
		p.y = (R - r) * Mathf.Sin(angle) - d * Mathf.Sin(angle * (R - r) / r);

		return p;
	}

	public static Vector2 PatternQueen(float a, float b, float c, float d, float j, float k, float t)
	{
		Vector2 p = Vector2.zero;
		float angle = t * 2 * Mathf.PI;

		p.x = Mathf.Cos(a * angle) - Mathf.Pow(Mathf.Cos(b * angle), j);
		p.y = Mathf.Sin(c * angle) - Mathf.Pow(Mathf.Sin(d * angle), k);

		return p;
	}

	public static Vector2 Stars(float k, float t)
	{
		Vector2 p = Vector2.zero;

		p.x = (1.0f / k) * (k - 1) * Mathf.Cos(t) + (1.0f / k) * Mathf.Cos(t * (k - 1));
		p.y = (1.0f / k) * (k - 1) * Mathf.Sin(t) - (1.0f / k) * Mathf.Sin(t * (k - 1));

		return p;
	}

	public static Vector2 Plus(float t)
	{
		Vector2 p = Vector2.zero;

		p.x = (t < 0.5f) ? 2.0f * t : 0.5f;
		p.x = (p.x * 2.0f) - 1.0f;
		p.y = (t < 0.5f) ? 0.5f : 2.0f * (t - 0.5f);
		p.y = (p.y * 2.0f) - 1.0f;

		return p;
	}

	public static Vector2 Square(float t)
	{
		Vector2 p = Vector2.zero;

		p.x = (t < 0.25f) ? 4.0f * t : (t < 0.5f) ? 1.0f : (t < 0.75f) ? 1.0f - (4.0f * (t - 0.5f)) : 0.0f;
		p.x = (p.x * 2.0f) - 1.0f;
		p.y = (t < 0.25f) ? 1.0f : (t < 0.5f) ? 1.0f - (4.0f * (t - 0.25f)) : (t < 0.75f) ? 0.0f : (4.0f * (t - 0.75f));
		p.y = (p.y * 2.0f) - 1.0f;

		return p;
	}

	public static Vector2 Triangle(float t)
	{
		Vector2 p = Vector2.zero;

		p.x = (t < 0.33f) ? 3.0f * t : (t < 0.66f) ? 1.0f - (1.5f * (t - 0.33f)) : 0.5f - (1.5f * (t - 0.66f));
		p.x = (p.x * 2.0f) - 1.0f;
		p.y = (t < 0.33f) ? 0.0f : (t < 0.66f) ? 3.0f * (t - 0.33f) : 1.0f - (3.0f * (t - 0.66f));
		p.y = (p.y * 2.0f) - 1.0f;

		return p;
	}
}
