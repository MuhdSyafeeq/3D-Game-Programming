using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Method
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] GameObject[] objects;
    [SerializeField] bool lvlAvail = false;

    public void saveProgress(int sceneBuild)
    {
        Debug.Log($"Saving...");
        
        foreach(GameObject @object in objects)
        {
            DontDestroyOnLoad(@object);
        }
        DontDestroyOnLoad(this.gameObject);
        
        try
        {
            SceneManager.LoadSceneAsync(sceneBuild);
            lvlAvail = true;
        }
        catch (UnityException e)
        {
            Debug.Log($"Level int {sceneBuild}: {e.Message}");
            lvlAvail = false;
        }

        if (lvlAvail)
        {
            SceneManager.LoadSceneAsync(sceneBuild);
            lvlAvail = false;
        }
    }

}
