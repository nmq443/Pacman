using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] GameObject creditPanel;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (creditPanel != null)
            creditPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game!");
    }

    public void Menu()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }

    public void Credit()
    {
        if (creditPanel != null)
            creditPanel.SetActive(true);
    }

    public void Return()
    {
        if (creditPanel != null) 
            creditPanel.SetActive(false);
    }
}
