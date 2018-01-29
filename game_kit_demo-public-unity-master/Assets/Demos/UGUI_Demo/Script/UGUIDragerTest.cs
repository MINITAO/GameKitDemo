using UnityEngine;
using System.Collections;

public class UGUIDragerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private Collider2D m_Current;
	
	void OnHover(Collider2D[] cols){
		m_Current = cols[0];
		print("Hover: "+cols[0].gameObject.name);
	}
	void OnHoverOut(){
		if(m_Current){
			print("HoverOut: "+m_Current.gameObject.name);
			m_Current=null;
		}
	}
}
