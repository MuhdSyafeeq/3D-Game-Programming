using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public List<Item> playerInventories;
    public float[] position;

    public PlayerData()
    {

        //playerInventories = Inventory.instance.inventories;

        //Health = (int)PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<Health>().GetComponent<Slider>().value;
        //Stamina = (int)PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<Stamina>().GetComponent<Slider>().value;


        position = new float[3];
        position[0] = MoveCharacter.instance.transform.position.x;
        position[1] = MoveCharacter.instance.transform.position.y;
        position[2] = MoveCharacter.instance.transform.position.z;

        Debug.Log(position[0] + "," + position[1] + "," + position[2]);
    }
}
