using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    Rigidbody2D vatly;
    Animator dichuyen;
    [Header("animation")]
    public float animationjump = 2;

    [Header("Check")]
    
    public bool IsGrounded = true;
    public bool IsJumping;
    public bool IsWallJumping;
    public bool IsWalling;
    public bool wallleft;
    public bool wallright;
    public bool IsSliding;
    public bool IsWallTouch;
   // public bool wallup;
    public int lookDirection;
    public Transform wallCheck;
    [Header("Khac")]
    public float LucHut = 1.0f;
    public float ox;
    public float timejump = 3;
    public float dem =0;
    public float db;
    public float wallJumpTime = 0.5f;
    public float jumpTime;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    
    void Start()
    {
        vatly = GetComponent<Rigidbody2D>();
        dichuyen = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {         
        move();       
        bamtuong();   
        if (Input.GetKeyDown(KeyCode.W)){            
            Jump();         
        }
        hoatanh();
        dem -=Time.deltaTime; 
        walljum();
    }
    
    private void hoatanh(){
        dichuyen.SetFloat("Look x",lookDirection);
        //if(wallright)dichuyen.SetFloat("Look y",-1);
        //else if(wallleft) dichuyen.SetFloat("Look y",1);
        if(animationjump==0) {
            if(!IsJumping) dichuyen.SetFloat("Jump",0);
            dichuyen.SetFloat("db",0);
            if(!IsJumping) dichuyen.SetFloat("Speed",Mathf.Abs(ox));
        }
        if(!IsWalling){
            if(animationjump==1) dichuyen.SetFloat("Jump",1);
            if(animationjump==2&&db==0) {   
                dichuyen.SetFloat("db",1);
                dem = timejump;
                db=1;
                }
        }
        if(!IsGrounded&&IsJumping) dichuyen.SetFloat("Jump",1);
        //if(!IsGrounded&&!IsJumping) dichuyen.SetFloat("Jump",1);
        else dichuyen.SetFloat("db",0);
        if(dem<0&&db==1){
            dichuyen.SetFloat("db",0);
            dem = timejump;
            db++;           
        }    
    }

    private void move(){
        ox = Input.GetAxisRaw("Horizontal");
        
        if(ox>0) lookDirection =1;
        if(ox<0) lookDirection =-1;
        Vector2 truc = vatly.position;
        truc.x = truc.x + ox*3.0f*Time.deltaTime;
        vatly.position = truc;    
    }
    private void Jump()
    {   
        if(animationjump<2)
        {
            if(animationjump==0&&!IsJumping)
            {
            vatly.velocity = new Vector2(vatly.velocity.x, 0f);
            vatly.AddForce(new Vector2(0f, 7.0f), ForceMode2D.Impulse);                      
            animationjump ++;
            }
            if(animationjump==1&&IsJumping)
            {    
            vatly.velocity = new Vector2(vatly.velocity.x, 0f);
            vatly.AddForce(new Vector2(0f, 7.0f), ForceMode2D.Impulse);                      
            animationjump ++;
            }
        }
            else IsJumping = true;
       /* if(IsSliding){
            IsWallJumping = true;
            Invoke("StopWallJump",wallJumpDuration);
        }*/
    }
    void StopWallJump(){
        IsWalling = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dat"))
        {    
             IsGrounded = true;           
            animationjump=0;            
            dichuyen.SetFloat("bam",-1);
            IsWalling =false;
            IsJumping = false;
            db=0;                     
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dat"))
        {    
            IsGrounded = false;           
            IsJumping = true;                     
        }        
    }

    void FixedUpdate()
    {
        if (IsSliding)
        {           
            // Áp dụng lực bám vào tường
            vatly.velocity = new Vector2(vatly.velocity.x, -LucHut);
        }
      /*  if(IsWallJumping){
            vatly.velocity = new Vector2(-ox*wallJumpForce.x,wallJumpForce.y);
        }
        else
        {
            vatly.velocity = new Vector2(ox*wallJumpForce.x,wallJumpForce.y);
        }*/
    }
    private void walljum(){
        if(Input.GetKeyDown(KeyCode.Q)){
           IsWallJumping = true;
            //vatly.velocity = new Vector2(vatly.velocity.x, 0f);
            vatly.velocity = new Vector2(-ox*wallJumpForce.x,wallJumpForce.y);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            vatly.velocity = new Vector2(vatly.velocity.x, 0f);
            vatly.velocity = new Vector2(ox*wallJumpForce.x,wallJumpForce.y);
            IsWallJumping = false;
        }
    }
    private void bamtuong(){
        RaycastHit2D hitleft = Physics2D.Raycast(vatly.position + Vector2.left*0.1f, Vector2.left, 0.2f, LayerMask.GetMask("tuong"));
        RaycastHit2D hitright = Physics2D.Raycast(vatly.position + Vector2.right*0.1f, Vector2.right, 0.2f, LayerMask.GetMask("tuong"));
        /*RaycastHit2D hitleft1 = Physics2D.Raycast(vatly.position + Vector2.left*0.2f, Vector2.left, 0.2f, LayerMask.GetMask("tuong"));
        RaycastHit2D hitright1 = Physics2D.Raycast(vatly.position + Vector2.right*0.1f, Vector2.right, 0.2f, LayerMask.GetMask("tuong"));
        if(hitleft1.collider!=null) wallright = true;
        else wallright = false;
        if(hitright1.collider!=null) wallleft = true;
        else wallleft = false;
        GetButton("Horizontal")
        */

        if(IsGrounded==false&&!IsWalling)
        {
            if (Input.GetKey(KeyCode.A)&&hitleft.collider != null&&!IsWallJumping)
            {
                // Đã có va chạm
                dichuyen.SetFloat("bam",1);
                IsWalling = true;
                animationjump =0;
                IsJumping = true;
                //dichuyen.SetFloat("Look y",-1);
                //IsWallJumping = false;
                IsSliding = true;
            }
            if (Input.GetKey(KeyCode.D)&&hitright.collider != null&&!IsWallJumping)
            {
                dichuyen.SetFloat("bam",1);
                IsWalling = true;
                animationjump =0;
                IsJumping = true;
                //dichuyen.SetFloat("Look y",-1);
                //IsWallJumping = false;
                IsSliding = true;
            }
            
            
            /*if(IsWallJumping&&Time.time>=jumpTime){
            vatly.gravityScale = 1f;
            IsWallJumping = false;
            //IsWalling = false;
            }*/
        }
        if(Input.GetKeyUp(KeyCode.D)||IsWallJumping==true){
                //if(hitright.collider!=null)
                //{
                // Không có va chạm                
                IsWalling=false;
                dichuyen.SetFloat("bam",-1);
                IsSliding = false;               
               // }
            }
            
            if(Input.GetKeyUp(KeyCode.A)||IsWallJumping==true){
                // Không có va chạm                
                IsWalling=false;
                IsSliding = false;  
                dichuyen.SetFloat("bam",-1);               
                
            }
    }
    
}
