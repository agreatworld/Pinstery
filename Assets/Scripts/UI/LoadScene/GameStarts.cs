using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStarts : MonoBehaviour , IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		StartCoroutine(LoadAsyncScene());
	}

	private IEnumerator LoadAsyncScene() {
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1");
		while (!asyncLoad.isDone) {
			yield return null;
		}
	}
}
