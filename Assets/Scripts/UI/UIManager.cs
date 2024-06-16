using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MyMonoBehaviour
{
    public Text ScoreText;
    public Text WinLoseText;
    private float _screenWidth;
    private float _screenHeight;
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        score = - 1;
        SetUILocation();
    }
    private void SetUILocation()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        RectTransform rectTransform = ScoreText.GetComponent<RectTransform>();
        if(rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector3(0, _screenHeight / 5 * 2, 0);
        }
    }

    public static void AddScore(int newScore)
    {
        LevelManager.Instance.PointsCount -= 0.5f;
        score += newScore;
    }

    private void UpdateScore()
    {
        ScoreText.text = "Score: " + score / 2;
    }

    private void UpdateGameProcess()
    {
        if(LevelManager.Instance.isGameEnded)
        {
            WinLoseText.text = "You Win!!! \n" + ScoreText.text;
            ScoreText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();  
        UpdateGameProcess(); 
    }
}
