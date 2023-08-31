using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDependenciesContainer : MonoBehaviour
{
    public GameObject
        funds,
        upgradeMenu,
        hitpoints,
        shield,
        gameMenu,
        BoughtGrades,
        ingameUi,
        RespawnTime,
        gradeMenuContent,
        BoughtgradeMenuContent,
        
        planetHpText,
        loseScreen,
        score,
        timescore;

    public Button
        callBoughtGradeBtn,
        closeBoughtGradeBtn,
        restartBtn;



    public Image
        shipHpImg,
        shipShieldImg,
        planetHpImg,
        fundsProgressImg;

}
