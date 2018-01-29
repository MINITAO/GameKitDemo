using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCheckerFromTexture : MonoBehaviour {

	RenderTexturePainter painter;
	PaintCompleteChecker checker;
	// Use this for initialization
	void Start () {
		painter = GetComponent<RenderTexturePainter>();
		checker = GetComponent<PaintCompleteChecker>();

		checker.SetDataByTexture((Texture2D)painter.sourceTexture,painter.penTex,painter.brushScale);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			painter.Drawing(Input.mousePosition);
			checker.Drawing(Input.mousePosition);
			print(checker.Progress);
		}
		else if(Input.GetMouseButtonUp(0))
		{
			painter.EndDraw();
			checker.EndDraw();
		}
	}
}
