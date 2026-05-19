using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;

    [SerializeField] GameObject startGamePanel, gameOverPanel;

    [SerializeField] TextMeshProUGUI scoreText, gameOverScoreTxt, gameOverHighScoreTxt, startGameHighScoreTxt;
    PlayerScript playerScr;

    int score = 0;

    int loadedHighScore;

    bool hasGameStarted = false;
    bool hasGameOver = false;

    private void Awake()
    {
        InvokeRepeating("SpawnObstacle", 0f, 1.5f);
        playerScr = Object.FindObjectOfType<PlayerScript>();

        loadedHighScore = PlayerPrefs.GetInt("highscore");
        startGameHighScoreTxt.SetText(loadedHighScore.ToString());
    }

    public void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(!hasGameStarted)
            {
                hasGameStarted = true;
                playerScr.StartGame();
                startGamePanel.SetActive(false);
            }
            else if(hasGameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void SpawnObstacle()
    {
        if (hasGameStarted && !hasGameOver)
        {
            Vector2 playerPos = playerScr.GetPosition();
            GameObject tmpObstacle = Instantiate(obstaclePrefab, new Vector2(playerPos.x + 8, Random.Range(-3f, 3f)), Quaternion.identity);
        }
    }

    public void scorePoint()
    {
        score++;
        //Debug.Log("Score: " + score);
        scoreText.SetText(score.ToString());
    }

    public void GameOver()
    {
        hasGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverScoreTxt.SetText(score.ToString());
        //gameOverHighScoreText.SetText(PlayerPrefs.GetInt("highscore").ToString());

        
        if(score > loadedHighScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            gameOverHighScoreTxt.SetText(score.ToString());
        } else
        {
            gameOverHighScoreTxt.SetText(loadedHighScore.ToString());
        }
    }
}
