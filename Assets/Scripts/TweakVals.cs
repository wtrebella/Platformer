using UnityEngine;
using System.Collections;

public class TweakVals : MonoBehaviour {
	public float timeScale = 1.0f;	

	void Start() {

	}

	void Update() {
		Time.timeScale = timeScale;
	}
}