using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            foreach (GameObject @object in objects)
            {
                DontDestroyOnLoad(@object);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] GameObject[] objects;
    [SerializeField] bool lvlAvail = false;

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public void saveProgress(int sceneIndex)
    {
        Debug.Log($"Saving...");
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void ExitLevel(int sceneIndex)
    {
        if(!GameOver.isDied) { SaveSystem.SavePlayer(); }
        
        StartCoroutine(LoadAsynchronously(sceneIndex));
        MoveCharacter.instance.setPause(false);
        MoveCharacter.instance.setTimeScale(1);
        MainMenu.DestroyAll();
        GameOver.isDied = false;
        
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        foreach (GameObject @object in objects)
        {
            DontDestroyOnLoad(@object);
        }
        DontDestroyOnLoad(this.gameObject);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
