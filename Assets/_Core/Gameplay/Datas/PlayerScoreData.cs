using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScoreData", menuName = "Game/Player Score Data")]
public class PlayerScoreData : ScriptableObject {
	#region Data
	public int baseMaxScore = 50;

	private int playerScore = 0;
	private int playerMaxScore = 100;
	#endregion

	#region Constant
	private const string SCORE_KEY = "MaxScore";
	#endregion

	#region Properties
	public int PlayerScore => playerScore;
	public int PlayerMaxScore => playerMaxScore;
	#endregion

	#region Callbacks
	public void Reset() {
		playerScore = 0;
	}
	#endregion

	#region Score Management
	public void IncrementScore() {
		playerScore++;

		if (playerScore > playerMaxScore) {
			playerMaxScore = playerScore;
		}
	}
	#endregion

	#region Save Load
	public void LoadScore() {
		int maxScore = PlayerPrefs.GetInt(SCORE_KEY);
		playerMaxScore = maxScore > baseMaxScore ? maxScore : baseMaxScore;
	}

	public void SaveScore() {
		PlayerPrefs.SetInt(SCORE_KEY, playerMaxScore);
	}
	#endregion
}
