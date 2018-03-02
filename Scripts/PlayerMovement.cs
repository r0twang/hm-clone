using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public bool moving = false;
    float speed = 3.5f;

    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (EnterCarScript.isPlayerInAnyCar == false)
        {
            movement();
        }
    }

    void movement ()
    {
        if (EnterCarScript.isPlayerInAnyCar == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
                moving = true;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
                moving = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                moving = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                moving = true;
            }
            if (Input.GetKey(KeyCode.I) != true && Input.GetKey(KeyCode.K) != true && Input.GetKey(KeyCode.J) != true && Input.GetKey(KeyCode.L) != true)
            {
                moving = false;
            }
        }
    }
}
