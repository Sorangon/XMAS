using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashableTree : MonoBehaviour {
	#region Events
	public delegate void TreeAction();
	public static TreeAction OnCutTree;
	#endregion

	#region Cut
	public void Cut() {
		OnCutTree?.Invoke();
	}
	#endregion
}
