using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/MusicSetupScriptableObject", order = 2)]
public class MusicSetup : ScriptableObject
{
    public AudioClip[] _musicClips;
}

