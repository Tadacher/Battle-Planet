using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class UiService
{
    Text hpText, shieldText, fundsText, planetHpText, score, timeScore;
    GameObject gameMenu,  upgradeMenu, boughtGradesMenu, losescreen;
    RespawnTimer respawnTimer;
    Image shipHpImg,
          shipShieldImg,
          planetHpImg,
          fundsProgressImg;
    public GameObject ingameUi;

    public Transform gradesContent, boughtGradesContent;

    [Inject]
    public void Init (UiDependenciesContainer uiDep)
    {
        hpText = uiDep.hitpoints.GetComponent<Text>();
        shieldText = uiDep.shield.GetComponent<Text>();
        fundsText = uiDep.funds.GetComponent<Text>();
        planetHpText = uiDep.planetHpImg.GetComponent<Text>();
        score = uiDep.score.GetComponent<Text>();
        timeScore = uiDep.timescore.GetComponent<Text>();
        planetHpText = uiDep.planetHpText.GetComponent<Text>();

        gameMenu = uiDep.gameMenu;
        upgradeMenu = uiDep.upgradeMenu;
        boughtGradesMenu = uiDep.BoughtGrades;
        losescreen = uiDep.loseScreen;
        ingameUi = uiDep.ingameUi;
        gradesContent = uiDep.gradeMenuContent.transform;
        boughtGradesContent = uiDep.BoughtgradeMenuContent.transform;
        shipHpImg = uiDep.shipHpImg;
        shipShieldImg = uiDep.shipShieldImg;
        planetHpImg = uiDep.planetHpImg;
        fundsProgressImg = uiDep.fundsProgressImg;

        respawnTimer = uiDep.RespawnTime.GetComponent<RespawnTimer>();

       // RegisterUiButtonListeners(uiDep);
        Debug.Log("uiservice injected");
    }
    public UiService()
    {

    }

    private void RegisterUiButtonListeners(UiDependenciesContainer uiDep)
    {
        AddUiButtonListener(uiDep.callBoughtGradeBtn, TurnBoughtGradesMenu);
        AddUiButtonListener(uiDep.closeBoughtGradeBtn, TurnBoughtGradesMenu);
        AddUiButtonListener(uiDep.restartBtn, RestartScene);
    }

    private void AddUiButtonListener(Button button, UnityAction onclick) => button.onClick.AddListener(onclick);

    public void ToggleGameMenu()
    {
        if (!gameMenu.activeSelf) gameMenu.SetActive(true);
        else gameMenu.SetActive(false);
    }

    public void DrawHP(int hp, int maxhp)
    {
        Debug.Log("drawn");
       hpText.text = hp.ToString() + " hp";
       shipHpImg.fillAmount = (float)((float)hp / (float)maxhp);
        Debug.Log((float)hp / (float)maxhp);
    }

    public void DrawShield(int shield, int maxshield)
    {
       shieldText.text = shield.ToString() + " shield";
        if (maxshield > 0) shipShieldImg.fillAmount = shield / maxshield;
        else shipShieldImg.fillAmount = 0f;
    }
    public void DrawFunds(string fundsToDraw, float funds, float maxfunds)
    {
       fundsText.text = fundsToDraw;
       fundsProgressImg.fillAmount = funds / maxfunds;
    }
    public void DrawPlanetHp(int hp, int maxhp)
    {
        planetHpText.text ="Planet " + hp.ToString() + " hp";
        planetHpImg.fillAmount = (float)hp / (float)maxhp;
    }
    public void DrawFragScore(int scoreCount) => score.text = scoreCount + " kills";
    public void DrawTimeScore(float time) => timeScore.text = "Holded invasion for " + time + " sec";

    public void EnableShield()
    {
        shieldText.gameObject.SetActive(true);
    }

    public void TurnUpgradeMenu(bool switcher)
    {
        upgradeMenu.SetActive(switcher);
        TurnIngameUi(!switcher);
    }

   
    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void TurnBoughtGradesMenu()
    {
        if (!boughtGradesMenu.activeSelf)
        {
            boughtGradesMenu.SetActive(true);
            TurnIngameUi(false);
        }
        else
        {
            boughtGradesMenu.SetActive(false);
            TurnIngameUi(true);
        }     
    }
    public void TurnIngameUi(bool switcher) => ingameUi.SetActive(switcher);
    public void TurnGameOverMenu()
    {
        TurnIngameUi(false);
        losescreen.SetActive(true);
    }
    public void SetRespawnTimer(float time)
    {
        respawnTimer.gameObject.SetActive(true);
        respawnTimer.timeLeft = time;
    }

    public void MoveToBought(UpgradeBtnScript _gameObject) => _gameObject.transform.parent = boughtGradesContent;
}
