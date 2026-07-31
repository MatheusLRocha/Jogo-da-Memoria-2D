using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Net;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> scoreNames;
    [SerializeField] private List<TextMeshProUGUI> scorePoints;
    [SerializeField] private List<TextMeshProUGUI> scoreTime;
    [SerializeField] private List<RectTransform> scoresPosition;
    [SerializeField] private List<GameObject> scores;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private List<RectTransform> parent;

        public void Awake()
            {
                InstantiateScores();
            }
        public void InstantiateScores()
    {
        // Grande loop que randomiza as cartas ao mesmo tempo que cria elas
        for (int i = scoresPosition.Count - 1; i > -1; i--)
        {
            scores[i] = Instantiate(scorePrefab, scoresPosition[i].position, Quaternion.identity, parent[i]);
        }
    }

}
