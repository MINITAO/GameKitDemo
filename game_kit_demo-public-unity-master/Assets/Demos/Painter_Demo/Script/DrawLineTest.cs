using UnityEngine;
using System.Collections;

public class DrawLineTest : MonoBehaviour {

	public RenderTexturePainter painter;
	private bool m_isEraser = false;
	private float m_alpha = 1f;
	private bool m_clickDraw = false;
	private bool m_isColorfulLine=false;

	public Texture[] penTexs;
	private int m_penTexIndex;


	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForEndOfFrame();
		Application.targetFrameRate=60;
		if(painter) {
			painter.canvasWidth =  Mathf.CeilToInt( Native2dScreenUtil.GetScreenWidth())*100;
			painter.canvasHeight =  Mathf.CeilToInt(Native2dScreenUtil.GetScreenHeight())*100;
			painter.Init();
			m_isEraser = painter.isErase;
		}
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			if(painter&&m_clickDraw) {
				painter.ClickDraw(Input.mousePosition,null,null,painter.brushScale);
			}
		}
		if(Input.GetMouseButton(0)){
			if(painter) {
				if(!m_clickDraw)
					painter.Drawing(Input.mousePosition);
			}
		}
		if(Input.GetMouseButtonUp(0)){
			if(painter) 
				painter.EndDraw();
		}
	}


	void OnGUI(){
		if(painter)
		{
			if(GUI.Button(new Rect(10,10,100,40),"Clear Canvas")){
				painter.ClearCanvas();
			}
			m_isEraser = GUI.Toggle(new Rect(120,10,100,40),m_isEraser,"Is Earse","Button");
			if(m_isEraser!=painter.isErase){
				painter.isErase=m_isEraser;
			}
			if(!m_isColorfulLine){
				m_clickDraw = GUI.Toggle(new Rect(240,10,100,40),m_clickDraw,"Click Draw","Button");
			}else{
				m_clickDraw = false;
			}
			if(!m_isColorfulLine && GUI.Button(new Rect(350,10,100,40),"Random Color")){
				painter.penColor=Random.ColorHSV();
			}

			GUI.color = Color.white;
			GUI.Label( new Rect(10, 60, 200, 40) ,"Brush Scale :"+painter.brushScale.ToString("N2"));
			painter.brushScale = GUI.HorizontalSlider(new Rect(10, 80, 200, 40), painter.brushScale , 0.1F, 5F);

			GUI.color = Color.white;
			GUI.Label( new Rect(10, 100, 200, 40) ,"Canvas Alpha :"+m_alpha.ToString("N2"));
			m_alpha = GUI.HorizontalSlider(new Rect(10, 120, 200, 40), m_alpha , 0F, 1F);
			painter.canvasAlpha = m_alpha;

			if(penTexs.Length>1 && GUI.Button(new Rect(220,60,110,40),"Change PenTex")){
				++m_penTexIndex;
				if(m_penTexIndex>=penTexs.Length) m_penTexIndex = 0;
				painter.penTex = penTexs[m_penTexIndex];
			}
			m_isColorfulLine = GUI.Toggle(new Rect(340,60,110,40),m_isColorfulLine,"Is ColorfulLine","Button");
			if(m_isColorfulLine) painter.paintType= RenderTexturePainter.PaintType.DrawColorfulLine;
			else painter.paintType= RenderTexturePainter.PaintType.DrawLine;

		}
	}
}
