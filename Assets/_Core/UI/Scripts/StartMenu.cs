using UnityEngine;
using DG.Tweening;

/// <summary>
/// Manages the start menu UI interactions with other classes and game events
/// </summary>
public class StartMenu : MonoBehaviour {
	#region References
	[SerializeField] private CanvasGroup _startMenuGroup = null;
	#endregion

	#region Start Game
	public void Active() {
		_startMenuGroup.alpha = 1f;
		_startMenuGroup.interactable = true;
		_startMenuGroup.blocksRaycasts = true;
	}

	/// <summary>
	/// Fade out the start menu screen and starts the game
	/// </summary>
	public void StartGame() {
		Tween fade = _startMenuGroup.DOFade(0f, 0.5f);
		_startMenuGroup.interactable = false;
		_startMenuGroup.blocksRaycasts = false;
		fade.OnComplete(() => Destroy(gameObject));

		GameManager.Instance.StartGame();
	}

	#endregion
}
