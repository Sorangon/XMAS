using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterCollisions : MonoBehaviour
{
	#region Events
	public UnityAction OnHitRock;
	public UnityAction OnHitTree;
	public UnityAction OnHitBorder;
	#endregion

	#region References
	public CharacterMovements movements;
	#endregion

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Rock")) {
			movements.Stop();
			OnHitRock();
		}

		if (collision.gameObject.CompareTag("Tree")) {
			OnHitTree();
		}

		if (collision.gameObject.CompareTag("Border")) {
			OnHitBorder();
		}
	}
}
