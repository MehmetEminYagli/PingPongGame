using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Canvas canvas;
    public TextMeshProUGUI textStart;
    public TextMeshProUGUI TextTryagain;
    public TextMeshProUGUI textplayerskor;
    public TextMeshProUGUI textAýSkor;
    

    public int oyuncuskoru 
    { 
        get => playerskor;
            set 
            {
            playerskor = value;
            textplayerskor.text = value.ToString();
                
            } 
    }
    private int playerskor;
    public int AIScore 
    {
        get => AIskoru;
        set
        {
            AIskoru = value;
            textAýSkor.text = value.ToString();
            
        } 
    }
    private int AIskoru;

    private void Awake()
    {
        Instance = this;
    }

    public void Onclik_StartButton()
    {
        canvas.enabled = false;
        BallController.Instance.OnStart();
    }

    public void OnGameover()
    {
        canvas.enabled = true;
        textStart.enabled = false;
        TextTryagain.enabled = true;
    }
}
