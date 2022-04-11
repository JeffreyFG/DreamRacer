using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
	public static float Clamp01(float f)
	{
		float result = f;
		if (f < 0)
		{
			result = 0;
		}
		else if (f > 1.0f)
		{
			result = 1.0f;
		}
		return result;
	}

	public static Vector3 Clamp01(Vector3 v)
	{
		Vector3 result = new Vector3(v.x, v.y, v.z);
		result.x = Clamp01(v.x);
		result.y = Clamp01(v.y);
		result.z = Clamp01(v.z);
		return result;
	}

	public static Vector3 Vector3FromColor(Color c)
	{
		return new Vector3(c.r, c.g, c.b);
	}

	public static Color ColorFromVector3(Vector3 v)
	{
		return new Color(v.x, v.y, v.z);
	}

	public static Color AddTintToColor(Color a, Vector3 b)
	{
		return ColorFromVector3(Vector3FromColor(a) + b);
	}
}
