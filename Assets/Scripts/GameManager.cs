using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static bool created = false;
    private AudioSource _audio;
    private int _activeScente;
    private int _nextScente;

    //Music
    public AudioClip mainMenuTheme;
    public AudioClip mainTheme;
    public AudioClip gameOverTheme;

    //SFX
    public AudioClip[] backgroundMonsterClips;
    public float minDelayMonsterBGSound;
    public float maxDelayMonsterBGSound;
    private float _nextDelayMonsterBGSound;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        {
            Debug.LogError("Need an AudioSource!!");
        }

        _nextDelayMonsterBGSound = Random.Range(minDelayMonsterBGSound, maxDelayMonsterBGSound);
    }

    void Update()
    {
        int _s = SceneManager.GetActiveScene().buildIndex;
        if (_s == 1 || _s == 2 || _s == 3)
        {
            PlayMonsterBGSounds();
        }  
    }



    public void UpdateBackgroundMusic(int _activeScene, int _nextScene)
    {
        if (_activeScene == 0)
        {
            StartCoroutine(ChangeMusic(mainTheme));
            return;
        }

        if (_nextScene == 4)
        {
            StartCoroutine(ChangeMusic(gameOverTheme));
            return;
        }

        if (_activeScene == 4)
        {
            _audio.Stop();
            return;
        }

    }

    IEnumerator ChangeMusic(AudioClip next)
    {
        _audio.Stop();
        _audio.clip = next;
        _audio.Play();
        yield return true;
    }

    void PlayMonsterBGSounds()
    {
        if (_nextDelayMonsterBGSound > 0)
        {
            _nextDelayMonsterBGSound -= Time.deltaTime;
        }
        else
        {
            _audio.PlayOneShot(backgroundMonsterClips[Random.Range(0, backgroundMonsterClips.Length)]);
            _nextDelayMonsterBGSound = Random.Range(minDelayMonsterBGSound, maxDelayMonsterBGSound);
        }
    }
}
