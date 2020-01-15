using System;
using UnityEngine;

[Serializable]
public class PlayableMusic
{
    [SerializeField] private AudioClip introMusic = null;
    public AudioClip IntroMusic => introMusic;

    [SerializeField] private AudioClip loopMusic = null;
    public AudioClip LoopMusic => loopMusic;
}