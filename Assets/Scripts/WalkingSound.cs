using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] AudioSource walk;
    private void OnTriggerEnter(Collider other)
    {
        walk.Play();
    }
}
