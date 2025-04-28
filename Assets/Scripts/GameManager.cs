using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int trashCleaned = 0;         // 정화한 오염물 수
    public int targetTrashCount = 10;    // 목표 오염물 수
    public int hp = 3;                   // 체력
    public float timeRemaining = 60f;    // 제한 시간 (초)

    public TextMeshProUGUI trashCountText;  // 수정
    public TextMeshProUGUI timeText;         // 수정
    public TextMeshProUGUI hpText;    
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            GameOver();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (trashCountText != null)
            trashCountText.text = $"clear: {trashCleaned}/{targetTrashCount}";

        if (timeText != null)
            timeText.text = $"time: {Mathf.Ceil(timeRemaining)}";

        if (hpText != null)
            hpText.text = $"HP: {hp}";
    }

    public void AddTrashCleaned(int amount)
    {
        if (isGameOver) return;

        trashCleaned += amount;
        UpdateUI();

        if (trashCleaned >= targetTrashCount)
        {
            GameClear();
        }
    }

    public void LoseHp(int amount)
    {
        if (isGameOver) return;

        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            GameOver();
        }
        UpdateUI();
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over...");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private void GameClear()
    {
        isGameOver = true;
        Debug.Log("Game Clear!");
        if (gameClearPanel != null)
            gameClearPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
