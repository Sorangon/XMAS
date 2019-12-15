using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScoreData", menuName = "Game/Player Score Data")]
public class PlayerScoreData : ScriptableObject
{
    public int playerScore = 0;

    public void Reset() {
        playerScore = 0;
    }

    public void IncrementScore() {
        playerScore++;
    }
}
