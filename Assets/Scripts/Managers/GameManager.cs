using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public CanvasGroup gameOverPanel;
    public TextMeshProUGUI gameOverText;
    private bool isGameOver = false;
    public List<Monster> monsters = new List<Monster>();
    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }

    private void Awake()
    {
        Time.timeScale = 1f;
        gameOverPanel.gameObject.SetActive(false);
    }
    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        Application.targetFrameRate = int.MaxValue;
#endif
    }
    private void Update()
    {
        if (IsGameOver)
            GameOverPanelFadeIn();

        if(gameOverPanel.alpha >= 1f && Input.touchCount == 1)
        {
            SceneManager.LoadScene(0);
        }
#if UNITY_EDITOR
        if(gameOverPanel.alpha >= 1f && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
#endif
    }

    public void OnGameOver()
    {
        IsGameOver = true;
        gameOverPanel.gameObject.SetActive(true);
    }
    private void GameOverPanelFadeIn()
    {
        if(gameOverPanel.enabled && gameOverPanel.alpha < 1f)
        {
            gameOverPanel.alpha += Time.deltaTime * 0.5f;
            gameOverText.alpha += Time.deltaTime;
        }
    }
    public void UpdateMonsterList()
    {
        foreach (var monster in monsters)
        {
            if (monster.isDie)
                monsters.Remove(monster);
        }
    }
}
