using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AppearEffect : MonoBehaviour {
	public float scale = 0.6f;
	public float duration = 0.2f;
	public int vibrato = 5;

	private void Start() {
		transform.DOPunchScale(Vector3.one * scale, duration, vibrato);
	}
}
