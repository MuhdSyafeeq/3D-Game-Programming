using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform mk_destination;
    [SerializeField] private float inTimer;
    [SerializeField] private bool isAccess = false;
    [SerializeField] private GameObject savedData;

    private void LateUpdate()
    {
        if (isAccess)
        {
            Vector3 offset = mk_destination.position + new Vector3(0, 3, 0);
            savedData.transform.position = offset;

            isAccess = false;
            inTimer = 0;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "House")
        {
            Debug.Log("TriggeredStay: Entering -> " + other.name);
            if(other.tag == "Player")
            {
                savedData = other.gameObject;

                if(inTimer >= 2f) { isAccess = true; }

                inTimer += Time.deltaTime;
            }
        }
    }
}
