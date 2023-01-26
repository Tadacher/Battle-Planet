using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    public AudioSource source;
    [SerializeField]
    public AudioClip canBuy,cantBuy,missileLaunch, enemyCanoneerShotSound, lvlup;
    [SerializeField]
    public AudioClip[] shotAudios, crushAudios, enemyDeathAiduos;
    public void PlayBuySound (bool bought)
    {
        if (bought) source.PlayOneShot(canBuy);
        else source.PlayOneShot(cantBuy);
    }
    public void PlayPlayerShotSound(int id)
    {
        source.PlayOneShot(shotAudios[id]);
    }
    public void PlayPlayerCrushSound(int id)
    {
        source.PlayOneShot(crushAudios[id]);
    }
    public void PlayEnemyDeathSound()
    {
        source.PlayOneShot(enemyDeathAiduos[Random.Range(0, enemyDeathAiduos.Length)]);
    }
    public void PlayMissileLaunchSound()
    {
        source.PlayOneShot(missileLaunch);
    }
    public void PlayenemyCanoneerShotSound ()
    {
        source.PlayOneShot(enemyCanoneerShotSound);
    }
    public void PlayLvlupSound()
    {
        source.PlayOneShot(lvlup);
    }
}
