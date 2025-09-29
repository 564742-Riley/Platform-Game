using System.Runtime.CompilerServices;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D rb;
    float xv, yv;
    bool isGrounded;
    public Animator anim;
    //private float Move;
    SpriteRenderer sr;

    public LayerMask groundLayer;

    void Start()
    {
        // set the mask to be ground
        groundLayer = LayerMask.GetMask("Ground");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }


    
    // Update is called once per frame
    void Update()
    {
        
        float xvel, yvel;

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;

        if (Input.GetKey("a"))
        {
            xvel = -8;
        }

        if (Input.GetKey("d"))
        {
            xvel = 8;
        }

        if (xvel >= 0.1f || xvel <= -0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


        if (Input.GetKeyDown(KeyCode.LeftAlt) || (Input.GetKeyDown(KeyCode.Space)) )
        {
            if (isGrounded == true)
            {
                yvel = 14;
                print("do jump");
            }
        }




        rb.linearVelocity = new Vector3(xvel, yvel, 0);


        //flip sprite
        if (xvel > 0)
        {
            //gameObject.transform.localScale = new Vector3(1, 1, 1);
            sr.flipX = false;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        if (xvel < 0)
        {
            sr.flipX = true;
        }

        //do the groundcheck
        if( ExtendedRayCollisionCheck(0,0) == true )
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }



    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {

        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);


        //cast a ray downward
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            print("Player has collided with ground layer ");
            hitColor = Color.green;
            hitSomething = true;
        }
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);
        return hitSomething;
    }

  
}

