using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreControll : MonoBehaviour
{
    UiController uiController;
    EnemySpawner enemySpawner;
    bool gameIsOver;
    int frag;
    [Inject]
    void Construct(UiController _uiController)
    {
        uiController = _uiController;
    }
   public void PlusFrag()
    {
        frag++;
    }
    public void GameOver()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            uiController.DrawFragScore(frag);
            uiController.DrawTimeScore(Time.time);
            uiController.TurnGameOverMenu();
        }
    }
}
