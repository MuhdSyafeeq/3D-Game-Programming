using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    
    // Start is called before the first frame update
    void Awake()
    {
        //gameOverUI.SetActive(false);
    }

    public void TryAgain()
    {
        Destroy(GameObject.Find("*thegameobjecttobedestroyed*"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
