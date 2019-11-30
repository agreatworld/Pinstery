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

	/// <summary>
	/// 线碰撞器
	/// </summary>
	private EdgeCollider2D edgeCollider;

	/// <summary>
	/// 蹼的原点（以线碰撞器为参照）
	/// </summary>
	private Vector2 flipperOrigin;

	/// <summary>
	/// 蹼的末端（以线碰撞器为参照）
	/// </summary>
	private Vector2 flipperTerminal;

	private void Awake() {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballRigidbody = ball.GetComponent<Rigidbody2D>();
		flipperOperation = GetComponent<FlipperOperation>();
		edgeCollider = GetComponent<EdgeCollider2D>();
		length = edgeCollider.bounds.size.magnitude;
		if (transform.name == "LeftFlipper") {
			flipperOrigin = edgeCollider.bounds.min;
			flipperTerminal = edgeCollider.bounds.max;
		} else {
			flipperOrigin = edgeCollider.bounds.max;
			flipperTerminal = edgeCollider.bounds.min;
		}
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
		if (collision.gameObject.CompareTag("Ball")) {
			if (canTrigger) {
				HitTheBall(collision.GetContact(0).normal, collision.GetContact(0).point);
				canTrigger = false;
			}
		}
	}

	private void HitTheBall(Vector2 normal, Vector2 point) {
		radius = (point - flipperOrigin).magnitude / length;
		flipperLinearVelocity = flipperOperation.getFlipperOmega() * radius * flipperLinearVelocityC;
		ballRigidbody.AddForce(-normal * flipperLinearVelocity);
	}

}
