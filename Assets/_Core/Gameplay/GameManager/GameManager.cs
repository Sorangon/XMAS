using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Spawner spawner = null;
	private int _logs = 0;
	public int Logs => _logs;

	private void Start() {
		StartGame();
	}

	public void StartGame() {
		spawner.Spawn(6, 0);
	}

	public void CollectLog(int collected) {
		_logs += collected;
	}
}
