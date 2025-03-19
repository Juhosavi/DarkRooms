using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseAndInventoryMenu : MonoBehaviour
{
    public Article article;
    public  bool gameIsPaused = false;//bool indicating if the game is paused
    public GameObject pauseMenuUi;// reference to the pause menu UI
    public Examine examine;// reference to the Examine script

    void Start()
    {
        article = GameObject.FindGameObjectWithTag("Article").GetComponent<Article>();
        examine = FindObjectOfType<Examine>();// find the Examine script in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))// if the Escape key is pressed
        {
            if(gameIsPaused)// if the game is already paused
            {
                Resume();// resume the game
            }
            else
            {
                Pause();// pause the game
            }
        } 
        if(examine.examineMode == true && gameIsPaused)// if examine mode is active and the game is paused
        {
            Resume();// resume the game and exit examine mode
            examine.examineMode = false;
        }
    }
   public void Resume()// method to resume the game
    {
        pauseMenuUi.SetActive(false);// hide the pause menu UI

        // if (article.articleText.activeInHierarchy == false)
        {
            Time.timeScale = 1f;// set the time scale to normal speed
        }

        gameIsPaused = false;// set the gameIsPaused bool to false
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()// method to pause the game
    {
        pauseMenuUi.SetActive(true);// show the pause menu UI
        Time.timeScale = 0f;// set the time scale to 0, effectively pausing the game
        gameIsPaused = true;// set the gameIsPaused bool to true
        Cursor.lockState = CursorLockMode.None;
    }
    public void QuitGame()// method to quit the game
    {
        Application.Quit();// quit the game(only works in a built executable, not in the editor)
    }
}
