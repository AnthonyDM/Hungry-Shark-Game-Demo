using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public int life;
    public int score;
    public int highScore;

    public List<GameObject> lifeHearts;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject beginningGamePanel;
    public GameObject gameOverPanel;

    public GameObject shark;
    public Transform sharkPositionTop;
    public Transform sharkPositionBottom;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

    public GameObject fishSpawner;
    public GameObject eatParticlePrefab;
    public Transform eatParticlePosition;

    public Animator cameraAnimator;
    public Animator sharkAnimator;

    public AudioSource hurtSound;
    public AudioSource gameOverSound;

    public bool isGameActive;

    void Start()
    {
        fishSpawner.SetActive(false);
        beginningGamePanel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isGameActive)
        {
            targetPosition = sharkPositionTop.TransformPoint(new Vector3(0, 0, 0));
        }
        else
        {
            targetPosition = sharkPositionBottom.TransformPoint(new Vector3(0, 0, 0));
        }

        shark.transform.position = Vector3.SmoothDamp(shark.transform.position, targetPosition, ref velocity, 0.3f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitButton();
        }
    }

    public void NewGame()
    {
        beginningGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        life = 3;
        score = 0;
        UpdateLife();
        UpdateScore();
        isGameActive = true;
        fishSpawner.SetActive(true);
        var leftOverFish = GameObject.FindGameObjectsWithTag("Fish");
        if (leftOverFish.Length > 0)
        {
            foreach (GameObject fish in leftOverFish)
            {
                Destroy(fish);
            }
        }
    }

    public void GainScore(int amount)
    {
        score += amount;

        Instantiate(eatParticlePrefab, eatParticlePosition.transform.position, transform.rotation);
        sharkAnimator.Play("eat", 0, 0);
        if (score > highScore)
        {
            highScore = score;
        }

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString("0");
        highScoreText.text = "High Score: " + highScore.ToString("0");
    }

    public void LoseLife()
    {
        life--;
        cameraAnimator.Play("shake", 0);
        hurtSound.Play();
        if (life <= 0)
        {
            EndGame();
            gameOverSound.Play();
            life = 0;
        }

        UpdateLife();
    }

    void UpdateLife()
    {
        foreach (GameObject lifeHeart in lifeHearts)
        {
            lifeHeart.SetActive(false);
        }

        for (int i = 0; i < life; i++)
        {
            lifeHearts[i].SetActive(true);
        }
    }

    void EndGame()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
        fishSpawner.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
