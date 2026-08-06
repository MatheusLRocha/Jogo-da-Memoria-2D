using System.Collections;
using System.Collections.Generic;
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
            isWindowActive = true;
            finalScreen.SetActive(true);
            //finalRankText = "Seu rank foi" + rank + tempo + pontos
            Points.SetActive(false);
        }
        else if (_timerManager.GetTime() >= 150.0f)
        {
            isWindowActive = true;
            failureScreen.SetActive(true);
            //failureRankText = "Seu rank foi" + rank + tempo + pontos
            Points.SetActive(false);
        }   
    }
}
