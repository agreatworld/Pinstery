using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperOperation : MonoBehaviour {
	/// <summary>
	/// 蹼旋转的最大限度
	/// </summary>
	private float maxRotationZ;

	/// <summary>
	/// 旋转的目标四元数
	/// </summary>
	private Quaternion targetQuaternion;

	/// <summary>
	/// 蹼旋转的速度计算参数
	/// </summary>
	private float flipperOmegaC = 18.0f;

	/// <summary>
	/// 蹼旋转的角速度
	/// </summary>
	private float flipperOmega;

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
	/// 连续按键时间上限
	/// </summary>
	private float constantHandleTime = 0.25f;

	/// <summary>
	/// 连续按键计时器
	/// </summary>
	private float constantHandleTimer = 0;

	/// <summary>
	/// 蹼的原位
	/// </summary>
	private Quaternion identity;

	/// <summary>
	/// 操作键
	/// </summary>
	private KeyCode key;


	// Start is called before the first frame update
	void Start() {
		Application.targetFrameRate = 30;
		InitParameter();
		speedCurve = new AnimationCurve();
		speedCurve.AddKey(new Keyframe(0, minCurveValue));
		speedCurve.AddKey(new Keyframe(maxPressingTime, maxCurveValue));
	}

	// Update is called once per frame
	void FixedUpdate() {
		HandleFlipper();
	}

	private void InitParameter() {
		identity = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
		if (transform.name == "LeftFlipper") {
			// 脚本挂在左蹼上
			maxRotationZ = 30.0f;
			targetQuaternion = Quaternion.Euler(0, 0, maxRotationZ);
			key = KeyCode.LeftShift;
		} else {
			// 脚本挂在右蹼上
			maxRotationZ = 150.0f;
			targetQuaternion = Quaternion.Euler(0, 0, maxRotationZ);
			key = KeyCode.RightShift;
		}
	}

	private void HandleFlipper() {

		if (Input.GetKeyDown(key)) {
			// 开始计时
			constantHandleTimer = Time.fixedDeltaTime;
		}
		if (constantHandleTimer > 0) {
			constantHandleTimer += Time.fixedDeltaTime;
		}
		if (Input.GetKey(key)) {

			if (pressTimer < maxPressingTime) {
				pressTimer += Time.fixedDeltaTime;
			}
			if (Mathf.Abs(transform.rotation.eulerAngles.z - maxRotationZ) > 0.05f) {
				flipperOmega = flipperOmegaC * speedCurve.Evaluate(pressTimer);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.fixedDeltaTime * flipperOmega);
			} else {
				flipperOmega = 0;
			}
		} else {
			pressTimer = 0;
			flipperOmega = 0;
			if (constantHandleTimer > 0 && constantHandleTimer < constantHandleTime) {
				// 蹼慢慢恢复
				transform.rotation = Quaternion.Slerp(transform.rotation, identity, Time.fixedDeltaTime * 10);
			} else {
				// 蹼回到原位，Reset 计时器
				constantHandleTimer = 0;
				transform.rotation = identity;
			}
		}
	}

	public float getFlipperOmega() {
		return flipperOmega;
	}

}
