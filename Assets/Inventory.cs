using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton Method
    public static Inventory instance;
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance(s) of Inventory found!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
    [SerializeField] int capacity = 5; 
    [SerializeField] public List<Item> inventories = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isIngredient)
        {
            if(inventories.Count >= capacity)
            {
                Debug.Log("Not enough spaces.");
                return false;
            }
            inventories.Add(item);
        }

        return true;
    }

    public void Remove(Item item)
    {
        inventories.Remove(item);
    }

}
