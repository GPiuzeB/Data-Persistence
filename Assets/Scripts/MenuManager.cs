using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public ScenePersistenceManager scenePersistenceManager;
    public TMP_Text highscoreText;
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        scenePersistenceManager = GameObject.Find("Scene Persistence Manager").GetComponent<ScenePersistenceManager>();

        highscoreText.text = "Current Highscore" + scenePersistenceManager.nameHS + " - " + scenePersistenceManager.highScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
