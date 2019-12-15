using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour {
	#region Properties
	public float _force = 1f;
	#endregion

	#region Getters
	public Vector2 Direction => (_rb.velocity).normalized;
	#endregion

	#region Currents
	#endregion

	#region References
	[Header("Components")]
	public Rigidbody2D _rb = null;
	#endregion

	#region Movement
	public void Impulse(Vector2 direction, float distance) {
		_rb.AddForce(direction * distance * _force);
	}
	#endregion
}
