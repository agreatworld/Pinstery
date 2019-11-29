using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperOperation : MonoBehaviour {
	/// <summary>
	/// 蹼旋转的最大限度（四元数描述）
	/// </summary>
	private float maxRotationZ = 0.3f;

	/// <summary>
	/// 旋转的目标四元数
	/// </summary>
	private Quaternion targetQuaternion;

	/// <summary>
	/// 蹼旋转的速度
	/// </summary>
	private float flipperSpeed = 8.0f;

	/// <summary>
	/// 按键时长计时器
	/// </summary>
	private float pressTimer = 0.0f;

	/// <summary>
	/// 速度曲线达到最大值的按键时间
	/// </summary>
	private float maxPressingTime = 0.25f;

	/// <summary>
	/// 速度曲线最大值
	/// </summary>
	private float minCurveValue = 1.0f;

	/// <summary>
	/// 速度曲线最小值
	/// </summary>
	private float maxCurveValue = 3.0f;

	/// <summary>
	/// 速度曲线
	/// </summary>
	private AnimationCurve speedCurve;

	/// <summary>
	/// 是否已反弹球
	/// </summary>
	private bool hasBoundedBall = false;

	/// <summary>
	/// 连续按键时间上限
	/// </summary>
	private float constantHandleTime = 0.25f;

	/// <summary>
	/// 连续按键计时器
	/// </summary>
	private float constantHandleTimer = 0;

	// Start is called before the first frame update
	void Start() {
		Application.targetFrameRate = 30;
		targetQuaternion = new Quaternion(0, 0, maxRotationZ, 0);
		speedCurve = new AnimationCurve();
		speedCurve.AddKey(new Keyframe(0, minCurveValue));
		speedCurve.AddKey(new Keyframe(maxPressingTime, maxCurveValue));
	}

	// Update is called once per frame
	void FixedUpdate() {
		HandleFlipper();
	}

	private void HandleFlipper() {
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			// 开始计时
			constantHandleTimer = Time.deltaTime;
		}
		if (constantHandleTimer > 0) {
			constantHandleTimer += Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftShift)) {
			if (pressTimer < maxPressingTime) {
				pressTimer += Time.deltaTime;
			}
			if (transform.rotation.z < maxRotationZ) {
				transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, flipperSpeed * Time.deltaTime * speedCurve.Evaluate(pressTimer));
			}
		} else {
			pressTimer = 0;
			//transform.rotation = Quaternion.identity;
			if (constantHandleTimer > 0 && constantHandleTimer < constantHandleTime) {
				// 蹼慢慢恢复
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10);
			} else {
				// 蹼回到原位，Reset 计时器
				constantHandleTimer = 0;
				transform.rotation = Quaternion.identity;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (!hasBoundedBall && collision.gameObject.CompareTag("Ball")) {
			//collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
			hasBoundedBall = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ball")) {
			hasBoundedBall = false;
		}
	}
}
