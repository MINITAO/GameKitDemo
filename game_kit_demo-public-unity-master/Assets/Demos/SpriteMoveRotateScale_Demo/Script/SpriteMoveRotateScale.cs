using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMoveRotateScale : MonoBehaviour {
	#region private
	//上次记录的两个触模点之间距离  
	private float m_LastDistance = 0f;  
	private float m_PreRadian = 0f;  
	private bool m_IsTwoFinger;
	//判断当前距离与上次距离变化，确定是放大还是缩小
	private float m_Factor=0.01f;
	//当前对象
	private GameObject m_SelectedSprite;
	#endregion

	#region public
	public float minScale = 0.7f;
	public float maxScale = 3f;
	public Transform spriteContainer;
	#endregion

	// Use this for initialization
	void Start () {
		if(spriteContainer.GetComponent<RectTransform>()!=null){
			//is UGUI
			minScale*=100f;
			maxScale*=100f;
			m_Factor*=100f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(m_SelectedSprite)  
		{  
			if(Input.touchCount<2){  
				if(!m_IsTwoFinger){  
					//移动  
					Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
					worldPos.z = m_SelectedSprite.transform.position.z;  
					m_SelectedSprite.transform.position = worldPos;//+new Vector3(-0.4f,0.4f,0f);//后面加的是偏移  
				}  
			}  
			else  
			{  
				//多点时也移动  
//              Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
//				worldPos.z = m_SelectedSprite.transform.position.z;  
//				m_SelectedSprite.transform.position = worldPos;//+new Vector3(-0.4f,0.4f,0f);//后面加的是偏移  

				//缩放  
				Touch t1 = Input.touches[0];  
				Touch t2 = Input.touches[1];  
				var dx = t1.position.x - t2.position.x;  
				var dy = t1.position.y - t2.position.y;  
				float distance = Mathf.Sqrt(dx * dx + dy * dy);  
				if(!m_IsTwoFinger) m_LastDistance = distance;  

				float sc = (distance - m_LastDistance) * m_Factor;  
				m_SelectedSprite.transform.localScale += new Vector3(sc,sc,0f);  
				if(m_SelectedSprite.transform.localScale.x<minScale){  
					m_SelectedSprite.transform.localScale = new Vector3(minScale,minScale,1f);  
				}else if(m_SelectedSprite.transform.localScale.x>maxScale){  
					m_SelectedSprite.transform.localScale = new Vector3(maxScale,maxScale,1f);  
				}  
				m_LastDistance = distance;  

				//旋转  
				float nowRadian = Mathf.Atan2(t1.position.y- t2.position.y,t1.position.x-t2.position.x);  
				if(!m_IsTwoFinger) m_PreRadian = nowRadian;  

				m_SelectedSprite.transform.Rotate(0f,0f, 180f / Mathf.PI * (nowRadian - m_PreRadian));  
				m_PreRadian = nowRadian;  

				m_IsTwoFinger = true;  
			}  
		} 

		if(Application.platform== RuntimePlatform.IPhonePlayer || Application.platform== RuntimePlatform.Android){  
			if(Input.touchCount==0){  
				m_IsTwoFinger = false;  
				if(m_SelectedSprite){
					CheckValidSprite();
					m_SelectedSprite = null;
				}  
			}  
		}
	}

	void CheckValidSprite(){
		if(m_SelectedSprite){
			//如果Sprite 超过某个区域，则Destroy

		}
	}


	#region 接收SpriteLayerInputManager 事件
	void OnDown( SpriteLayerInputManager.TouchEvent evt){  
		if(m_SelectedSprite==null && m_SelectedSprite!=evt.pressObject){  
			if(evt.pressObject.transform.parent==spriteContainer){  
				m_SelectedSprite = evt.pressObject;  
				m_SelectedSprite.transform.SetAsLastSibling();  
				for(int i=0;i<spriteContainer.childCount;++i){  
					Transform child = spriteContainer.GetChild(i);  
					Vector3 v = child.localPosition;  
					v.z = -i*0.001f;  
					child.localPosition = v;  
				}  
			}  
		}  
	}  

	void OnUp( SpriteLayerInputManager.TouchEvent evt){  
		if(m_SelectedSprite && m_SelectedSprite==evt.pressObject){  
			if(evt.pressObject.transform.parent==spriteContainer){  
				CheckValidSprite();
				m_SelectedSprite = null;  
			}  
		}  
	}
	#endregion
}
