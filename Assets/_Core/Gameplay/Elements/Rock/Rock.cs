using UnityEngine;
using UnityEngine.Events;

public class Rock : MonoBehaviour {
	public UnityEvent OnRockSlashed;

	private void OnCollisionEnter2D(Collision2D other) {
		OnRockSlashed.Invoke();
		GameManager.Instance.Lose();
	}
}
