using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour {
	#region Properties
	public float _force = 1f;
	public AnimationCurve _moveCurve = new AnimationCurve();
	public AnimationCurve bounceCurve = new AnimationCurve();
	#endregion

	#region Getters
	public bool IsMoving => _currentChargeDuration > 0.0f;
	public Vector2 Direction => (_targetPosition - _originPos).normalized;
	#endregion

	#region Currents
	private float currentSpeed;
	[HideInInspector] public bool bounced = false;
	[HideInInspector] public float _currentChargeDuration = 0.0f;
	[HideInInspector] public float _distance = 0.0f;
	private Vector2 _targetPosition = Vector2.zero;
	private Vector2 _originPos = Vector2.zero;
	#endregion

	#region References
	[Header("Components")]
	public Rigidbody2D _rb = null;
	public CharacterCollisions characterCollisions;
	#endregion

	#region Movement
	public void StartMove(Vector2 direction, float distance) {
		_rb.AddForce(direction * distance * _force);
	}
	#endregion
}
