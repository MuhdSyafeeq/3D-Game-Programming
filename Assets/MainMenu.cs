using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    bool loadLevel = false;

    public void setLoadLevel(bool result)
    {
        loadLevel = result;
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void QuitLevel()
    {
        Application.Quit();
    }

    public static void DestroyAll()
    {
        Destroy(MoveCharacter.instance.gameObject);
        Destroy(PlayerCamera.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(Clock.instance.gameObject);
        Destroy(Marker.instance.gameObject);
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        if (loadLevel) {
            MoveCharacter.instance.LoadData();
            loadLevel = false;
        }
    }
}
