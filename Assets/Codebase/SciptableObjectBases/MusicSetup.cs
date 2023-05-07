using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SfxData", menuName = "ScriptableObjects/SfxSetupScriptableObject", order = 2)]
public class MusicSetup : ScriptableObject
{
    public AudioClip[] _musicClips;
}

