using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;
    public float jumpForce;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemies;

    private bool attack;
    private bool faceright;
    private float deathTimer = 1.7F;
    public bool die;
    public float health;

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
        health = 200;
    }


    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();


        myCollider = GetComponent<Collider2D>();

    }

    void FixedUpdate()
    {

        m_Anim.SetBool("Die", die);
        if (health <= 0)
        {
            die = true;
        }
        if (die)
        {
            deathTimer -= Time.deltaTime;


            if (deathTimer <= 0)
                SceneManager.LoadScene("Game Over");
        }

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

            Collider2D[] Hit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
            for (int i = 0; i < Hit.Length; i++)
            {
                Hit[i].GetComponent<AI_Behaviour>().death = true;

                Debug.Log("Goddamnit fucking work already you shit");
            }

        }
        //else if (Input.GetKeyUp(KeyCode.LeftShift))
        else
        {
            attack = false;
        }


    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}