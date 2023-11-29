using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vcl : MonoBehaviour
{
    float h;
    public float speed;
    Rigidbody2D rb;

    public float jumpForce;
    public Transform groundCheck;
    public LayerMask dat;
    public bool isGrounded;
    bool jump;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space)){
            jump = true;
        }
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.6f,0.07f),0,dat);
        flip();
        Vector2 truc = transform.position;
        truc.x = truc.x + h*3.0f*Time.deltaTime;
        transform.position = truc; 
    }
    private void FixedUpdate(){
       rb.velocity = new Vector2(h*speed, rb.velocity.y);
        if(jump){
            Jump();
        }
    }
    private void Jump(){
        if(isGrounded){
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            
        }
        jump = false;
    }
    private void flip(){
        if(h<-0.01f) transform.localScale = new Vector2(-1,1);
        if(h>-0.01f) transform.localScale = new Vector2(1,1);
    }
    
}
