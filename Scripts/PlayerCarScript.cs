using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarScript : MonoBehaviour {

    Vector3 directionToMove = Vector3.zero;
    int gear = 1;
    float gearTimer = 2.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        inputDetect();
        moveCar();
        gearControl();
    }

    void inputDetect()
    {
        if (EnterCarScript.isPlayerInAnyCar == true)
        {
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                directionToMove = (Vector3.right + Vector3.up).normalized;
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                directionToMove = (Vector3.left + Vector3.up).normalized;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                directionToMove = (Vector3.right + Vector3.down).normalized;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                directionToMove = (Vector3.left + Vector3.down).normalized;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                directionToMove = Vector3.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                directionToMove = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                directionToMove = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                directionToMove = Vector3.right;
            }
            else
            {
                directionToMove = Vector3.zero;
                gear = 1;
            }
        }
    }

    void moveCar()
    {
        transform.Translate(directionToMove * (5 * gear) * Time.deltaTime);
    }

    void gearControl()
    {
        if (gear < 3)
        {
            gearTimer -= Time.deltaTime;
            if (gear <= 0)
            {
                gear++;
                gearTimer = 2.0f;
            }
        }
    }


    public int getSpeed()//new method for working out collision
    {
        return 5 * gear;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (this.gameObject.GetComponent<EnterCarScript>().isPlayerInThisCar == true)
        {
            if (other.gameObject.tag == "Car")
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce((getDirectionToAddForce() * (gear + 1) * (25 * 2)), ForceMode2D.Impulse);
            }
            else if (other.gameObject.tag == "NPC")
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce((getDirectionToAddForce() * (2) * (5)), ForceMode2D.Impulse);
            }
        }

    }

    Vector2 getDirectionToAddForce()
    {
        float x = directionToMove.x;
        float y = directionToMove.y;


        return new Vector2(x, y).normalized;
    }
}
