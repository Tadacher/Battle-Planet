using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraseRespawnTime : UpgradeBtnScript
{
    [SerializeField]
    float  minVal =1f, maxVal=3f;
    float decreaseTime;

    private void Start()
    {       
        decreaseTime = Random.Range(minVal, maxVal);
        gradeName = "Upgrade planetary infrastructure (Decrease respawn time by " + decreaseTime + " sec";
        InitializeSelf();
    }
    protected override void Effect()
    {
        shipcontroll.respawnTime -= decreaseTime;
    }
}
