using TMPro;
using UnityEngine;

public class WindowManagerScore : MonoBehaviour{
    public static WindowManagerScore instance;

    [Header("Configurações das Janelas")]

    [SerializeField] public GameManager gameManager; 
    public bool isWindowActive = false;
    public int finaleActivator = 0;
    [SerializeField] public GameObject Points;

    [SerializeField] public TimerManager _timerManager;

    [SerializeField] public GameObject finalScreen;
    [SerializeField] public TextMeshProUGUI finalRankText;
    [SerializeField] public GameObject failureScreen;
    [SerializeField] public TextMeshProUGUI failureRankText;
    [SerializeField] public CompetitiveManager competitiveManager;
    private SQLiteDataBase _db;
    private PlayerModel actualPlayer;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        finaleActivator = gameManager.finaleActivator;

        if (gameManager.finaleActivator == 13)
        {
            actualPlayer = GetCurrentPlayer();

            if (actualPlayer != null)
            {
                finalRankText.text = BuildScoreText(actualPlayer);
            }

            isWindowActive = true;
            finalScreen.SetActive(true);
            Points.SetActive(false);
        }
        else if (_timerManager.GetTime() >= 150.0f)
        {
            actualPlayer = GetCurrentPlayer();

            if (actualPlayer != null)
            {
                failureRankText.text = BuildScoreText(actualPlayer);
            }

            isWindowActive = true;
            failureScreen.SetActive(true);
            Points.SetActive(false);
        }
    }

    private PlayerModel GetCurrentPlayer()
    {
        if (competitiveManager != null && competitiveManager.player != null)
        {
            return competitiveManager.player;
        }

        if (competitiveManager != null && competitiveManager._db != null)
        {
            string username = PlayerPrefs.GetString("Username");
            return competitiveManager._db.GetPlayerByName(username);
        }

        return null;
    }

    private string BuildScoreText(PlayerModel player)
    {
        string points = player.Points.ToString("0.##");
        string time = player.Time.ToString("0.##");

        return $"Você fez\n{points} pts\nem {time} segundos";
    }
}
