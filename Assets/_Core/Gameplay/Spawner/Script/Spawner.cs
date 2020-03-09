﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	#region Settings
	public bool _debugRocks = false;
	public Vector2 center = Vector2.zero;
	public Vector2 size = Vector2.one;
	public LayerMask _exceptionLayers = new LayerMask();
	#endregion

	#region References
	public SlashableTree treePrefab = null;
	public Rock rockPrefab = null;
	#endregion

	#region Constants
	private const float SPAWN_EFFECT_FREQUENCY = 0.06f;
	#endregion

	#region Current
	private int _remaining = 0;
	private int _level = 1;
	private int rockWave = 3;
	private bool _generating = false;
	#endregion

	#region Callbacks
	public void OnEnable() {
		SlashableTree.OnCutTree += OnCutTree;
	}
	public void OnDisable() {
		SlashableTree.OnCutTree -= OnCutTree;
	}
	#endregion

	#region Spawn
	public void Spawn(int trees, int rocks) {
		Debug.Log(string.Format("Generate {0} trees and {1} rock",trees, rocks));
		_remaining += trees;
		StartCoroutine(SpawnCoroutine(trees, rocks));
	}
	#endregion

	private Vector2 GetRandomVector() {
		//Random. 
		Vector2 randomPos = new Vector2(Random.Range(-size.x / 2f, size.x / 2f), Random.Range(-size.y / 2f, size.y / 2f));
		randomPos += center;
		return randomPos;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(center, size);
	}

	#region Events
	public void OnCutTree() {
		_remaining--;

		if(_remaining < 3 && !_generating) {
			Invoke("NewWave", 0.5f);
			Debug.Log("Spawn");
			_generating = true;
		}
	}

	private void NewWave() {
		rockWave--;
		int rocks = _debugRocks ? 1 : 0;
		if(rockWave <= 0) {
			rockWave = 3;
			rocks = 1;
		}

		int trees = 3 * Mathf.CeilToInt((float)_level / 3);
		Spawn(trees, rocks);
		_level++;
		_generating = false;
	}
	#endregion

	#region Coroutines
	private IEnumerator SpawnCoroutine(int trees, int rocks) {
		for (int i = 0; i < trees; i++) {
			SlashableTree tree = Instantiate(treePrefab, GetRandomVector(), Quaternion.identity, transform);
			yield return new WaitForSeconds(SPAWN_EFFECT_FREQUENCY);
		}

		for (int i = 0; i < rocks; i++) {
			Vector2 position;
			float radius = 10f;

			position = GetRandomVector();
			Debug.Log(GetRandomVector());
			Collider2D col = Physics2D.OverlapCircle(position, radius, _exceptionLayers);
			if (col != null) {
				Debug.Log("Replace rock");
				Vector2 replacementVec = center - (Vector2)col.transform.position;
				position = (Vector2)col.transform.position + replacementVec.normalized * radius;
			}
			Instantiate(rockPrefab, position, Quaternion.identity, transform);
			yield return new WaitForSeconds(SPAWN_EFFECT_FREQUENCY);
		}
	}
	#endregion
}
