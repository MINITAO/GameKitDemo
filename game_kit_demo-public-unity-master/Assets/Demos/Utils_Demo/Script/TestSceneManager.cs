using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneManager : MonoBehaviour {

	public ResManager.Asset[] preloadAssets;


	public SpriteRenderer testSprite;

	// Use this for initialization
	void Start () {
		if(preloadAssets!=null && preloadAssets.Length>0)
		{
			ResManager.Instance.LoadGroup(preloadAssets,PreloadComplete);
		}
		else
		{
			PreloadComplete(null);
		}
	}

	void PreloadComplete(ResManager.Asset[] assets){
		print("加载完成");

		//取图集中的sprite
		testSprite.sprite = ResManager.Instance.GetAsset("Encryption/sprites.xml").sprites["a_body.png"];


		//取单独加载的sprite
//		testSprite.sprite = ResManager.Instance.GetAsset("Encryption/storyboard.png").sprite;
	}
	

	void OnDestry(){
		ResManager.Instance.DisposeAll();
	}
}
