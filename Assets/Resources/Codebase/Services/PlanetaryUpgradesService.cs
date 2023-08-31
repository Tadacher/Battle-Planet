using UnityEngine;
using Infrastructure;

public class PlanetaryUpgradesService
{

    public float passiveIncome;
    public float currentFunds;
    public GameObject[] upgrades;
    public bool isGrading;

    private float gradecost;

    private UiService _uiService;
    private SfxService _sfx;
    private LocationInstaller _locationInstaller;
    private GameObject _menuContent;

    public PlanetaryUpgradesService(UiService uiService, SfxService sfx, UiDependenciesContainer uiDependenciesContainer, LocationInstaller locationInstaller, TickableService tickableService)
    {
        _uiService = uiService;
        _sfx = sfx;
        _menuContent = uiDependenciesContainer.gradeMenuContent;
        _locationInstaller = locationInstaller;
        tickableService.AddToTick(Tick);

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
    /// constructs upgrade GameObject attached to upgrade menu
    /// </summary>
    public void ConstructUpgrade(int index)
    {
        GameObject upgradeBase = _locationInstaller.CreateUpgrade(upgrades[index], _menuContent.transform);
    }

    public void ConstructRandomUpgrade() => ConstructUpgrade(Random.Range(0, upgrades.Length));
}
