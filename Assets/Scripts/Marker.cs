using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform mk_destination;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "House")
        {
            Debug.Log("TriggeredStay: Entering -> " + other.name);
            if(other.tag == "Player")
            {
                Vector3 offset = mk_destination.position + new Vector3(0, 3, 0);
                other.transform.position = offset;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "House")
        {
            Debug.Log("TriggerEnter: Entering -> " + other.name);
            if (other.tag == "Player")
            {
                Vector3 offset = mk_destination.position + new Vector3(0, 3, 0);
                other.transform.position = offset;
            }
        }
    }
}
