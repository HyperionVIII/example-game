using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Movement
    [Header("Movement")]
    public float speed;
    public float jumpForce;
    private float moveInput;
    [HideInInspector]
    public bool facingRight = true;

    //Ground check
    [Header("Ground check")]
    public Vector2 groundCheckSize;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded;

    //Shooting
    [Header("Shooting")]
    public float startTimeBtwShots;
    public Transform shootPos;
    public GameObject projectile;
    private float timeBtwShots;
    private UnityEngine.Object shotExplosion;

    //Components
    private Rigidbody2D rb;
    private Animator anim;
    private SmoothCamera smoothCamera;

    private UnityEngine.Object playerParticles;
    private bool particlesSpawned = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        smoothCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothCamera>();
        timeBtwShots = startTimeBtwShots;
        shotExplosion = Resources.Load("ShotExplosion");
        playerParticles = Resources.Load("PlayerDestroy");
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (!facingRight && moveInput > 0) Flip();
        else if (facingRight && moveInput < 0) Flip();
    }

    private void Update()
    {
        //Jumping
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOff");
        }

        if(moveInput != 0) anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);

        if(!isGrounded) anim.SetBool("isJumping", true);
        else anim.SetBool("isJumping", false);

        //Shooting
        if(Input.GetKey(KeyCode.Q) && timeBtwShots <= 0)
        {
            Shoot();
            timeBtwShots = startTimeBtwShots;
        }
        else if(timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(projectile, shootPos.position, Quaternion.identity);
        Instantiate(shotExplosion, shootPos);
        smoothCamera.Shake();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Damage") && !particlesSpawned) Death();

        if(col.CompareTag("Heal") || col.CompareTag("Coin")) Destroy(col.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy" && !particlesSpawned) Death();
    }

    public void Death()
    {
        gameObject.SetActive(false);
        Instantiate(playerParticles, transform.position, Quaternion.identity);
        particlesSpawned = true;
        Invoke("RestartScene", 2);
    }

    private void Flip()
    {
       facingRight = !facingRight;
       Vector3 Scaler = transform.localScale;
       Scaler.x *= -1;
       transform.localScale = Scaler;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}