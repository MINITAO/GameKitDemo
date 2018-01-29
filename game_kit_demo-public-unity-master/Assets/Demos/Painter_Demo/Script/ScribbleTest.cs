using UnityEngine;
using System.Collections;

public class ScribbleTest : MonoBehaviour {

	public RenderTexturePainter lipstick,eyelash;

	private bool m_isEraser = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			if(lipstick&&eyelash) {
				lipstick.Drawing(Input.mousePosition);
				eyelash.Drawing(Input.mousePosition);
			}
		}
		if(Input.GetMouseButtonUp(0)){
			if(lipstick&&eyelash) {
				lipstick.EndDraw();
				eyelash.EndDraw();
			}
		}
	}

	void OnGUI(){
		m_isEraser = GUI.Toggle(new Rect(5,5,100,40),m_isEraser,"Is Earse","Button");
		if(lipstick && eyelash){
			if(m_isEraser!=lipstick.isErase){
				lipstick.isErase=m_isEraser;
				eyelash.isErase=m_isEraser;
			}
		}

		if(GUI.Button(new Rect(5,55,100,40),"Clear Canvas")){
			if(lipstick && eyelash){
				lipstick.ClearCanvas();
				eyelash.ClearCanvas();
			}
		}

		if(lipstick && eyelash){
			GUI.color = Color.white;
			GUI.Label( new Rect(5, 100, 150, 40) ,"Brush Scale :"+lipstick.brushScale.ToString("N2"));
			lipstick.brushScale = GUI.HorizontalSlider(new Rect(5, 120, 150, 40), lipstick.brushScale , 0.1F, 2F);
			eyelash.brushScale = lipstick.brushScale;
		}
	}
}
