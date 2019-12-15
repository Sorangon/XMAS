using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public PlayerScoreData playerScoreData;

    private void Awake()
    {
        playerScoreData.Reset();
    }
}
