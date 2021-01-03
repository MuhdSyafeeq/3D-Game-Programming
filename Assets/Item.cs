using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] int rotationSpeed = 5;

    void Update()
    {
        transform.Rotate(
            this.transform.rotation.x,
            rotationSpeed * Time.deltaTime,
            this.transform.rotation.z
            );
    }
}
