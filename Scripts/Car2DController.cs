using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DController : MonoBehaviour
{
    bool setAccelerate = false;
    bool setBrake = false;
    float speed = 13.0f;
    float torque = -200.0f;
    float friction = 4.0f;
    float driftFactorSticky = 0.9f;
    float driftFactorSlippy = 1.0f;
    float maxStickyVelocity = 2.5f;
    //float minSlippyVelocity = 1.5f;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (EnterCarScript.isPlayerInAnyCar)
        {
            if (Input.GetButtonDown("Accelerate"))
            {
                setAccelerate = true;
            }
            else if (Input.GetButtonDown("Brakes"))
            {
                setBrake = true;
            }
            if (Input.GetButtonUp("Accelerate"))
            {
                setAccelerate = false;
            }
            else if (Input.GetButtonUp("Brakes"))
            {
                setBrake = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rigBod2 = GetComponent<Rigidbody2D>();

        float driftFactor = driftFactorSticky;
        if (RightVelocity().magnitude > maxStickyVelocity)
        {
            driftFactor = driftFactorSlippy;
        }

        rigBod2.velocity = ForwardVelocity() + RightVelocity() * driftFactor;

        //Debug.Log(RightVelocity().magnitude);

        if (setAccelerate == true)
        {
            //Debug.Log("Accelerate");
            rigBod2.AddForce(transform.up * speed);
        }

        float tf = Mathf.Lerp(0, torque, rigBod2.velocity.magnitude / 4);

        rigBod2.angularVelocity = Input.GetAxis("Horizontal") * tf;

        if (setBrake == true)
        {
            rigBod2.AddForce(transform.up * (-1) * (speed / 2));
        }

        
    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }
}