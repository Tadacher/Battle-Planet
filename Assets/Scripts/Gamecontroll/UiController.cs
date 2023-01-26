using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.SceneManagement;


public class UiController : MonoBehaviour
{
    Text hpText, shieldText, fundsText, planetHpText, score, timeScore;
    GameObject gameMenu,  upgradeMenu, boughtGradesMenu, restartBtn, losescreen;
    RespawnTimer respawnTimer;
    Image shipHpImg,
          shipShieldImg,
          planetHpImg,
          fundsProgressImg;
    public GameObject ingameUi;

    public Transform gradesContent, boughtGradesContent;
  
    [Inject]
    void Construct(UiDependenciesContainer _uiDependenciesContainer)
    {
       Init(_uiDependenciesContainer);
    }
    void Init(UiDependenciesContainer uiDep)
    {
        hpText = uiDep.hitpoints.GetComponent<Text>();
        shieldText = uiDep.shield.GetComponent<Text>();
        fundsText = uiDep.funds.GetComponent<Text>();
        planetHpText = uiDep.planetHpImg.GetComponent<Text>();
        score = uiDep.score.GetComponent<Text>();
        timeScore = uiDep.timescore.GetComponent<Text>();
        planetHpText = uiDep.planetHpText.GetComponent<Text>();

        gameMenu = uiDep.gameMenu;
        restartBtn = uiDep.restartBtn;
        upgradeMenu = uiDep.upgradeMenu;
        boughtGradesMenu = uiDep.BoughtGrades;
        losescreen = uiDep.loseScreen;
        ingameUi = uiDep.ingameUi;
        gradesContent = uiDep.gradeMenuContent.transform;
        boughtGradesContent = uiDep.BoughtgradeMenuContent.transform;
        this.shipHpImg = uiDep.shipHpImg;
        shipShieldImg = uiDep.shipShieldImg;
        planetHpImg = uiDep.planetHpImg;
        fundsProgressImg = uiDep.fundsProgressImg;

        respawnTimer = uiDep.RespawnTime.GetComponent<RespawnTimer>();

        uiDep.callBoughtGradeBtn.GetComponent<Button>().onClick.AddListener(TurnBoughtGradesMenu);
        uiDep.closeBoughtGradeBtn.GetComponent<Button>().onClick.AddListener(TurnBoughtGradesMenu);
        uiDep.restartBtn.GetComponent<Button>().onClick.AddListener(RestartScene);

        
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameMenu.activeSelf) gameMenu.SetActive(true);
            else gameMenu.SetActive(false);
        }
    }
    public void DrawHP(int hp, int maxhp)
    {
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
    public void DrawFragScore(int scoreCount)
    {
        score.text = scoreCount + " kills";
    }
    public void DrawTimeScore(float time)
    {
        timeScore.text = "Holded invasion for " + time + " sec";
    }

    public void EnableShield()
    {
        shieldText.gameObject.SetActive(true);
    }

    public void TurnUpgradeMenu(bool switcher)
    {
        upgradeMenu.SetActive(switcher);
        TurnGameTime(switcher);
        TurnIngameUi(!switcher);
    }
    /// <summary>
    /// Sets timescale to 0 if swithcer param is true, to 1 if not
    /// </summary>
    public void TurnGameTime(bool switcher)
    {
        if (switcher) StopGame();
        else StartGame();
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TurnBoughtGradesMenu()
    {
        if (!boughtGradesMenu.activeSelf)
        {
            boughtGradesMenu.SetActive(true);
            TurnGameTime(true);
            TurnIngameUi(false);
        }
        else
        {
            boughtGradesMenu.SetActive(false);
            TurnGameTime(false);
            TurnIngameUi(true);
        }     
    }
    public void TurnIngameUi(bool switcher)
    {
        ingameUi.SetActive(switcher);
    }
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

    public void MoveToBought(UpgradeBtnScript _gameObject)
    {
        _gameObject.transform.parent = boughtGradesContent;
    }
}
