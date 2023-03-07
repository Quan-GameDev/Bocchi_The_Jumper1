using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpPower;
    private Rigidbody2D rb;
    //1=left, 2=ri
    private int direction;
    public bool movement;
    private float dirX;
    private float height;
    private float preheight;
    // Start is called before the first frame update
    public void Start()
    {
        direction = 1;
        movement = false;
        rb = GetComponent<Rigidbody2D>();
        height = transform.position.y;
    }

    // Update is called once per frame

    public void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        preheight = height;
        height = transform.position.y;
        if (height==preheight)
            rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
        if (height != preheight)
        {
            Debug.Log("height: " + height.ToString());
            Debug.Log(preheight.ToString());
        }

        if (dirX < 0)
        {
            direction = 1;
        }
        else if (dirX > 0)
        {
            direction = 2;
        }
        
        if (Input.GetKeyDown("space"))
        {
            movement = true;
            if (direction == 1)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(-2 * 7f, jumpPower, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(2 * 7f, jumpPower, 0);
            }
        }
    }
}
