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
    protected SoundEffectsService sfx;
    protected Button selfbutton;

    protected UiController uiController;
    protected PlanetaryUpgrades planetaryUpgrades;
    protected Shipcontroll shipcontroll;
    protected PlayerBehavoiur playerBehavoiur;
    protected PlanetHitpoints planetHitpoints;

    [Inject]
    void Construct(UiController _uicontroller, PlanetaryUpgrades _planetaryUpgrades, SoundEffectsService _sfx, Shipcontroll _shipcontroll, PlayerBehavoiur _playerBehavoiur, PlanetHitpoints _planetHitpoints)
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
