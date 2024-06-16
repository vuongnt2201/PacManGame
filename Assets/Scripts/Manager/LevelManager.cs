using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public float PointsCount {get; set;}
    public bool isGameEnded {get; private set;}
    private void Start()
    {
        PacMan.Instance.UpdatePacManMoving(true);
        isGameEnded = false;
    }

    private void Update()
    {
        if(PointsCount < 1.0f)
        {
            PacMan.Instance.UpdatePacManMoving(false);
            isGameEnded = true;
        }
    }
}
