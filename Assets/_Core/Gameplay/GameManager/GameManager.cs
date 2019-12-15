using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	public Spawner spawner = null;
	private int _logs = 0;
	public int Logs => _logs;
	public static GameManager gameManager;
	public UnityEvent _onLoose = new UnityEvent();

	private void Awake() {
		gameManager = this;
	}

	private void Start() {
		StartGame();
	}

	public void StartGame() {
		spawner.Spawn(6, 0);
	}

	public void CollectLog(int collected) {
		_logs += collected;
	}

	public void Lose() {
		Debug.Log("T mor lol");
		Camera.main.DOShakePosition(0.5f, 0.5f, 20);
		_onLoose?.Invoke();
	}

	public void TryAgain() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
