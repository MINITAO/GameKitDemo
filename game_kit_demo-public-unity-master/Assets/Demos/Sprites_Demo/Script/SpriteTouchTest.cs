using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SpriteTouchTest : MonoBehaviour {


	void OnClick(SpriteLayerInputManager.TouchEvent evt){
		print(evt.pointerOnObject.name);
	}
	void OnClickNone(SpriteLayerInputManager.TouchEvent evt){

	}
	void OnDown( SpriteLayerInputManager.TouchEvent evt){
		if(evt.pressObject){
			evt.pressObject.transform.DOScale(1.1f,0.1f);
		}
	}

	void OnUp( SpriteLayerInputManager.TouchEvent evt){
		if(evt.pressObject){
			evt.pressObject.transform.DOScale(1f,0.1f);
		}
	}

	public void OnClickSingle(GameObject go){
		print("click "+go.name);
	}
}
