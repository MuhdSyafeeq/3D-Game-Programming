using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    // Item Checking
    Item currentIngredients;
    [SerializeField] int hotbar;
    [SerializeField] int sameIngre = 0;

    // For Item to be Stored into another Inventories
    [SerializeField] List<Item> itemCook = new List<Item>();
    [SerializeField] Recipe recipe;
    [SerializeField] List<Recipe> cookRecipe = new List<Recipe>();

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
        //recipe = [];  
    }

    private void LateUpdate()
    {
        for(int i = 0; i < itemArr.Length; i++)
        {
            if(inBounds(i, itemCook))
            {
                itemArr[i].sprite = itemCook[i].icon;
                itemArr[i].color = new Color(255, 255, 255, 255);
            }
            else
            {
                itemArr[i].sprite = null;
                itemArr[i].color = new Color(255, 255, 255, 255);
            }
        }

        if(viewTimer.fillAmount != 0)
        {
            viewTimer.fillAmount -= (float)(Time.deltaTime / 10);
        }
        else if(viewTimer.fillAmount == 0 && itemCook.Count != 0)
        {
            for(int i = 0; i < itemCook.Count; i++)
            {
                if(itemCook[i] != null)
                {
                    Item current_ = itemCook[i];
                    Inventory.instance.inventories.Add(current_);
                    itemCook.Remove(current_);
                    sameIngre = 0;
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

        if(isNearPlate && Input.GetMouseButtonDown(1) && !Input.GetMouseButtonUp(1))
        {
            /*** 
             * === Using Search Through Item ===
             * Calculate Current Item in Cook, 
             * Calculate Recipe Required, 
             * Check if both meets requirements
             ***/
            
            if(itemCook.Count != 0)
            {
                int cur_Bread = 0, cur_Egg = 0, cur_Meat = 0, curr_Cheese = 0;

                for (int i = 0; i < itemCook.Count; i++)
                {
                    if (itemCook[i] != null)
                    {
                        switch (itemCook[i].name)
                        {
                            case "Bread":
                                cur_Bread += 1;
                                break;
                            case "Egg":
                                cur_Egg += 1;
                                break;
                            case "Meat":
                                cur_Meat += 1;
                                break;
                            case "Cheese":
                                curr_Cheese += 1;
                                break;
                            default:
                                Debug.LogError("Unable to Determine Item Name!");
                                break;
                        }
                    }
                }

                int req_Bread = 0, req_Egg = 0, req_Meat = 0, req_Cheese = 0;

                for (int i = 0; i < recipe.itemObj.Length; i++)
                {
                    if (recipe.itemObj[i] != null)
                    {
                        switch (recipe.itemObj[i].name)
                        {
                            case "Bread":
                                req_Bread += 1;
                                break;
                            case "Egg":
                                req_Egg += 1;
                                break;
                            case "Meat":
                                req_Meat += 1;
                                break;
                            case "Cheese":
                                req_Cheese += 1;
                                break;
                            default:
                                Debug.LogError("Unable to Determine Item Name!");
                                break;
                        }
                    }
                }

                //Debug.LogError($"R_Bread -> {req_Bread} R_Egg -> {req_Egg} R_Meat -> {req_Meat} R_Cheese -> {req_Cheese}");
                //Debug.LogError($"Bread -> {cur_Bread} Egg -> {cur_Egg} Meat -> {cur_Meat} Cheese -> {curr_Cheese}");

                if (checkRequirements(cur_Bread, req_Bread) && checkRequirements(cur_Egg, req_Egg)
                    && checkRequirements(cur_Meat, req_Meat) && checkRequirements(curr_Cheese, req_Cheese))
                {
                    Inventory.instance.inventories.Add(recipe.FinalProduct);
                    while (true)
                    {
                        if (itemCook.Count > 0) itemCook.Remove(itemCook[0]);
                        else { break; }
                    }
                    //for(int i = 0; i < itemCook.Count; i++)
                    //{
                    //    if (itemCook[i] != null)
                    //    {
                    //        itemCook.Remove(itemCook[i]);
                    //    }
                    //}
                    cur_Bread = 0; cur_Egg = 0; cur_Meat = 0; curr_Cheese = 0;
                    req_Bread = 0; req_Egg = 0; req_Meat = 0; req_Cheese = 0;
                }
                else { Debug.Log("Little Red -> I NEED MORE INGREDIENTS"); }
            }

            
        }
    }

    bool checkRequirements(int @CurrentItem, int @RequiredItem) 
    {
        if(CurrentItem == RequiredItem) { return true; }
        else { return false; }
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
                        Inventory.instance.Remove(currentIngredients);
                    }

                }
                else { Debug.Log("I have nothing to add on this slot."); }
            }
        }
    }
}
