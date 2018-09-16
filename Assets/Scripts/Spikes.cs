using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public float hiddenTime;
    float _timer;
    Animator _anim;
    bool _isActive = false;
    public float delay;
    AudioSource _spikeSource;
    public AudioClip spikeSound;
    public float soundDelay = .5f;
    
	// Use this for initialization
	void Start () {
        _anim = GetComponent<Animator>();
        _timer = hiddenTime;
        _spikeSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_isActive)
        {
            if (_timer >= 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _anim.SetTrigger("Reveal");
                StartCoroutine(PlaySound());
                _timer = hiddenTime;
            }
        }
        else
        {
            if(Time.timeSinceLevelLoad > delay)
            {
                _isActive = true;
            }
        }
		
	}
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(soundDelay);
        _spikeSource.PlayOneShot(spikeSound);
    }
}
