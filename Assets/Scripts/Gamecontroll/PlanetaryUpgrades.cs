using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Infrastructure;

public class PlanetaryUpgrades : MonoBehaviour
{
    public float passiveIncome;
    public float currentFunds;
    public GameObject[] upgrades;
    public bool isGrading;

    [SerializeField]
    float gradecost;

    //to inject
    UiController uiController;
    SfxController sfx;
    LocationInstaller locationInstaller;
    GameObject menuContent;
    //
    [Inject]
    void Construct(UiController uiContr, SfxController _sfx, UiDependenciesContainer uiDependenciesContainer, LocationInstaller _locationInstaller)
    {
        uiController = uiContr;
        sfx = _sfx;
        menuContent = uiDependenciesContainer.gradeMenuContent;
        locationInstaller = _locationInstaller;
    }
    void Update()
    {
        currentFunds += passiveIncome * Time.deltaTime;
        uiController.DrawFunds(string.Format("{0:0.00}", currentFunds), currentFunds, gradecost);
        if(currentFunds> gradecost && !isGrading)
        {
            currentFunds -= gradecost;
            isGrading = true;
            sfx.PlayLvlupSound();
            uiController.TurnUpgradeMenu(true);
            ConstructRandomUpgrade();
            ConstructRandomUpgrade();
            ConstructRandomUpgrade();
        }
    }
    /// <summary>
    /// constructs upgrade GO attached to upgrade menu
    /// </summary>
    public void ConstructUpgrade(int index)
    {
        GameObject upgradeBase = locationInstaller.UpgradeFactory(upgrades[index], menuContent.transform);
    }

    public void ConstructRandomUpgrade()
    {
        ConstructUpgrade(Random.Range(0, upgrades.Length));
    }
}
