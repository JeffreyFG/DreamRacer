using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
	public Vector3 SelectedTint = new Vector3(0.2f, 0.3f, 0.3f);
	public Vector3 HighlightTint = new Vector3(0.0f, 0.2f, 0.2f);
	public Vector3 DisabledTint = new Vector3(0.1f, -0.1f, -0.1f);

	private Material material;
	private Color originalColor;
	private bool isSelected = false;
	private bool isHighlighted = false;
	private bool isEnabled;

	void Start()
	{
		material = gameObject.GetComponentInChildren<Renderer>().material;
		originalColor = material.color;
	}

	public void OnHighlight(bool enabled)
	{
		Vector3 tint = enabled ? HighlightTint : DisabledTint;
		if (isSelected)
		{
			tint += SelectedTint;
		}
		material.color = Util.AddTintToColor(originalColor, tint);
		isHighlighted = true;
		isEnabled = enabled;
	}

	public void OnUnhighlight()
	{
		if (isSelected)
		{
			material.color = Util.AddTintToColor(originalColor, SelectedTint);
		}
		else
		{
			material.color = originalColor;
		}
		isHighlighted = false;
	}

	public void OnSelect()
	{
		Vector3 tint = isHighlighted ? HighlightTint + SelectedTint : SelectedTint;
		material.color = Util.AddTintToColor(originalColor, tint);
		isSelected = true;
	}

	public void OnDeselect()
	{
		if (isHighlighted)
		{
			Vector3 tint = isEnabled ? HighlightTint : DisabledTint;
			material.color = Util.AddTintToColor(originalColor, tint);
		}
		else
		{
			material.color = originalColor;
		}
		isSelected = false;
	}
}
