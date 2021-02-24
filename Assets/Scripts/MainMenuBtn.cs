using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBtn : MonoBehaviour
{
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();    
    }

    public void OnClick()
    {
        audio.Play();
    }
}
