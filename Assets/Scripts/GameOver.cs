using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    public static bool isDied = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        //gameOverUI.SetActive(false);
    }

    public void TryAgain()
    {
        GameManager.instance.saveProgress(SceneManager.GetActiveScene().buildIndex);
        MoveCharacter.instance.setPause(false);
        MoveCharacter.instance.setTimeScale(1);
        gameOverUI.SetActive(false);
        MainMenu.DestroyAll();
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
