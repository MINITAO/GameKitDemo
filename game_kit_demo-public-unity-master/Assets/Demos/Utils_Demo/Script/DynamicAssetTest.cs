using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAssetTest : MonoBehaviour {

	public DynamicAsset[] initDynamicAssets;

	// Use this for initialization
	void Start () {
		DynamicAsset.Init(initDynamicAssets,delegate() {
			initDynamicAssets = null;
			print("初始化完成");	
		},delegate(float obj) {
			print(obj);
		});

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
