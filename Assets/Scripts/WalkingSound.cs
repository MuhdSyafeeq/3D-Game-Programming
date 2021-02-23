using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip walkGrass;
    [SerializeField] AudioClip walkFloor;
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            audio.PlayOneShot(walkGrass);
        else
            audio.PlayOneShot(walkFloor);
    }
}
