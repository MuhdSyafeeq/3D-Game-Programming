using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "House")
        {
            Debug.Log("TriggeredStay: Entering -> " + other.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "House")
        {
            Debug.Log("TriggerEnter: Entering -> " + other.name);
        }
    }
}
