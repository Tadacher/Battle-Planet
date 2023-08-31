using UnityEngine;

[CreateAssetMenu(fileName = "SfxData", menuName = "ScriptableObjects/SfxSetupScriptableObject", order = 2)]
public class SfxSetup : ScriptableObject
{
    public AudioClip
        canBuy,
        cantBuy,
        missileLaunch,
        enemyCanoneerShotSound,
        lvlup;
    public AudioClip[]
        shotAudios,
        crushAudios,
        enemyDeathAiduos;
}

