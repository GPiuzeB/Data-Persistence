using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highScoreText;
    public Text namePlayerHSText;
    public TMP_InputField nameBox;
    public GameObject GameOverText;
    public ScenePersistenceManager ScenePersistenceManager;
    
    private bool m_Started = false;
    private int m_Points;
    public string HS;
    
    private bool m_GameOver = false;
    private bool highScoreUp = false;


    // Start is called before the first frame update
    void Start()
    {
        ScenePersistenceManager = GameObject.Find("Scene Persistence Manager").GetComponent<ScenePersistenceManager>();
        HS = nameBox.GetComponent<TMP_InputField>().text;

        highScoreText.text = "Highscore : " + ScenePersistenceManager.highScore;
        namePlayerHSText.text = "Name :  " + ScenePersistenceManager.nameHS;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started && !highScoreUp)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver && !highScoreUp)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartScene();
            }
        }
    }

    public void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SetHighScore();
    }

    public void SetHighScore()
    {
        if(m_Points > ScenePersistenceManager.highScore)
        {
            ScenePersistenceManager.highScore = m_Points;
            nameBox.gameObject.SetActive(true);
            highScoreUp = true;
        }
    }

    public void SetHSName()
    {
        ScenePersistenceManager.nameHS = HS;
        RestartScene();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
