using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithBall : MonoBehaviour {

	/// <summary>
	/// 是否已反弹球
	/// </summary>
	private bool hasBoundedBall = false;

	/// <summary>
	/// 球
	/// </summary>
	private GameObject ball;

	/// <summary>
	/// 球的刚体组件
	/// </summary>
	private Rigidbody2D ballRigidbody;

	private void Awake() {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballRigidbody = ball.GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!hasBoundedBall && collision.gameObject.CompareTag("Ball")) {
			HitTheBall();
			hasBoundedBall = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ball")) {
			hasBoundedBall = false;
		}
	}

	private void HitTheBall() {
		ballRigidbody.AddForce(Vector2.up * 200);
	}

}
