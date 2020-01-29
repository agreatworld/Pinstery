using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoadSceneButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{

	private Vector3 initScale;

	private bool hasClicked = false;

	// Start is called before the first frame update
	void Start() {
		initScale = transform.localScale;
	}

	// Update is called once per frame
	void Update() {

	}
	public void OnPointerEnter(PointerEventData eventData) {
		if (!hasClicked) {
			transform.DOScale(initScale * 1.3f, 0.2f);
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		if (!hasClicked) {
			transform.DOScale(initScale, 0.2f);
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		hasClicked = true;
		transform.DOMove((Vector2)transform.position + Vector2.up * 1.5f, 0.2f);
		GetComponent<Text>().DOFade(0.0f, 0.2f);
	}
}
