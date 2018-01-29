using UnityEngine;
using System.Collections;

public class MessagerTest : MonoBehaviour {

	const string EVENT_NAME = "Check";

	// Use this for initialization
	void Start () {
		//添加侦听
		Messenger.AddListener<bool>(EVENT_NAME,Check);

		//抛出事件
		Messenger.DispatchEvent<bool>(EVENT_NAME,false);
		Messenger.DispatchEvent<bool>(EVENT_NAME,true);

		//移除侦听
		Messenger.RemoveListener<bool>(EVENT_NAME,Check);
	}

	/// <summary>
	/// 事件回调
	/// </summary>
	/// <param name="b">回调的参数</param>
	void Check(bool b){
		print("Check:" + b);
	}
}
