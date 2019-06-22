using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public Vector3  socketPosition;
    public Vector3  offsetToSocketPos;
    public Vector3  alternativeToSocket;
    public GameObject socketRef = null;
    public bool     isCorner = false;

    // Start is called before the first frame update
    void Start()
    {
        socketPosition = transform.position + offsetToSocketPos;
    }

    public Vector3 GetSocketPosition()
    {
        //Gets the location of the 'Socket' within the scene as it will be adjusted by rotation
        Vector3 toSocketPos = transform.position - socketRef.transform.position;
        toSocketPos.Normalize();
        toSocketPos *= -10;
        socketPosition = transform.position + toSocketPos;
        return socketPosition;
    }

    public void InformOfPreviousPosition(Vector3 position)
    {
        Vector3 directionToPrevious = position - transform.position;
        Vector3 directionToBack = alternativeToSocket;
        
        //Checks if the object is parallel and facing away (needs to rotate 180 degrees)
        if (directionToBack + directionToPrevious != new Vector3(0, 0, 0))
        {
            //Uses cross product to rotate the object
            directionToPrevious.y = 0;
            directionToBack.y = 0;
            Vector3 v3;
            v3.x = (directionToBack.y * directionToPrevious.z) - (directionToBack.z * directionToPrevious.y);
            v3.y = (directionToBack.z * directionToPrevious.x) - (directionToBack.x * directionToPrevious.z);
            v3.z = (directionToBack.x * directionToPrevious.y) - (directionToBack.y * directionToPrevious.x);
            float chosenCross = v3.y;
            float theta = Mathf.Asin(chosenCross / (directionToPrevious.magnitude * directionToBack.magnitude));
            theta *= (180 / Mathf.PI);
            transform.Rotate(new Vector3(0, theta, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
        
    }
}
