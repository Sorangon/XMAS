using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {
	#region References
	public PlayerScoreData playerScoreData;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI maxScoreText;

	public bool _diplayScoreOnly = false;
	#endregion

	#region Callbacks
	private void Update() {
		OnUpdateScore();		
	}
	#endregion

	#region Update Score
	private void OnUpdateScore() {
		scoreText.text = playerScoreData.playerScore.ToString();
		if (!_diplayScoreOnly) {
			maxScoreText.text = playerScoreData.playerMaxScore.ToString();
		}
	}
	#endregion
}
