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

	/// <summary>
	/// FlipperOperation 脚本
	/// </summary>
	private FlipperOperation flipperOperation;

	/// <summary>
	/// 蹼的线速度
	/// </summary>
	private float flipperLinearVelocity = 0;

	/// <summary>
	/// 计算蹼的线速度时用的常数
	/// </summary>
	private float flipperLinearVelocityC = 30;

	/// <summary>
	/// 计算蹼的线速度时使用的半径
	/// </summary>
	private float radius = 1;

	private void Awake() {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballRigidbody = ball.GetComponent<Rigidbody2D>();
		flipperOperation = GetComponent<FlipperOperation>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!hasBoundedBall && collision.gameObject.CompareTag("Ball")) {
			HitTheBall(collision.GetContact(0).point);
			hasBoundedBall = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ball")) {
			hasBoundedBall = false;
		}
	}

	private void HitTheBall(Vector2 p) {
		flipperLinearVelocity = flipperOperation.getFlipperOmega() * radius * flipperLinearVelocityC;
		Vector2 v = new Vector2(p.y, -p.x);
		ballRigidbody.AddForce(v.normalized * flipperLinearVelocity);
	}

}
