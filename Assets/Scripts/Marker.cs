using UnityEngine;
using UnityEngine.SceneManagement;

public class Marker : MonoBehaviour
{
    
    public static Marker instance;
    void Awake()
    {
        // Collider Transfering
        foreach(Transform child in transform)
        {
            Collider collide = child.GetComponent<BoxCollider>();
            if (collide.gameObject != gameObject)
            {
                ColliderBridge cb = collide.gameObject.AddComponent<ColliderBridge>();
                cb.Initialize(this);
            }
        }
        // Collider Transfering

        #region Singleton Method
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        instance = this;
        #endregion
    }
    
    [SerializeField] GameManager gManager;
    [SerializeField] string currentActiveScene = null;
    
    [SerializeField] private float inTimer;
    [SerializeField] private bool isAccess = false, isEntered = false;
    [SerializeField] private Transform player_;

    private void LateUpdate()
    {
        currentActiveScene = SceneManager.GetActiveScene().name;

        if (isAccess && !isEntered)
        {
            isEntered = true;
            isAccess = false;
            inTimer = 0;
            player_.position += (player_.forward*(float)2.5);

            if(currentActiveScene == "Asset_Copy_Milestone_1")
            {
                gManager.saveProgress(1);
            }
            else if(currentActiveScene == "House-Interior-A")
            {
                gManager.saveProgress(3);
            }

            //if (currentActiveScene == "Milestone 1")
            //{
            //    gManager.saveProgress(1);
            //}
            //else if (currentActiveScene == "House-Interior-A")
            //{
            //    gManager.saveProgress(0);
            //}
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag != "House")
        {
            //Debug.Log("TriggeredStay: Entering -> " + other.name);
            if(other.tag == "Player")
            {
                if(inTimer >= 2f) { isAccess = true; }

                inTimer += Time.deltaTime;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "House")
        {
            isEntered = false;
            isAccess = false;
            inTimer = 0;
        }
    }
}

public class ColliderBridge : MonoBehaviour
{
    Marker _listener;
    public void Initialize(Marker l)
    {
        _listener = l;
    }
    void OnTriggerStay(Collider collision)
    {
        _listener.OnTriggerStay(collision);
    }
    void OnTriggerExit(Collider other)
    {
        _listener.OnTriggerExit(other);
    }
}
