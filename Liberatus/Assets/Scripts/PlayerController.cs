using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;
    public float jumpForce;

    private bool attack;
    private bool faceright;

    private Rigidbody2D myRigidbody;
    private Animator m_Anim;
    private Transform mytransform;

    public bool grounded;
    public LayerMask whatIsGround;

    private Collider2D myCollider;



    private void Awake()
    {
        // Setting up references.

        m_Anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mytransform = GetComponent<Transform>();
    }


    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();


        myCollider = GetComponent<Collider2D>();

    }

    void FixedUpdate()
    {
        //Detects whether the player is on the ground
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        CharacterAttack();

        //If spacebar or mouse button is pushed then character will jump
        CharacterJump();

        //Controls character movement
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        CharacterMovement(h);



    }

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }
    }

    void CharacterMovement(float m)
    {
        m_Anim.SetFloat("Speed", Mathf.Abs(m));
        m_Anim.SetBool("FaceRight", faceright);

        if (Input.GetKey(KeyCode.D))
        {
            faceright = true;
            mytransform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            faceright = false;
            mytransform.eulerAngles = new Vector2(0, 180);
        }

        if (m > 0 && faceright)
        {
            myRigidbody.velocity = new Vector2(m * moveSpeed, myRigidbody.velocity.y);
        }
        else if (m>0 && faceright && attack)
        {
            myRigidbody.velocity = new Vector2(m * (moveSpeed / 2), myRigidbody.velocity.y);
        }

        if (m < 0 && !faceright)
        {
            myRigidbody.velocity = new Vector2(m * moveSpeed, myRigidbody.velocity.y);

        }
        else if (m < 0 && !faceright && attack)
        {
            myRigidbody.velocity = new Vector2(m * (moveSpeed / 2), myRigidbody.velocity.y);
        }

    }



    void CharacterAttack()
    {
        m_Anim.SetBool("Attack", attack);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            attack = true;
        }
        //else if (Input.GetKeyUp(KeyCode.LeftShift))
        else
        {
            attack = false;
        }


    }
}