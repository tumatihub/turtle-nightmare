using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;
    private int levelToLoad;
    private GameManager _gm;

    void Start()
    {
        _gm = GameObject.FindObjectOfType<GameManager>();
        if (_gm == null)
        {
            Debug.LogError("Need an GameManager!!");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void FadeToLevel(int levelIndex)
    {
        if(levelIndex == 0)
        {
            Cursor.visible = true;
        }
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
        _gm.UpdateBackgroundMusic(SceneManager.GetActiveScene().buildIndex, levelIndex);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void FadeToDeath()
    {
        animator.SetTrigger("Death");
    }
    public void Respawn()
    {
        animator.SetTrigger("Respawn");
    }
}
