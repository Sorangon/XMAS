using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	#region Settings
	public Vector2 center = Vector2.zero;
	public Vector2 size = Vector2.one;
	#endregion

	#region References
	public SlashableTree treePrefab = null;
	public Rock rockPrefab = null;
	#endregion

	#region Current
	private int _remaining = 0;
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
		for(int i = 0; i < trees; i++) {
			SlashableTree tree = Instantiate(treePrefab, GetRandomVector() , Quaternion.identity, transform);
			_remaining++;
		}

		for(int i = 0; i < rocks; i++) {
			Instantiate(rockPrefab, GetRandomVector(), Quaternion.identity, transform);
		}
	}
	#endregion

	private Vector2 GetRandomVector() {
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
	}
	#endregion
}
