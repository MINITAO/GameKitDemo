using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Joysticktest : MonoBehaviour {
	public Text text;
	// Use this for initialization
	void Start () {
		GetComponent<JoystickController>().OnJoystickMovement+= delegate(JoystickController arg1, Vector2 arg2) {
			text.text = arg2.ToString();
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
