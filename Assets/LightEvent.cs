using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : MonoBehaviour
{
    [SerializeField] Clock current_;

    private void LateUpdate()
    {
        if(current_.time >= 64800)
        {
            this.GetComponent<Light>().enabled = true;
        }
        else if(current_.time >= 21600) this.GetComponent<Light>().enabled = false;
    }
}
