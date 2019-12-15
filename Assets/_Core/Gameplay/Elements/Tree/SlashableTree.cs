using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlashableTree : MonoBehaviour {

	public PlayerScoreData playerScoreData;
	public Animator animator;
	
	#region Events
	public delegate void TreeAction();
	public static TreeAction OnCutTree;

	public UnityEvent OnTreeSlashed;
	#endregion

	#region Cut
	public void Cut() {
	}
	#endregion

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Start Slash animation
		animator.SetTrigger("Slash");

		// Disable collider
		GetComponent<CircleCollider2D>().enabled = false;

		// Destroy after 2.3 seconds
		Destroy(gameObject, 2.3f);

		OnCutTree?.Invoke();

		// Increment score
		playerScoreData.IncrementScore();

		// Launch UnityAction OnTreeSlashed
		OnTreeSlashed.Invoke();
	}
}
