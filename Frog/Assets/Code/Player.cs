using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float h;
    public float jumpHeight = 5000;
    public float gravityScale = 3.5f;
    public float fallGravityScale = 7;
    public float speed = 300;
    public float jumpForce;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool jump;
    public float buttonPressedTime;
    public float buttonPressWindow = 0.22f;
    public bool jumpCancelled;
    public float fallForce = 200;

    public Transform wallCheck;
    public LayerMask wallLayer;
    public bool isWallTouch;
    public bool isSliding;
    public float wallSpeed = 0.001f;
    public Vector2 wallForce = new Vector2(4,26);
    public bool wallJump;
    public bool isWallJump;

    public int jumpCount = 1;
    public bool dbJump;
    public float a = 300;
    public Animator animator;
    
    public float immortal;
    public float immortalTime;
    public bool immortalCheck;
    public float maxHeal;
    public float currentHeal;

    public AudioClip stepSound;
    private AudioSource audioSource;
    public AudioClip healSound;
    public AudioClip trapSound;
    public AudioClip jumpSound;
    public AudioClip groundedSound;
    public float StepCound;
    public float StepTime;
    public bool botCheck;
    public LayerMask botLayer;
    public float botForce;

    public Image heal;
    [SerializeField] GameObject looseMenu;
    [SerializeField] GameObject healBar;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHeal = maxHeal;
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.3f, 0.06f), 0, groundLayer);
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.03367274f, 0.4586498f), 0, wallLayer);
        botCheck = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.1296849f, 0.242228f), 0, botLayer);

        Animation();
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            Jump();
            audioSource.PlayOneShot(jumpSound);
        }
        if (Input.GetKeyUp(KeyCode.Space) && jump)
        {
            jumpCount++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount == 2)
        {
            Jump();
            audioSource.PlayOneShot(jumpSound);
        }
        if (jump)
        {
            buttonPressedTime += Time.deltaTime;
            if (buttonPressedTime < buttonPressWindow && Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }

            if (rb.velocity.y <= 0)
            {
                rb.gravityScale = fallGravityScale;
                jump = false;
            }
        }
        if (isGrounded && !isSliding)
        {
            jump = false;
            wallJump = false;
            isWallJump = false;
            jumpCount = 1;
            animator.SetFloat("Jump", 0);
        }
        else jump = true;
        if (!isGrounded && isWallTouch && h != 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        if (jump && wallJump && rb.velocity.y <= wallForce.y / 6)
        {
            wallJump = false;
            isWallJump = true;
        }
        if (isSliding)
        {
            jump = false;
            isWallJump = false;
        }
        if (immortalCheck)
        {
            immortal = immortal - Time.deltaTime;
            if (immortal < 0)
            {
                immortalCheck = false;
            }
        }
        if (botCheck)
        {
            jumpCancelled = true;
            rb.velocity = new Vector2(rb.velocity.x, botForce);
        }
        Flip();
        if (isWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 25 * Time.deltaTime);
        }
        if (currentHeal == 0)
        {
            Time.timeScale = 0;
            healBar.SetActive(false);
            looseMenu.SetActive(true);
        }
    }
    private void FixedUpdate()
    {   
        if (isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSpeed*Time.deltaTime, float.MaxValue));
            wallJump = true;
            jumpCancelled = true;
            isWallJump = false;
            rb.gravityScale = gravityScale;
            jumpCount = 4;
        }
        else if (!wallJump)
        {

            rb.velocity = new Vector2(h * speed * Time.deltaTime , rb.velocity.y);
        }
        if (jumpCancelled && jump && rb.velocity.y > 0 && !isSliding)
        {
            if (jumpCount == 3)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + a*Time.deltaTime);
                jumpCancelled = false;
            }
            rb.AddForce(Vector2.down * fallForce*Time.deltaTime);
        }
        
       
    }
    void Jump()
    {
        if (isSliding)
        {
            rb.velocity = new Vector2(-wallForce.x * h + 50 * Time.deltaTime, wallForce.y);
        }
        if ( jumpCount == 1 && !isSliding)
        {
            rb.gravityScale = gravityScale;
            jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(Vector2.up * (jumpForce+50*Time.deltaTime));
            //jump = true;
            buttonPressedTime = 0;
            jumpCancelled = false;
            
        }
        if ( jumpCount == 2 && !isSliding)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * (jumpForce+50*Time.deltaTime));
            jumpCount++;           
        }
    }
    void Flip()
    {
        if (h > 0.01f) rb.transform.localScale = new Vector3(1, 1, 1);
        if (h < -0.01f) rb.transform.localScale = new Vector3(-1, 1, 1);
    }
    void Animation()
    {
        if (jump)
            {
                animator.SetFloat("Jump", 1);
            }
        if (isSliding)
            {
                animator.SetFloat("Slide", 1);
            }
        else
            {
                animator.SetFloat("Slide", 0); 
            }
        if (h != 0 && isGrounded) animator.SetFloat("Speed", 1f);
        if (h == 0 && isGrounded) animator.SetFloat("Speed", 0);
        if (isGrounded)
            {
                animator.SetFloat("Jump", 0);
            }
        if (isGrounded && isWallTouch && isSliding)
            {
                animator.SetTrigger("RunSlide");
            }
    }
    public void changeHeal(int amount)
    {
        if (amount < 0)
        {
            if (immortalCheck) return;
            animator.SetTrigger("Hit");
            immortalCheck = true;
            immortal = immortalTime;
            audioSource.PlayOneShot(trapSound);

        }
        else audioSource.PlayOneShot(healSound);
        heal.fillAmount = heal.fillAmount + (float)amount / maxHeal;
        
        currentHeal = Mathf.Clamp(currentHeal + amount, 0, maxHeal);
        
        
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            ScreenController.instance.NextLevel();
        }
    }

}
