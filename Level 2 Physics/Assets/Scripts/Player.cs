using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool IsFacingRight = true;
    
    public float Speed;
    public float Smooth;

    public float JumpForce;
    public LayerMask GroundLayer;
    public Transform Ground;
    public float Checkradius;
    public bool Grounded;

    private Rigidbody2D rb;
    private Animator ar;

    
    private void Flip()
    {

        IsFacingRight = !IsFacingRight;

        Vector2 scale = transform.localScale;

        scale.x = scale.x * -1;

        transform.localScale = scale;

    }

    
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        ar = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");

        ar.SetBool("Walking", x != 0);
        ar.SetBool("Jumping", Grounded == false);

        Grounded = Physics2D.OverlapCircle(Ground.position, Checkradius, GroundLayer);
        

        Vector2 targetvelocity = new Vector2(x * Speed, rb.velocity.y);

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetvelocity, ref targetvelocity, Smooth * Time.deltaTime);

        if(x < 0 && IsFacingRight == true)
        {

            Flip();

        }

        if(x > 0 && IsFacingRight == false)
        {

            Flip();

        }

        if(Input.GetKeyDown(KeyCode.Space) && Grounded == true)
        {

            rb.velocity = new Vector2(rb.velocity.x, JumpForce);

        }

    }
}
