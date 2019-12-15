using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour {
	#region Properties
	public float speed = 1f;
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
	#endregion

	#region Callbacks
	private void FixedUpdate() {
		if(_currentChargeDuration > 0) {
			Move();
		}
	}
	#endregion

	#region Movement
	public void StartMove(Vector2 direction, float distance) {
		_currentChargeDuration = 1.0f;
		_targetPosition = (Vector2)transform.position + (direction * distance);
		_originPos = transform.position;
		//Debug.DrawLine(_targetPosition - Vector2.one * 0.4f, _targetPosition + Vector2.one * 0.4f, Color.green, 0.5f);
		Debug.DrawLine(_originPos, _targetPosition, Color.green, 0.5f);
		currentSpeed = speed;
	}

	private void Move() {
		//Vector2 movement = currentDirection * speed * Time.fixedDeltaTime;
		AnimationCurve curve = bounced ? bounceCurve : _moveCurve;
		float t = curve.Evaluate(1 - _currentChargeDuration);
		Vector2 newPos = Vector2.Lerp(_originPos, _targetPosition, t);
		_rb.MovePosition(newPos);
		_currentChargeDuration -= Time.fixedDeltaTime / currentSpeed; //Decrement timer
	}

	public void Bounce(Vector2 bounceNormal) {
		//currentDirection = Vector2.Reflect(currentDirection, bounceNormal);
		
		float baseDistanceAmount = (_targetPosition - _originPos).magnitude;
		float distanceAmount = (_targetPosition - (Vector2)transform.position).magnitude / baseDistanceAmount;
		Debug.Log(distanceAmount);

		_targetPosition = (Vector2)transform.position + Vector2.Reflect(Direction, bounceNormal);
		_targetPosition *= 1 - distanceAmount;
		//_originPos = ((Vector2)transform.position - _targetPosition).normalized;
		_originPos = transform.position;
		//_originPos = (Vector2)transform.position;

		Debug.DrawLine(_originPos, _targetPosition, Color.blue, 1f);
		currentSpeed = speed / distanceAmount;
		bounced = true;
		_currentChargeDuration = 1.0f;
	}

	public void Stop() {
		_currentChargeDuration = 0f;
		bounced = false;
		Debug.Log("Stop");
	}
	#endregion
}
