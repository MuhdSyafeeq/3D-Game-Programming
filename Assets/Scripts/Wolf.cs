using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

        isFinished = false;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        currentLevels = SceneManager.GetActiveScene().name;
        if (currentLevels == "Milestone1")
        {
            Hunger = new List<Item>(3);
            Debug.Log($"Wolf: -> Feed me with this {SceneManager.GetActiveScene().name}'s best Sandwiches.");
        }
    }

    // Wolf Inventories / Food Requirements
    [SerializeField] string currentLevels = null;
    [SerializeField] List<Item> Hunger;
    
    // Wolf Animation
    [SerializeField] Animation _animate;
    private bool changeAnim = false;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject gameEnd;

    // Wolf Interaction
    [SerializeField] private bool isNearWolf = false, isFinished = false, itemAccept = false;
    Item intItem;

    public void resetLevel()
    {
        isFinished = false;
    }

    bool AddItem(Item item)
    {
        if(Hunger.Count == Hunger.Capacity)
        {
            isFinished = true;
            return false;
        }

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
        if (other.tag == "Player") {
            isNearWolf = false;
        }
    }

    void wander()
    {
        float valX = Random.Range(-500, 500);
        float valZ = Random.Range(-500, 500);

        Vector3 newPosition = new Vector3 (valX, this.transform.position.y, valZ);

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(newPosition, path);
        if(path.status == NavMeshPathStatus.PathPartial)
        {
            _animate.PlayQueued("idle", QueueMode.PlayNow);
            wander();
        }
        else
        {
            agent.SetDestination(newPosition);
            _animate.PlayQueued("walk", QueueMode.PlayNow);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        if (!agent.pathPending)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    _animate.PlayQueued("idle", QueueMode.PlayNow);
                    wander();
                }
            }
        }

        if (isFinished)
        {
            //Debug.Log($"Wolf: -> Thank you, I will rest now.. Enjoy the {SceneManager.GetActiveScene().name}'s Village.");
            //Debug.LogError("Game Ended");
            GameOver.isDied = true;
            gameEnd = PlayerCamera.instance.transform.GetChild(2).gameObject;
            gameEnd.SetActive(true);
            MoveCharacter.instance.setPause(true);
            MoveCharacter.instance.setTimeScale(0);
            isFinished = false;

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
