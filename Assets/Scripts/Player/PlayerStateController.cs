using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {

    // Public
    [HideInInspector] public float moveInput;
    [HideInInspector] public Rigidbody2D rb;
    public float speed;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool isGrounded;
    public Transform feetPos;
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpTime;
    public Transform hook;
    [HideInInspector] public LineRenderer hookLine;
    [HideInInspector] public Vector2 whereToShoot;
    public Camera cam;
    public Animator animator;
    public float fallingSpeed;
    [HideInInspector] public GameObject lifeController;
    [HideInInspector] public LifeController life;
    public Transform target;
    public bool isHidden = false;
    public Transform wallHole;
    [HideInInspector] public SpriteRenderer turtleSprite;
    public bool dead = false;
    public AudioSource audioSource;

    // Private
    private GameObject fadeLevel;
    private LevelChanger levelScript;
    
    // Hook
    public float speedHook;
    public bool isHooked;
    public bool isRetracting;
    public bool isRetracted = true;
    public IHookable hookedObject;
    public float hookMaxDistance;
    public float speedHookPlayer;
    public float hookInertia;
    public float hookTargetDistance;

    // Shadow
    public bool isInShadow;


    // States
    [SerializeField] private State _state;
    public State movingState;
    public State jumpingState;
    public State shootingState;
    public State hookedState;
    public State hidingState;
    public State retractingState;
    public State pullingState;
    public State fallingState;
    public State deadState;

    public float scalex;
    public float scaley;

    //SFX
    public AudioClip jumpSound;
    public AudioClip landingSound;
    public AudioClip hookSound;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _state = movingState;
        hookLine = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        turtleSprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Missing AudioSource");
        }
    }

    // Use this for initialization
    void Start () {
        lifeController = GameObject.Find("LifeController");
        life = lifeController.GetComponent<LifeController>();
        life.initialPosition = transform.position;
        life.currentPosition = transform.position;
        fadeLevel = GameObject.Find("FadeLevel");
        levelScript = fadeLevel.GetComponent<LevelChanger>();

        Cursor.visible = false;

    }
	
	// Update is called once per frame
	void Update () {
        
        _state.handle_input(this);
	}

    private void FixedUpdate()
    {
        _state.update(this);
        UpdateAnimator();
        UpdateHookTarget();
    }

    public void Flip()
    {
        if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        
    }

    void UpdateHookTarget()
    {
        target.parent.transform.position = transform.position;

        Vector2 mousePos = new Vector2(
                    cam.ScreenToWorldPoint(Input.mousePosition).x,
                    cam.ScreenToWorldPoint(Input.mousePosition).y
                );
        Vector3 diff = (new Vector3(mousePos.x, mousePos.y, 0) - target.parent.transform.position).normalized;
        target.localPosition = diff * hookTargetDistance;
        
    }

    void UpdateAnimator()
    {
        animator.SetBool("Ground", isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("vSpeed", rb.velocity.y);
    }

    public void ChangeState(State nextState)
    {
        StartCoroutine(ChangeStateCoroutine(nextState));
    }

    private IEnumerator ChangeStateCoroutine(State nextState)
    {
        yield return new WaitForEndOfFrame();
        _state.onExit(this);
        _state = nextState;
        _state.onEnter(this);
    }

    public Vector2 GetWhereToShoot()
    {
        Vector2 mousePos = new Vector2(
                    cam.ScreenToWorldPoint(Input.mousePosition).x,
                    cam.ScreenToWorldPoint(Input.mousePosition).y
                );
        return (new Vector3(mousePos.x, mousePos.y, 0) - hook.position).normalized;
    }

    public void Damaged(int damage)
    {
        dead = true;
        Debug.Log("Damage");
        life.OnDamage(damage);
        ChangeState(deadState);
        levelScript.FadeToDeath();
        
        
        
    }

    public void OnCheckpoint(Vector2 checkpoint)
    {
        life.currentPosition = checkpoint;
    }

    public void Respawn()
    {
        print("teste");
       if (life.health > 0)
        {
            transform.position = life.currentPosition;
            levelScript.Respawn();
        }
        else
        {
            
            life.currentPosition = life.initialPosition;
            life.GameOver();
        }
        animator.SetBool("Dead", false);
        ChangeState(movingState);
        dead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shadow"))
        {
            wallHole = collision.transform;
            isInShadow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shadow"))
        {
            wallHole = null;
            isInShadow = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
