using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour {

    public AudioClip toggleOnClip;
    public AudioClip toggleOffClip;
    public AudioClip startGameClip;
    public AudioClip enterClip;
    public AudioClip backClip;



    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }

    public void PlayToggleOn()
    {
        source.clip = toggleOnClip;
        source.Play();
    }
    public void PlayToggleOff()
    {
        source.clip = toggleOnClip;
        source.Play();
    }

    public void PlayStartGame()
    {
        source.clip = startGameClip;
        source.Play();
    }

    public void PlayEnter()
    {
        source.clip = enterClip;
        source.Play();
    }
    public void PlayBack()
    {
        source.clip = backClip;
        source.Play();
    }
}
