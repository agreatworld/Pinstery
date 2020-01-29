using UnityEngine.EventSystems;
using UnityEngine;
public class GameQuits : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		Application.Quit();
	}


}
