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

	#region Currents
	#endregion

	#region References
	[Header("Components")]
	public Rigidbody2D _rb = null;
	public Transform _characterRenderer = null;
	#endregion

	private void Update() {
		float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg + 90f;
		_characterRenderer.transform.rotation = Quaternion.AngleAxis(angle, _rotationAxis.normalized);
	}

	#region Movement
	public void Impulse(Vector2 direction, float distance) {
		_rb.AddForce(direction * distance * _force);
	}
	#endregion

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, _rotationAxis);
	}
}
