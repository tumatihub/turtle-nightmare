using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

    public GameObject player;
    private PlayerStateController _controller;
    private SpriteRenderer _hookSprite;

    void Start()
    {
        _controller = player.GetComponent<PlayerStateController>();
        _hookSprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _controller.isRetracted = true;
            _hookSprite.enabled = false;

        }

        if (collision.gameObject.tag == "Hookable" || collision.gameObject.tag == "HookSupport")
        {
            Debug.Log(collision.gameObject.tag);
            _controller.isHooked = true;
            _controller.hookedObject = collision.gameObject.GetComponent<IHookable>();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            _controller.isHooked = false;
            _controller.isRetracting = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _controller.isRetracted = false;
            _hookSprite.enabled = true;
        }

        if (collision.gameObject.tag == "Hookable" || collision.gameObject.tag == "HookSupport")
        {
            _controller.isHooked = false;
            _controller.hookedObject = null;
        }
    }
}
