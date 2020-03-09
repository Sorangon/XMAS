using UnityEngine;

public class EndMenu : MonoBehaviour {
	#region Call Restart
	public void CallTryAgain() {
		GameManager.Instance.TryAgain();
	}
	#endregion
}
