using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PauseButton : MonoBehaviour, IPointerClickHandler {

	private bool hasPaused = false;

	public void OnPointerClick(PointerEventData eventData) {
		if (hasPaused) {
			// 处于暂停状态
			Time.timeScale = 1;
			hasPaused = false;
		} else {
			// 处于非暂停状态
			Time.timeScale = 0;
			hasPaused = true;
		}
	}
}
