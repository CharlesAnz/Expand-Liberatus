using UnityEngine;
using UnityEngine.AI;


/////////////////////////////* Instruction of use *///////////////////////////////////////
/* create two game objects as children for the enemy, wall detection and ground detection
 * set distance of the raycasts
 * set the target 
*/


public class AI_Behaviour : MonoBehaviour
{
    [Range(0,10)]
    public float speed;
    [Range(0, 10)]
    public float GroundDetectionRayLength;
    [Range(0, 10)]
    public float WallDetectionRayLength;

    [SerializeField]
    private bool movingRight = false;
    public bool death;
    public bool attack;
    private float deathTimer;

    public GameObject behindPos;
    //public float behindRange;
    public LayerMask Player;
    public ContactFilter2D players;


    public Transform groundDetection;
    public Transform wallDetection;
    public GameObject target;

    private Animator m_Anim;
    private Collider2D myCollider;


    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        deathTimer = 2;
        players.SetLayerMask(Player);
        players.useLayerMask = true;
    }

    void Update()
    {
        m_Anim.SetBool("Attack", attack);
        m_Anim.SetBool("Death", death);

        Move();

        if (death)
        {
            deathTimer -= Time.deltaTime;


            if (deathTimer <= 0)
                Destroy(gameObject);
        }
    }

    void Patrol()
    {
        //speed = 1;
        attack = false;
        transform.Translate(Vector2.right * speed * Time.deltaTime);


        RaycastHit2D hit = Physics2D.Raycast(groundDetection.position, Vector2.down, GroundDetectionRayLength);
        RaycastHit2D hitWall = Physics2D.Raycast(wallDetection.position, Vector2.left, WallDetectionRayLength);

        if (hit.collider == false) // Raycast for ground detection
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        if (hitWall.collider == true) // Raycast for wall detection
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

        void Move()
    {
        ApproachPlayer();
    }

    void ApproachPlayer() // when player is less than 5 untis, approach with greater speed
    {
        //speed = 2;

        if (Vector2.Distance(transform.position, target.transform.position) <= 3
        && Vector2.Distance(transform.position, target.transform.position) > 1)
        {


            if (behindPos.GetComponent<Collider2D>().IsTouchingLayers(Player))
            {
                Debug.Log("Goddamnit fucking work already you shit");
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }

            attack = false;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, target.transform.position) <= 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            attack = true;
            //death = true; 
        }
        else Patrol();
      }

    /*
    private void OnCollisionEnter2D(Collision2D collision) // deactivate when colliding with player
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
    */
}


