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
    UiService uiService;
    SfxService sfx;
    LocationInstaller locationInstaller;
    GameObject menuContent;
    //
    [Inject]
    void Construct(UiService uiContr, SfxService _sfx, UiDependenciesContainer uiDependenciesContainer, LocationInstaller _locationInstaller)
    {
        Debug.Log(uiContr);
        uiService = uiContr;
        sfx = _sfx;
        menuContent = uiDependenciesContainer.gradeMenuContent;
        locationInstaller = _locationInstaller;
    }
    void Update()
    {
        currentFunds += passiveIncome * Time.deltaTime;
        uiService.DrawFunds(string.Format("{0:0.00}", currentFunds), currentFunds, gradecost);
        if(currentFunds> gradecost && !isGrading)
        {
            currentFunds -= gradecost;
            isGrading = true;
            sfx.PlayLvlupSound();
            uiService.TurnUpgradeMenu(true);
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
        GameObject upgradeBase = locationInstaller.CreateUpgrade(upgrades[index], menuContent.transform);
    }

    public void ConstructRandomUpgrade()
    {
        ConstructUpgrade(Random.Range(0, upgrades.Length));
    }
}
