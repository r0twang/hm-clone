using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    float speed = 10.0f;
    float torque = 10.0f;
    bool setAccelerate = false;
    bool setBrake = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EnterCarScript.isPlayerInAnyCar == true)
        {
            if (Input.GetButtonDown("Accelerate"))
            {
                setAccelerate = true;
            }
            else if (Input.GetButtonDown("Brake"))
            {
                setBrake = true;
            }
            if (Input.GetButtonUp("Accelerate"))
            {
                setAccelerate = false;
            }
            else if (Input.GetButtonUp("Brake"))
            {
                setBrake = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rigBod2 = GetComponent<Rigidbody2D>();
        if (setAccelerate == true)
        {
            //Debug.Log("Accelerate");
            rigBod2.AddForce(transform.up * speed);
        }
        rigBod2.AddTorque(Input.GetAxis("Horizontal") * torque);

        if (setBrake == true)
        {
            //Debug.Log("Brake");
            rigBod2.AddForce(transform.up * (-1) * speed);
        }
    }
}
