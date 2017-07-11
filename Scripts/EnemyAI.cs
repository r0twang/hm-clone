using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    GameObject Player;
    public bool patrol = true, guard = false, clockwise = false;
    public bool moving = true;
    public bool pursuingPlayer = false, goingToLastLoc = false;
    Vector3 target;
    Rigidbody2D rid;
    public Vector3 playerLastPos;
    RaycastHit2D hit;
    float speed = 2.0f;
    int layerMask = 1 << 8;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = this.transform.position;
        rid = this.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;
	}
	
	// Update is called once per frame
	void Update () {
        movement();
        PlayerDetect();
	}

    void movement ()
    {
        float dist = Vector3.Distance(Player.transform.position, this.transform.position);
        Vector3 dir = Player.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x, dir.y), dist, layerMask);
        Debug.DrawRay(transform.position, dir, Color.red);

        Vector3 fwt = this.transform.TransformDirection(Vector3.right);

        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), 1.0f, layerMask);

        Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), Color.cyan);

        if (moving == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (patrol == true)
        {
            //Debug.Log("Patrolling normally");
            speed = 2.0f;

            if (hit2.collider != null)
            {
                if (hit2.collider.gameObject.tag == "Wall")
                {
                    if (clockwise == false)
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
        }

        if (pursuingPlayer == true)
        {
            Debug.Log("Pursuing Player");
            speed = 3.5f;
            rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
            if (hit.collider.gameObject.tag == "Player")
            {
                playerLastPos = Player.transform.position;
            }
        }

        if (goingToLastLoc == true)
        {
            Debug.Log("Checking last known Player position");
            speed = 3.0f;
            rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
            if (Vector3.Distance(this.transform.position, playerLastPos) < 1.5f)
            {
                patrol = true;
                goingToLastLoc = false;
            }
        }
    }

    public void PlayerDetect()
    {
        Vector3 pos = this.transform.InverseTransformPoint(Player.transform.position);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player" && pos.x > 1.5f && Vector3.Distance(this.transform.position, Player.transform.position) < 9)
            {
                patrol = false;
                pursuingPlayer = true;
            }
            else
            {
                if (pursuingPlayer == true)
                {
                    goingToLastLoc = true;
                    pursuingPlayer = false;
                }
            }
        }
    }
}
