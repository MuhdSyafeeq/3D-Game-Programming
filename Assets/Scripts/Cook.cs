using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    // Item Checking
    Item currentIngredients;
    [SerializeField] int hotbar;

    // For Item to be Stored into another Inventories
    [SerializeField] List<Item> itemCook = new List<Item>();
    [SerializeField] List<Item> recipe = new List<Item>();

    // Check if Near the Plate
    [SerializeField] bool isNearPlate = false;

    // Ui Placement
    [SerializeField] Image[] itemArr;
    [SerializeField] Image viewTimer;

    public void getSituation(bool situation)
    {
        situation = isNearPlate ;
    }

    bool AddIngredients(Item item)
    {
        if(itemCook.Capacity != 5)
        {
            if (!item.isIngredient) { return false; }

            itemCook.Add(item);
            return true;
        }
        else { return false; }
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

    private bool inBounds(int index, List<Item> array)
    {
        return (index >= 0) && (index < array.Count);
    }

    // Start is called before the first frame update
    void Start()
    {
        //recipe = ;  
    }

    private void LateUpdate()
    {
        if(Inventory.instance.inventories.Count != 0)
        {
            for(int i = 0; i < 5; i++)
            {
                if(inBounds(i, itemCook))
                {
                    itemArr[i].sprite = itemCook[i].icon;
                    itemArr[i].color = new Color(255, 255, 255, 255);
                }
                else
                {
                    itemArr[i].sprite = null;
                    itemArr[i].color = new Color(255, 255, 255, 0);
                }
            }
        }

        if(viewTimer.fillAmount != 0)
        {
            viewTimer.fillAmount -= (float)(Time.deltaTime / 10);
        }
        else if(viewTimer.fillAmount == 0 && Inventory.instance.inventories.Count != 0)
        {
            for(int i = 0; i < itemCook.Count; i++)
            {
                if(itemCook[i] != null)
                {
                    Item current_ = itemCook[i];
                    Inventory.instance.inventories.Add(current_);
                    itemCook.Remove(current_);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        hotbar = PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<ItemDisplay>().getHotkey();
        if (isNearPlate && Input.GetKey(KeyCode.E))
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

        if(isNearPlate && Input.GetMouseButton(1))
        {
            for (int i = 0; i < itemCook.Count; i++)
            {
                if (itemCook[i] != null)
                {
                    Item current_ = itemCook[i];
                    Inventory.instance.inventories.Add(current_);
                    itemCook.Remove(current_);
                }
                else { Debug.Log($"I need to find ingredients before cooking..."); break; }
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
                    Debug.Log($"System: -> Cooking: ({Inventory.instance.inventories[hotbar - 1].name}) added.");
                    currentIngredients = Inventory.instance.inventories[hotbar - 1];

                    bool acceptIngredientsOnly = AddIngredients(currentIngredients);
                    if (acceptIngredientsOnly)
                    {
                        viewTimer.fillAmount = 1;
                        Inventory.instance.inventories.Remove(currentIngredients);
                    }

                }
                else { Debug.Log("I have nothing to add on this slot."); }
            }
        }
    }
}
