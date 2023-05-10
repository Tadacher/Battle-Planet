using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SfxService
{
    public AudioSource _source;
    SfxSetup _sfxSetup;

    public SfxService()
    {
    }

    [Inject]
    public void Construct(SfxSetup sfxSetup, LocationInstaller locationInstaller)
    {
        GameObject protosource = new GameObject("sfxSource");
        protosource.AddComponent<AudioSource>();
        protosource.transform.parent = locationInstaller.transform;
        _source = protosource.GetComponent<AudioSource>();
        _sfxSetup = sfxSetup;
    }

    public void PlaySucessBuySound() => _source.PlayOneShot(_sfxSetup.canBuy);
    public void PlayUnsucessBuySound() => _source.PlayOneShot(_sfxSetup.cantBuy);
    public void PlayPlayerShotSound(int id) => _source.PlayOneShot(_sfxSetup.shotAudios[id]);
    public void PlayPlayerCrushSound(int id) => _source.PlayOneShot(_sfxSetup.crushAudios[id]);
    public void PlayEnemyDeathSound() => _source.PlayOneShot(_sfxSetup.enemyDeathAiduos[Random.Range(0, _sfxSetup.enemyDeathAiduos.Length)]);
    public void PlayMissileLaunchSound() => _source.PlayOneShot(_sfxSetup.missileLaunch);
    public void PlayenemyCanoneerShotSound() => _source.PlayOneShot(_sfxSetup.enemyCanoneerShotSound);
    public void PlayLvlupSound() => _source.PlayOneShot(_sfxSetup.lvlup);
}
