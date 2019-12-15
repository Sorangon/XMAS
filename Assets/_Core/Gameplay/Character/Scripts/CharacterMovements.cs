using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour {
	#region Properties
	public float _force = 1f;
	public Vector3 _rotationAxis;
	#endregion

	#region Getters
	public Vector2 Direction => (_rb.velocity).normalized;
	#endregion


	#region References
	[Header("Components")]
	public Rigidbody2D _rb = null;
	public Transform _characterRenderer = null;
	#endregion

	private void Update() {
		if(_rb.velocity.magnitude > 2f) {
			CharacterLookAt(_rb.velocity);
		}
	}

	#region Movement
	public void Impulse(Vector2 direction, float distance) {
		_rb.AddForce(direction * distance * _force);
	}

	public void Stop() {
		_rb.velocity = Vector2.zero;
		GameManager.gameManager.Lose();
	}
	#endregion

	#region Rendering
	public void CharacterLookAt(Vector2 direction) {
		direction.Normalize();
		float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		_characterRenderer.transform.rotation = Quaternion.AngleAxis(angle, _rotationAxis.normalized);
	}
	#endregion

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, _rotationAxis);
	}
}
