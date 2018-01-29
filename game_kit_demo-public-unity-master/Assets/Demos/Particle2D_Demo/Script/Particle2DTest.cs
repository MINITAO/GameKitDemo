using UnityEngine;
using System.Collections;

public class Particle2DTest : MonoBehaviour {

	private Particle2DSystem ps;

	void Awake(){
		ps = GetComponent<Particle2DSystem>();
		ps.playOnAwake = false;
	}

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(2f);
		print("particle is play");
		ps.Play();
		yield return new WaitForSeconds(5f);
		print("particle is pause");
		ps.Stop();


		yield return new WaitForSeconds(2f);
		print("particle is play");
		ps.Play();
		yield return new WaitForSeconds(5f);
		print("particle is stop");
		ps.Stop(true);
	}

}
