using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlurPopUp : MonoBehaviour {


	private bool _haveUI = false;

	public RawImage mask;
	public RectTransform container;
	public Button btnBack;

	private RenderTexture _rt;


	void OnEnable(){
		StartCoroutine(Anim());
	}

	void OnDisable(){
		if(_rt!=null)
			RenderTexture.ReleaseTemporary(_rt);
	}

	IEnumerator Anim(){
		(transform as RectTransform).offsetMin = new Vector2 (0,0);
		(transform as RectTransform).offsetMax = new Vector2 (0,0);

		container.localScale = Vector3.zero;
		Color c = mask.color;
		c.a = 0;
		mask.color = c;

		//如果要清晰的图，这儿的宽高就不用除
		_rt = RenderTexture.GetTemporary(Screen.width/25,Screen.height/25,16,RenderTextureFormat.RGB565);
		_rt.autoGenerateMips = false;
		if(_haveUI){
			Camera.main.targetTexture = _rt;
			Camera.main.Render();
			Camera.main.targetTexture = null;
		}else{
			GameObject go = new GameObject();
			Camera camera = go.AddComponent<Camera>();
			camera.clearFlags = Camera.main.clearFlags;
			camera.backgroundColor = Camera.main.backgroundColor;
			camera.orthographic = Camera.main.orthographic;
			camera.orthographicSize = Camera.main.orthographicSize;
			camera.transform.position = Camera.main.transform.position;

			camera.targetTexture = _rt;
			camera.Render();
			camera.targetTexture = null;

			Destroy(go);
		}
		mask.texture = _rt;//RenderTextureToTexture2D();

		yield return mask.DOFade(1f,0.3f).WaitForCompletion();
		yield return container.DOScale(1,0.3f).WaitForCompletion();

		btnBack.onClick.AddListener(delegate() {
			gameObject.SetActive(false);
		});
	}


	public void Show(bool haveUI){
		_haveUI = haveUI;
		gameObject.SetActive(true);
	}


	Texture2D RenderTextureToTexture2D(){
		if(_rt!=null){
			Texture2D t = new Texture2D(_rt.width,_rt.height,TextureFormat.ARGB32,false);
			RenderTexture.active = _rt;
			t.ReadPixels( new Rect(0,0,_rt.width,_rt.height),0,0,false);
			t.Apply();
			RenderTexture.active = null;
			return t;
		}
		return null;
	}

	#if !UNITY_EDITOR
	void OnDestroy(){
		//如果最后不用这张RenderTexture，则释放
		if(_rt){
			RenderTexture.ReleaseTemporary(_rt);
		}
	}
	#endif
}
