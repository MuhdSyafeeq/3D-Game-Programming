using UnityEngine;
using UnityEngine.SceneManagement;

public class Marker : MonoBehaviour
{
    #region Singleton Method
    public static Marker instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance(s) of Inventory found!");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    #endregion


    [SerializeField] GameManager gManager;
    [SerializeField] string currentActiveScene = null;
    
    [SerializeField] private float inTimer;
    [SerializeField] private bool isAccess = false, isEntered = false;

    private void LateUpdate()
    {
        currentActiveScene = SceneManager.GetActiveScene().name;

        if (isAccess && !isEntered)
        {
            isEntered = true;
            isAccess = false;
            inTimer = 0;

            if(currentActiveScene == "Milestone1")
            {
                gManager.saveProgress(1);
            }
            else if(currentActiveScene == "House-Interior-A")
            {
                gManager.saveProgress(0);
            }
        }
    }


    private void OnTriggerStay(Collider other)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "House")
        {
            isEntered = false;
            isAccess = false;
            inTimer = 0;
        }
    }
}
