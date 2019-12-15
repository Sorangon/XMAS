﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour {
	#region References
	public CharacterMovements movements;
	#endregion


	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Rock")) {
			movements.Stop();
		}
	}
}
