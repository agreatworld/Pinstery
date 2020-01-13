using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour {

	private Rigidbody2D rb;

	private KeyCode key;

	private float positiveVelocity;

	private float negativeVelocity;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		key = transform.name == "LeftFlipper" ? KeyCode.LeftShift : KeyCode.RightShift;
		positiveVelocity = transform.name == "LeftFlipper" ? 1000f : -1000f;
		negativeVelocity = transform.name == "LeftFlipper" ? -1000f : 1000f;
	}

	// Update is called once per frame
	void FixedUpdate() {
		HandleFlipper();
	}

	private void HandleFlipper() {
		if (Input.GetKey(key)) {
			if (Clamp(transform.eulerAngles.z) < 30) {
				rb.angularVelocity = positiveVelocity;
			} else {
				rb.angularVelocity = 0;
			}
		} else {
			if (Clamp(transform.eulerAngles.z) > -20) {
				rb.angularVelocity = negativeVelocity;
			} else {
				rb.angularVelocity = 0;
			}
		}

	}

	private float Clamp(float angle) {
		angle = (angle + 360) % 360;
		return angle > 180 ? angle - 360 : angle;
	}
}
