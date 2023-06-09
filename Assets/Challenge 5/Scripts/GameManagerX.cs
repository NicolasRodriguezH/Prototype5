using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public GameObject titleScreen;
    // El Button se referencia gracias a la libreria UnityEngine.UI
    public Button restartButton; 

    // Declaracion de lista para el prefab de targets
    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    public bool isGameActive;

    private int sumarTiempo = 2;
    public float timer = 5;

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    private void Start()
    {
        Time.timeScale = 0;
        // StartGame(0);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "" + timer.ToString("Timer: 0");

        Debug.Log(timer);

        if(timer < 0)
        {
            GameOver();
        }
    }

    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int dificulty)
    {

        isGameActive = true;
        score = 0;
        spawnRate /= dificulty;
        
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        //UpdateTimer();
        titleScreen.SetActive(false);
        Time.timeScale = 1;
    }


    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "score" + score;
    }

    // Suma tiempo al timer
    public void UpdateTimer()
    {
        timer += sumarTiempo;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
            Time.timeScale = 0;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
