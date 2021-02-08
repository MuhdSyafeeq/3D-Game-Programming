using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    Item currentIngredients;
    [SerializeField] int hotbar;
    [SerializeField] Image[] player_Htkey;
    [SerializeField] List<Item> itemCook = new List<Item>();
    [SerializeField] bool isNearPlate = false;

    public void getSituation(bool situation)
    {
        situation = isNearPlate ;
    }

    bool AddIngredients(Item item)
    {
        if (!item.isIngredient) { return false; }

        itemCook.Add(item);
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearPlate = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearPlate = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerCamera.instance.GetComponent<Canvas>().GetComponent<GameObject>().GetComponent<ItemDisplay>().getHotkeys(player_Htkey);
    }

    // Update is called once per frame
    void Update()
    {
        hotbar = PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<ItemDisplay>().getHotkey();
        if (isNearPlate && Input.GetMouseButtonDown(0))
        {
            if (Inventory.instance.inventories.Count != 0)
            {
                Cooking(hotbar);
            }
            else if (Inventory.instance.inventories.Count == 0)
            {
                Debug.Log($"I need to find ingredients before cooking...");
            }
        }
    }

    public void Cooking(int hotkeyNum)
    {
        for (int i = 0; i < Inventory.instance.inventories.Count; i++)
        {
            if (hotbar <= Inventory.instance.inventories.Count)
            {
                if (Inventory.instance.inventories[hotbar - 1] != null)
                {
                    Debug.Log($"System: -> Item: ({Inventory.instance.inventories[hotbar - 1].name}) dropped.");
                    currentIngredients = Inventory.instance.inventories[hotbar - 1];

                    bool acceptIngredientsOnly = AddIngredients(currentIngredients);
                    if (acceptIngredientsOnly)
                    {
                        Inventory.instance.inventories.Remove(currentIngredients);
                    }

                }
                else { Debug.Log("I have nothing to drop on this slot..."); }
            }
        }
    }
}
