using System.Collections;
using UnityEngine;
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

	[Header("Datas")]
	public PlayerScoreData _scoreData = null;
	#endregion

	#region Current
	private Spawner _spawner = null;
	private static GameManager _instance;
	private CharacterInputs _currentCharacterInstance = null;
	private bool _reloading = false;
	private Vector2 _characterSpawnPosition = Vector2.zero;
	private bool _isGameOver = false;
	#endregion

	#region Properties
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

	private void Start() {
		_scoreData.LoadScore();
	}
	#endregion

	#region Manage Game
	public void StartGame() {
		if (_isGameOver) {
			_isGameOver = false;
		}

		_spawner = FindObjectOfType<Spawner>();
		_spawner.Spawn(6, 0);
		//Spawn a player instance
		_currentCharacterInstance = Instantiate(_characterPrefab, _characterSpawnPosition, Quaternion.identity, _characterSpawnTransform);
	}

	[ContextMenu("Debug Lose")]
	public void Lose() {
		if (_isGameOver) return; //Cannot trigger lose callback again if the game is already lost
		_isGameOver = true;

		_scoreData.SaveScore();

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
}
