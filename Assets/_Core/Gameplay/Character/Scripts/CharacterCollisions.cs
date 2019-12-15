using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour {
	#region References
	public float _rayLength = 0.5f;
	public CharacterMovements movements;
	#endregion

	private void FixedUpdate() {
		if (movements.IsMoving) {
			CheckDirection();
		}
	}

	private void CheckDirection() {
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, movements.Direction, 20f);
		if(hit.distance <= _rayLength) {
			movements.Bounce(hit.normal);
		}
	}


	private void OnTriggerEnter2D(Collider2D collision) {
		
	}
}
