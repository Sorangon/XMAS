using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScoreData", menuName = "Game/Player Score Data")]
public class PlayerScoreData : ScriptableObject
{
    public int playerScore = 0;
    public int playerMaxScore = 100;

    public void Reset() {
        playerScore = 0;
    }

    public void IncrementScore() {
        playerScore++;

        if(playerScore > playerMaxScore) {
            playerMaxScore = playerScore;
        }
    }
}
