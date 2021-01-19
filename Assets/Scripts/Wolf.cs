using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wolf : MonoBehaviour
{
    public static Wolf instance;
    void Awake()
    {
        #region Singleton Method
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        instance = this;
        #endregion

        Wolve = State.Idle;
        _animate.PlayQueued("idle", QueueMode.PlayNow);
        Waiting_Time = 35f;

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

    // Wolf Animation
    [SerializeField] Animation _animate;
    private bool changeAnim = false;

    // Wolf Inventories / Food Requirements
    [SerializeField] string currentLevels = null;
    [SerializeField] List<Item> Hunger;

    // Wolf State
    [SerializeField] private State Wolve = State.Idle;
    [SerializeField] private float Waiting_Time;

    // Wolf Interaction
    private bool isNearWolf = false, isFinished = false, itemAccept = false;
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

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearWolf = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Hunger.Count == 3)
        {
            isFinished = true;
        }

        if (Waiting_Time <= 0)
        {
            if (Wolve == State.Idle)
            {
                Wolve = State.Walk;
                Waiting_Time = 50f;

                changeAnim = true;
            }
            else if (Wolve == State.Walk) //Create an Opening of Chase
            {
                Wolve = State.Jump;
                Waiting_Time = 2.03f;

                changeAnim = true;
            }
            else if(Wolve == State.Jump)
            {
                Wolve = State.Chase;
                Waiting_Time = 75f;

                changeAnim = true;
            }
        }
        Waiting_Time -= Time.deltaTime;
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
                    if (itemAccept) { Inventory.instance.inventories.Remove(intItem); }
                }
            }
            else if (Inventory.instance.inventories.Count == 0)
            {
                Debug.Log("Wolf: -> Trying to give me empty handed?");
            }
        }


        if (Wolve == State.Walk && changeAnim == true)
        {
            _animate.PlayQueued("walk", QueueMode.PlayNow);
            Debug.Log(_animate.name);
            changeAnim = false;
        }
        if (Wolve == State.Jump && changeAnim == true)
        {
            _animate.PlayQueued("jump", QueueMode.PlayNow);
            Debug.Log(_animate.name);
            changeAnim = false;
        }
        if (Wolve == State.Chase && changeAnim == true)
        {
            _animate.PlayQueued("run", QueueMode.PlayNow);
            Debug.Log(_animate.name);
            changeAnim = false;
        }
    }
}
