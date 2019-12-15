using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public PlayerScoreData playerScoreData;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = playerScoreData.playerScore.ToString();
    }
}
