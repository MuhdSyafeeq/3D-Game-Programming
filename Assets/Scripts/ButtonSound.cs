using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = PlayerCamera.instance.GetComponentInChildren<AudioSource>();
    }

    public void OnClick()
    {
        source.Play();
    }
}
