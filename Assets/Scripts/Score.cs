using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    void Update()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        score.text = "Score:" + PlayerStats.Score;
    }
}
