using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public AudioClip hoverSound;
    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        {
            Debug.LogError("Need an AudioSource!!");
        }
    }

    public void PlayOnHover()
    {
        _audio.PlayOneShot(hoverSound);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
