using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UiService
{
    public GameObject ingameUi;
    public Transform 
        gradesContent, 
        boughtGradesContent;

    private Text hpText, shieldText, fundsText, planetHpText, score, timeScore;
    private GameObject gameMenu,  upgradeMenu, boughtGradesMenu, losescreen;
    private RespawnTimer respawnTimer;
    private Image 
          shipHpImg,
          shipShieldImg,
          planetHpImg,
          fundsProgressImg;
   
    public UiService(UiDependenciesContainer uiDep)
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
    
    public void ToggleGameMenu()
    {
        if (!gameMenu.activeSelf) 
            gameMenu.SetActive(true);
        else 
            gameMenu.SetActive(false);
    }

   
    public void DrawHitpoints(int hp, int maxhp)
    {
       hpText.text = hp.ToString() + " hp";
       shipHpImg.fillAmount = (float)((float)hp / (float)maxhp);
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

    public void EnableShieldUi() => shieldText.gameObject.SetActive(true);

    public void TurnUpgradeMenu(bool state)
    {
        upgradeMenu.SetActive(state);
        TurnIngameUi(!state);
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
        respawnTimer._timeLeft = time;
    }

    public void MoveToBought(UpgradeBtnScript _gameObject) => _gameObject.transform.parent = boughtGradesContent;



    private void RegisterUiButtonListeners(UiDependenciesContainer uiDep)
    {
        AddUiButtonListener(uiDep.callBoughtGradeBtn, TurnBoughtGradesMenu);
        AddUiButtonListener(uiDep.closeBoughtGradeBtn, TurnBoughtGradesMenu);
        AddUiButtonListener(uiDep.restartBtn, RestartScene);
    }

    private void AddUiButtonListener(Button button, UnityAction onclick) => button.onClick.AddListener(onclick);



}
