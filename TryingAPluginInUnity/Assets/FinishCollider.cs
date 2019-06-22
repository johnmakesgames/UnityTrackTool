using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishCollider : MonoBehaviour
{
    int laps;
    Vector3 enterPos;
    private void OnTriggerEnter(Collider other)
    {
        enterPos = other.gameObject.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 exitPos = other.gameObject.transform.position;
        Vector3 direction = exitPos - enterPos;
        direction.Normalize();
        if (direction.z > 0)
            laps++;
        else
            laps--;
    }

    private void Update()
    {
        Text lapText = GameObject.Find("LapsText").GetComponent<Text>();
        lapText.text = "Laps: " + (laps/11).ToString();
    }

}
