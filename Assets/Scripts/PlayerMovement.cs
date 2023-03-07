using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpPower;
    public float jumpDistance;


    private Rigidbody2D rb;
    //1=left, 2=ri
    private int direction;
    private bool movement;
    private float dirX;
    private float height;
    private bool isJump;
    private float preheight;
    private Animator animation;
    private MovementState state;
    private bool facingRight = false;
    private float timer;
    private float lasttimer;
    private bool holdSpace;
    // Start is called before the first frame update


    private enum MovementState
    {
        idle, running, falling, jumping, abouttojump
    }

    public void Start()
    {
        MovementState state;
        direction = 1;
        movement = true;

        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        state = MovementState.idle;
        animation.SetInteger("state",(int)state);
        height = transform.position.y;
        rb.freezeRotation = true;
        isJump = false;
        holdSpace = false;

    
    }

    // Update is called once per frame

    public void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        preheight = height;
        height = transform.position.y;
        if (movement)
            rb.velocity = new Vector2(dirX * 4f, rb.velocity.y);
        if (height != preheight)
        {
            // Debug.Log("height: " + height.ToString());
            // Debug.Log(preheight.ToString());
        }
        if (dirX < 0 && facingRight && !isJump)
        {
            direction = 1;
            Flip();
        }
        else if (dirX > 0 && !facingRight && !isJump)
        {
            direction = 2;
            Flip();
        }
        if (Input.GetKeyDown("space") && !isJump){
            timer = Time.time;
            holdSpace = true;
        }
        
        if (Input.GetKeyUp("space") && !isJump)
        {
            timer = Time.time - timer;
            if (timer<0.5f) timer = 0.5f;

            movement = false;
            isJump = true;
            holdSpace = false;

            if (direction == 1)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(-jumpDistance, jumpPower*timer, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(jumpDistance, jumpPower*timer, 0);
            }
            timer = 0;
        }

        AnimationChecker();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        isJump = false;
        movement = true;
    }

    private void AnimationChecker(){
        if (holdSpace){
            state = MovementState.abouttojump;
        }
        else if (rb.velocity.x<-.1f && !isJump){
            state = MovementState.running;
        }
        else if (rb.velocity.x>.1f && !isJump){
            state = MovementState.running;
        }
        else if (rb.velocity.y>.1f && isJump){
            state = MovementState.jumping;
        }
        else if (rb.velocity.y<-.1f && isJump){
            state = MovementState.falling;
        }
        else if (holdSpace){
            state = MovementState.abouttojump;
        }
        else{
            state = MovementState.idle;
        }
        Debug.Log((int)state);
        animation.SetInteger("state",(int)state);
    }

    private void Flip(){
        facingRight = !facingRight;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
}
