using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputs : MonoBehaviour
{
	#region Properties
	public float minDistance = 10f;
	public float maxDistance = 50f;
	public float distanceGrowTime = 1f;
	public AnimationCurve distanceGrowCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
	#endregion

	#region Currents
	private bool _loading = false;
	private float _chargeAmount = 0.0f;
	private float _distance = 0.0f;
	private Vector2 _direction = Vector2.zero;
	#endregion

	#region References
	public CharacterMovements movements;
	#endregion

	#region Callbacks
	private void Update() {
		if (!movements.IsMoving) {
			if (Input.touchCount > 0) {
				GetTouchDistance();
				if (Input.GetTouch(0).phase == TouchPhase.Began) {
					_loading = true;
				}
			}
			else {
				if (_loading == true) {
					Throw();
					_loading = false;
				}
			}
		}
    }

	private void GetTouchDistance() {
		_direction = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		_direction -= (Vector2)transform.position;
		_direction.Normalize();
		_direction = -_direction;

		_chargeAmount += Time.deltaTime / distanceGrowTime;
		_distance = Mathf.Lerp(minDistance, maxDistance, distanceGrowCurve.Evaluate(_chargeAmount));

		Debug.DrawRay(transform.position, _direction * _distance, Color.red);
	}

	private void Throw() {
		_chargeAmount = 0f;
		movements.StartMove(_direction.normalized, _distance);
		_direction = Vector2.zero;
	}

	#endregion
}
