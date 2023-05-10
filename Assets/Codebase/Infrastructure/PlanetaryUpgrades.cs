using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Infrastructure;

public class PlanetaryUpgrades
{
    public float passiveIncome;
    public float currentFunds;
    public GameObject[] upgrades;
    public bool isGrading;

    [SerializeField] float gradecost;
    TickDelegate tickDelegate;
    //to inject
    UiService _uiService;
    SfxService _sfx;
    LocationInstaller _locationInstaller;
    GameObject _menuContent;
    TickableService _tickableService;
    //
    [Inject]
    void Construct(UiService uiService, SfxService sfx, UiDependenciesContainer uiDependenciesContainer, LocationInstaller locationInstaller, TickableService tickableService)
    {
        _uiService = uiService;
        _sfx = sfx;
        _menuContent = uiDependenciesContainer.gradeMenuContent;
        _locationInstaller = locationInstaller;
        tickDelegate += Tick;
        tickableService.AddToTick(tickDelegate);

    }
    void Tick()
    {
        currentFunds += passiveIncome * Time.deltaTime;
        _uiService.DrawFunds(string.Format("{0:0.00}", currentFunds), currentFunds, gradecost);
        if(currentFunds> gradecost && !isGrading)
        {
            currentFunds -= gradecost;
            isGrading = true;
            _sfx.PlayLvlupSound();
            _uiService.TurnUpgradeMenu(true);
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
        GameObject upgradeBase = _locationInstaller.CreateUpgrade(upgrades[index], _menuContent.transform);
    }

    public void ConstructRandomUpgrade() => ConstructUpgrade(Random.Range(0, upgrades.Length));
}
