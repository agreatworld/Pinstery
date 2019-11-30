using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithBall : MonoBehaviour {

	/// <summary>
	/// 弹球计时器
	/// </summary>
	private float timer = 0;

	/// <summary>
	/// 两次弹球的时间间隔最小值
	/// </summary>
	private float interactTime = 0.1f;

	/// <summary>
	/// 是否能触发弹球操作
	/// </summary>
	private bool canTrigger = true;

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
	private float flipperLinearVelocityC = 10;

	/// <summary>
	/// 计算蹼的线速度时使用的半径
	/// </summary>
	private float radius = 1;

	/// <summary>
	/// 蹼的长度
	/// </summary>
	private float length;

	private void Awake() {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballRigidbody = ball.GetComponent<Rigidbody2D>();
		flipperOperation = GetComponent<FlipperOperation>();
		length = GetComponent<EdgeCollider2D>().edgeRadius;
		Debug.Log(length);
	}

	private void FixedUpdate() {
		if (!canTrigger) {
			timer += Time.fixedDeltaTime;
		}
		if (timer > interactTime) {
			timer = 0;
			canTrigger = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!hasBoundedBall && collision.gameObject.CompareTag("Ball")) {
			if (canTrigger) {
				HitTheBall(collision.GetContact(0).normal);
				canTrigger = false;
			}
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
		ballRigidbody.AddForce(-p * flipperLinearVelocity);
	}

}
