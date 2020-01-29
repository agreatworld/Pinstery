using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarDontDestory : MonoBehaviour {

	private void Awake() {
		DontDestroyOnLoad(this);
	}

	// Start is called before the first frame update
	void Start() {
		GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

}
