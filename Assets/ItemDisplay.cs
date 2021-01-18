using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image[] img;

    // Update is called once per frame
    void Update()
    {
        if(Inventory.instance != null && Inventory.instance.inventories.Count != 0)
        {
            for(int i = 0; i < Inventory.instance.inventories.Count; i++)
            {
                if(Inventory.instance.inventories[i] != null)
                {
                    img[i].sprite = Inventory.instance.inventories[i].icon;
                }
            }
        }
        else
        {
            foreach(Image @img in img)
            {
                @img.sprite = null;
            }
        }
    }
}
