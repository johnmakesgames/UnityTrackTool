using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    float speed = 0;
    float maxSpeed = 30;
    float speedIncrease = 1.5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(speed < maxSpeed)
            {
                speed += speedIncrease * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (speed < maxSpeed)
            {
                speed -= speedIncrease * Time.deltaTime;
            }
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            if (speed < maxSpeed)
            {
                speed -= speedIncrease * 2 * Time.deltaTime;
            }
        }
        else
        {
            if(speed > 0)
            {
                speed -= speedIncrease / 2 * Time.deltaTime;
            }
            else
            {
                speed = 0;
            }
        }
        if (speed < 0)
            speed = 0;
        transform.position = transform.position + ((transform.forward * -speed) * Time.deltaTime);
    }

    public float GetCurrentSpeed()
    {
        return speed;
    }
}
