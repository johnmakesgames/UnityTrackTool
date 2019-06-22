using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {
            Debug.Log("Collided");
            other.gameObject.GetComponent<Rigidbody>().velocity += transform.forward * 10;
        }
    }
}
