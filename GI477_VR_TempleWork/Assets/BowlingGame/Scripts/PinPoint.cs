using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject pin;
    private int score = 0;

    private void Start()
    {
        SetScore(1);
        PinFall();
    }

    public void SetScore(int score)
    {
        scoreText.text = $"Score {score}";

    }

    public void PinFall()
    {
        if (pin.transform.rotation.x !=0)
        {
            score += 1;
        }
    }
    public void PinSpawn()
    {
        
    }
}
