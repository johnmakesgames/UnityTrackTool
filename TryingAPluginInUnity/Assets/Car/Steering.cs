using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public GameObject[] wheels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateWheels();
        RotateCar();
    }

    void RotateWheels()
    {
        if(Input.GetKey(KeyCode.A))
        {
            wheels[0].transform.eulerAngles = new Vector3(wheels[0].transform.eulerAngles.x, transform.eulerAngles.y -30, wheels[0].transform.eulerAngles.z);
            wheels[1].transform.eulerAngles = new Vector3(wheels[1].transform.eulerAngles.x, transform.eulerAngles.y - 30, wheels[1].transform.eulerAngles.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            wheels[0].transform.eulerAngles = new Vector3(wheels[0].transform.eulerAngles.x, transform.eulerAngles.y + 30, wheels[0].transform.eulerAngles.z);
            wheels[1].transform.eulerAngles = new Vector3(wheels[1].transform.eulerAngles.x, transform.eulerAngles.y + 30, wheels[1].transform.eulerAngles.z);
        }
        else
        {
            wheels[0].transform.eulerAngles = new Vector3(wheels[0].transform.eulerAngles.x, transform.eulerAngles.y, wheels[0].transform.eulerAngles.z);
            wheels[1].transform.eulerAngles = new Vector3(wheels[1].transform.eulerAngles.x, transform.eulerAngles.y, wheels[1].transform.eulerAngles.z);
        }
    }

    void RotateCar()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -1, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }
}
