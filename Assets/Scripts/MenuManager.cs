using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public ScenePersistenceManager scenePersistenceManager;

    // Start is called before the first frame update

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
