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
    public int currentStage = 2;
    public CanvasGroup gameOverPanel;
    public TextMeshProUGUI gameOverText;
    private bool isGameOver = false;
    public bool isClearAble = false;
    public List<Monster> monsters = new List<Monster>();
    private EliteMonster eliteMonster;
    private BossMonster bossMonster;
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
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = int.MaxValue;
#endif
        eliteMonster = GameObject.FindGameObjectWithTag(Tags.Elite)?.GetComponent<EliteMonster>() ?? null;
        bossMonster = GameObject.FindGameObjectWithTag(Tags.Boss)?.GetComponent<BossMonster>() ?? null;

        isClearAble = false;
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

    public void OnClear()
    {
        isClearAble = true;
    }
}
