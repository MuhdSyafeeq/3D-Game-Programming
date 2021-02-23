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
    [SerializeField] int capacity = 7;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip addItem;
    [SerializeField] AudioClip cannotAddItem;
    [SerializeField] AudioClip dropItem;

    [SerializeField] public List<Item> inventories = new List<Item>();

    public bool Add(Item item)
    {
        if(inventories.Count >= capacity)
        {
            Debug.Log("Not enough spaces.");
            audio.PlayOneShot(cannotAddItem);
            return false;
        }
        inventories.Add(item);
        audio.PlayOneShot(addItem);

        return true;
    }

    public void Remove(Item item)
    {
        audio.PlayOneShot(dropItem);
        inventories.Remove(item);
    }

}
