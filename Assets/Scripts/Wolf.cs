using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wolf : MonoBehaviour
{
    public static Wolf instance;
    void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            Debug.Log("SPAWNED");
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
        {
            Debug.Log("DESTROYED");
            Destroy(gameObject);
        }
        #endregion

        Wolve = State.Idle;
        Waiting_Time = 11f;

        currentLevels = SceneManager.GetActiveScene().name;
        if (currentLevels == "Milestone1")
        {
            Hunger = new List<Item>(3);
            Debug.Log($"Wolf: -> Feed me with this {SceneManager.GetActiveScene().name}'s best Sandwiches.");
        }
    }

    enum State
    {
        Idle,
        Walk,
        Chase,
        Jump,
        Attack,
    }

    // Wolf Inventories / Food Requirements
    [SerializeField] string currentLevels = null;
    [SerializeField] List<Item> Hunger;
    
    // Wolf Animation
    [SerializeField] Animation _animate;
    private bool changeAnim = false;

    // Wolf State
    [SerializeField] private float Waiting_Time;
    [SerializeField] private State Wolve = State.Idle;

    // Wolf Interaction
    [SerializeField] private bool isNearWolf = false, isFinished = false, itemAccept = false;
    Item intItem;

    bool AddItem(Item item)
    {
        if(item.name != "Sandwich")
        {
            Debug.Log("That's Not What I Wanted");
            return false;
        }

        Hunger.Add(item);
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearWolf = true;
        }
    }
    void OnTriggerExit(Collider other) { if (other.tag == "Player") { isNearWolf = false; } }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        if (isFinished)
        {
            Debug.Log($"Wolf: -> Thank you, I will rest now.. Enjoy the {SceneManager.GetActiveScene().name}'s Village.");
            Debug.LogError("Game Ended");
        }

        if (isNearWolf && Input.GetKeyDown(KeyCode.E))
        {
            if (Inventory.instance.inventories.Count != 0)
            {
                for (int i = 0; i < Inventory.instance.inventories.Count; i++)
                {
                    intItem = Inventory.instance.inventories[i];
                    itemAccept = AddItem(intItem);
                    if (itemAccept) { Inventory.instance.Remove(intItem); }
                }
            }
            else if (Inventory.instance.inventories.Count == 0)
            {
                Debug.Log("Wolf: -> Trying to give me empty handed?");
            }
        }
    }
}
