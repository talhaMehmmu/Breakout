using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BallMove : MonoBehaviour
{
    public TextMeshProUGUI continueButtonText;
    public Button continueButton;
    public float launchSpeed;
    private int health = 3;
    public Rigidbody2D rb;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3; 
    public Text restartText;
    public Text scoreText;
    public int score;
    public Text highScoreText;
    public int highScore;

    void Start()
    {
        score = 0;
        restartText.gameObject.SetActive(false);

        rb = GetComponent<Rigidbody2D>();

        // Wait for 1 second before launching the ball
        Invoke("ballLaunch", 1f);

        // Find and assign TextMeshProUGUI and Button components
        // Disable the button initially
        continueButtonText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // Save the new high score
            UpdateHighScoreText();
        }
        UpdateScoreText();
        UpdateHighScoreText();
        // Check if there are no objects with tag "EBrick"
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("EBrick");
        if (bricks.Length == 0)
        {
            continueButtonText.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButtonText.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
        }
        if (health == 2)
        {
            Destroy(heart1);
        }
        else if (health == 1)
        {
            Destroy(heart2);
        }
        else if (health ==0)
        {
            Destroy(heart3);
            restartText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1;
        }
    }

    private void ballLaunch()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        Vector2 launchDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = launchDirection * launchSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity *= 1.1f;
        if (collision.gameObject.tag == "EBrick")
        {
            Destroy(collision.gameObject);
            IncreaseScore(Random.Range(0,10));
        }
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    // Method to update the Text component
    void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score;
    }
    void UpdateHighScoreText()
    {
        highScoreText.text = "HIGHSCORE: " + highScore;
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "GameOver")
        {
            health -= 1;
            Invoke("ballLaunch", 1f); // Invoke ballLaunch after 1 second
        }
    }
}