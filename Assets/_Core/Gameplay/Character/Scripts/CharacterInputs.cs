using UnityEngine;
using UnityEngine.Events;

public class CharacterInputs : MonoBehaviour
{
	#region Events
	public UnityAction OnThrow;
	public UnityAction OnAimThrow;
	#endregion

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
	private bool _lock = false;
	#endregion

	#region References
	public CharacterMovements movements;
	public GameObject aimLogo = null;
	#endregion

	#region Callbacks

	private void Start() {
		aimLogo.SetActive(false);		
	}

	private void Update() {
		if (_lock) return;
#if UNITY_EDITOR
		CheckMouseInput();
#endif
		CheckTouchInput();
	}

#if UNITY_EDITOR
	private bool CheckMouseInput() {
		if(Input.GetMouseButtonDown(0)) {
			aimLogo.SetActive(true);
		}
		if(Input.GetMouseButtonUp(0)) {
			Throw();
			aimLogo.SetActive(false);
		}

		if (Input.GetMouseButton(0)) {
			GetTouchDistance(Input.mousePosition);
		}
		return false;
	}
#endif

	private void CheckTouchInput() {
		if (Input.touchCount > 0) {
			GetTouchDistance(Input.GetTouch(0).position);
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				_loading = true;
				aimLogo.SetActive(true);
				
				OnAimThrow();
			}
		}
		else if (_loading == true) {
			Throw();
			_loading = false;
			aimLogo.SetActive(false);

			OnThrow();
		}
	}

	private void GetTouchDistance(Vector2 cursorDirection) {
		_direction = Camera.main.ScreenToWorldPoint(cursorDirection);
		_direction -= (Vector2)transform.position;
		_direction.Normalize();
		_chargeAmount += Time.deltaTime / distanceGrowTime;
		_distance = Mathf.Lerp(minDistance, maxDistance, distanceGrowCurve.Evaluate(_chargeAmount));

		aimLogo.transform.localScale = new Vector3(1f,0f,1f) + Vector3.down * _distance;
		float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + 90f;
		aimLogo.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if(movements._rb.velocity.magnitude <= 2f) {
			movements.CharacterLookAt(_direction);
		}

		Debug.DrawRay(transform.position, _direction * _distance, Color.red);
	}

	private void Throw() {
		_chargeAmount = 0f;
		movements.Impulse(_direction.normalized, _distance);
		_direction = Vector2.zero;
	}

	public void Lock() {
		_lock = true;
	}

	#endregion
}
