using UnityEngine;
using UnityEngine.UI;

public enum FadeMode
{
	LeftTORight,
	UpToDown,
	LeftUpTORightDown,
	LeftDownTORightUp,
}

public class FadeImage : Image
{
	public FadeMode FadeMode;
	public byte FadeStartAlpha = 0;
	public byte FadeEndAlpha = 255;

	protected override void OnPopulateMesh(VertexHelper toFill)
	{
		base.OnPopulateMesh(toFill);
		UIVertex vertex = new UIVertex();
		for (int i = 0; i < toFill.currentVertCount; i++)
		{
			toFill.PopulateUIVertex(ref vertex, i);

			if (FadeMode == FadeMode.UpToDown)
			{
				if (i == 0 || i == 3)
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeStartAlpha);
					toFill.SetUIVertex(vertex, i);
				}
				else
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeEndAlpha);
					toFill.SetUIVertex(vertex, i);
				}
			}
			else if (FadeMode == FadeMode.LeftTORight)
			{
				if (i == 0 || i == 1)
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeStartAlpha);
					toFill.SetUIVertex(vertex, i);
				}
				else
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeEndAlpha);
					toFill.SetUIVertex(vertex, i);
				}
			}
			else if (FadeMode == FadeMode.LeftDownTORightUp)
			{
				if (i == 0)
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeStartAlpha);
					toFill.SetUIVertex(vertex, i);
				}
				else if (i == 1 || i == 3)
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, (byte)((FadeEndAlpha + FadeStartAlpha) / 2));
					toFill.SetUIVertex(vertex, i);
				}
				else
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeEndAlpha);
					toFill.SetUIVertex(vertex, i);
				}
			}
			else if (FadeMode == FadeMode.LeftUpTORightDown)
			{
				if (i == 1)
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeStartAlpha);
					toFill.SetUIVertex(vertex, i);
				}
				else if (i == 0 || i == 2)
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, (byte)((FadeEndAlpha + FadeStartAlpha) / 2));
					toFill.SetUIVertex(vertex, i);
				}
				else
				{
					vertex.color = new Color32(vertex.color.r, vertex.color.g, vertex.color.b, FadeEndAlpha);
					toFill.SetUIVertex(vertex, i);
				}
			}
		}
	}
}