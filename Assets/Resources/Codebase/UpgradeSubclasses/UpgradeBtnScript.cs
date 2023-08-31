using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class UpgradeBtnScript : MonoBehaviour
{  
    [SerializeField]
    protected bool isActive;
    [SerializeField]
    protected string gradeName, description;
    protected GameObject gameController;
    protected SfxService sfx;
    protected Button selfbutton;

    protected UiService uiController;
    protected PlanetaryUpgradesService planetaryUpgrades;
    protected ShipBehaviour shipcontroll;
    protected ShipHitpointComponent playerBehavoiur;
    protected PlanetHitpoints planetHitpoints;

    [Inject]
    void Construct(UiService _uicontroller, PlanetaryUpgradesService _planetaryUpgrades, SfxService _sfx, ShipBehaviour _shipcontroll, ShipHitpointComponent _playerBehavoiur, PlanetHitpoints _planetHitpoints)
    {
        uiController = _uicontroller;
        planetaryUpgrades = _planetaryUpgrades;
        sfx = _sfx;
        shipcontroll = _shipcontroll;
        playerBehavoiur = _playerBehavoiur;
        planetHitpoints = _planetHitpoints;
    }

    protected abstract void Effect();
    /// <summary>
    ///  applies upgrade effect
    /// </summary>
    protected virtual void Apply()
    {
        planetaryUpgrades.isGrading = false;
        PlayBuySound(true);
        isActive = true;
        Effect();
        RemoveToBoughtList();
        CloseMenu();
    }

    private void PlayBuySound(bool sucess)
    {
        if (sucess) sfx.PlaySucessBuySound();
        else sfx.PlayUnsucessBuySound();
    }

    protected virtual void InitializeSelf()
    {
            transform.GetChild(0).GetComponent<Text>().text = this.gradeName;
            GetComponent<Button>().onClick.AddListener(Apply);
    }
    protected virtual void RemoveToBoughtList()
    {
        uiController.MoveToBought(this);
    }
    protected void CloseMenu()
    {
        uiController.TurnUpgradeMenu(false);
    }
}
