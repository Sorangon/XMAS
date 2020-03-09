using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	#region Settings
	[Header("Character")]
	public CharacterInputs _characterPrefab = null;
	public Transform _characterSpawnTransform = null;

	[Header("UI")]
	public StartMenu _startMenu;
	public EndMenu _endMenu;
	#endregion

	#region Current
	private Spawner _spawner = null;
	private int _logs = 0;
	private static GameManager _instance;
	private CharacterInputs _currentCharacterInstance = null;
	private bool _reloading = false;
	private Vector2 _characterSpawnPosition = Vector2.zero;
	#endregion

	#region Properties
	public int Logs => _logs;
	public static GameManager Instance => _instance;
	#endregion

	#region Callbacks
	private void Awake() {
		if(_instance == null) {
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
			return;
		}

		Instantiate(_startMenu);
		_characterSpawnPosition = _characterSpawnTransform.position;
	}
	#endregion

	#region Manage Game
	public void StartGame() {
		_spawner = FindObjectOfType<Spawner>();
		_spawner.Spawn(6, 0);
		//Spawn a player instance
		_currentCharacterInstance = Instantiate(_characterPrefab, _characterSpawnPosition, Quaternion.identity, _characterSpawnTransform);
	}

	[ContextMenu("Debug Lose")]
	public void Lose() {
#if UNITY_EDITOR
		if (!Application.isPlaying) return; //Cannot debug if the game isn't running
#endif

		Camera.main.DOShakePosition(0.5f, 0.5f, 20);
		_currentCharacterInstance._lock = true;
		Instantiate(_endMenu);
	}

	public void TryAgain() {
		if (_reloading) return;
		StartCoroutine(ReloadGameCoroutine());
	}

	private IEnumerator ReloadGameCoroutine() {
		_reloading = true;

		var asyncLoading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

		while (!asyncLoading.isDone) {
			yield return null;
		}

		StartGame();

		_reloading = false;
		yield return null;
	}
#endregion

#region Manage Score
	public void CollectLog(int collected) {
		_logs += collected;
	}
#endregion
}
