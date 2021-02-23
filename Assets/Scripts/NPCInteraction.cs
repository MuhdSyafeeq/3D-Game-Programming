using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] GameObject interaction;

    bool isInteractable;
    bool isInteracting;

    private void Awake()
    {
        isInteractable = false;
        isInteracting = false;
        interaction.SetActive(false);
    }

    private void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            interaction.SetActive(true);
            isInteracting = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInteractable = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInteractable = false;
            isInteracting = false;
            interaction.SetActive(false);
        }
    }
}
