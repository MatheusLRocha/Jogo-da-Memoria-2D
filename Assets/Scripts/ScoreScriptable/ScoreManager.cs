using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<RectTransform> scoreSlots;
    [SerializeField] private List<RectTransform> scoreParents;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private int pageSize = 12;
    [SerializeField] private float slotSpacing = 70f;

    private readonly List<GameObject> spawnedScoreBlocks = new List<GameObject>();
    private readonly List<ScoreEntry> allScores = new List<ScoreEntry>();
    public List<ScoreEntry> sc = new List<ScoreEntry>
    {
        
            new ScoreEntry
            {
                rank = "1",
                username = "Jonhaton",
                points = 298.54f,
                time = 123.34f,
            },
            new ScoreEntry
            {
                rank = "2",
                username = "Golan",
                points = 466.54f,
                time = 78.34f,
            },
            new ScoreEntry
            {
                rank = "3",
                username = "Begarrit",
                points = 1328.54f,
                time = 12.34f,
            },
            new ScoreEntry
            {
                rank = "4",
                username = "Visn",
                points = 4626.54f,
                time = 178.34f,
            },
            new ScoreEntry
            {
                rank = "5",
                username = "Jqswssn",
                points = 122228.54f,
                time = 123.34f,
            },
            new ScoreEntry
            {
                rank = "6",
                username = "Bartrs",
                points = 46.54f,
                time = 728.34f,
            },
            new ScoreEntry
            {
                rank = "7",
                username = "Blimb",
                points = 2298.54f,
                time = 113.34f,
            },
            new ScoreEntry
            {
                rank = "8",
                username = "Jgesn",
                points = 26.54f,
                time = 78.34f,
            },
            new ScoreEntry
            {
                rank = "9",
                username = "Jasda",
                points = 12.54f,
                time = 0.34f,
            },
            new ScoreEntry
            {
                rank = "10",
                username = "Derf",
                points = 466.54f,
                time = 78.34f,
            },
            new ScoreEntry
            {
                rank = "11",
                username = "Cica",
                points = 1298.54f,
                time = 123.34f,
            },
            new ScoreEntry
            {
                rank = "12",
                username = "Vincan",
                points = 466.54f,
                time = 78.34f,
            },
            new ScoreEntry
            {
                rank = "13",
                username = "Blue-Palms",
                points = 1298.54f,
                time = 123.34f,
            },
            new ScoreEntry
            {
                rank = "14",
                username = "Gingerboy",
                points = 42266.54f,
                time = 78.34f,
            }
        
    };
    private int currentPage = 0;

    private void Awake()
    {
        SetScores(sc);
    }

    public void SetScores(List<ScoreEntry> scores)
    {
        allScores.Clear();

        if (scores != null)
        {
            allScores.AddRange(scores);
        }

        //allScores.Sort((a, b) => b.points.CompareTo(a.points));
        currentPage = 0;
        RenderCurrentPage();
    }

    public void ShowNextPage()
    {
        if (GetTotalPages() <= 1)
            return;

        currentPage = Mathf.Min(currentPage + 1, GetTotalPages() - 1);
        RenderCurrentPage();
    }

    public void ShowPreviousPage()
    {
        if (GetTotalPages() <= 1)
            return;

        currentPage = Mathf.Max(currentPage - 1, 0);
        RenderCurrentPage();
    }

    public void ShowPage(int pageIndex)
    {
        if (GetTotalPages() <= 0)
            return;

        currentPage = Mathf.Clamp(pageIndex, 0, GetTotalPages() - 1);
        RenderCurrentPage();
    }

    public int GetTotalPages()
    {
        if (pageSize <= 0)
            return 0;

        return Mathf.CeilToInt(allScores.Count / (float)pageSize);
    }

    private void RenderCurrentPage()
    {
        ClearScoreBlocks();

        if (scorePrefab == null)
            return;

        int startIndex = currentPage * pageSize;
        int endIndex = Mathf.Min(startIndex + pageSize, allScores.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            int slotIndex = i - startIndex;

            RectTransform slot = null;
            if (scoreSlots.Count > 0)
            {
                int slotListIndex = Mathf.Min(slotIndex, scoreSlots.Count - 1);
                slot = scoreSlots[slotListIndex];
            }

            int parentIndex = Mathf.Min(slotIndex, Mathf.Max(0, scoreParents.Count - 1));
            Transform parentTransform = scoreParents.Count > 0 ? scoreParents[parentIndex] : transform;

            Vector3 targetPosition = slot != null ? slot.position : transform.position;
            if (slot != null && slotIndex >= scoreSlots.Count)
            {
                targetPosition = scoreSlots[scoreSlots.Count - 1].position + Vector3.down * slotSpacing * (slotIndex - scoreSlots.Count + 1);
            }

            GameObject scoreObject = Instantiate(scorePrefab, targetPosition, Quaternion.identity, parentTransform);
            scoreObject.transform.SetParent(parentTransform, false);
            scoreObject.transform.position = targetPosition;

            PopulateScoreBlock(scoreObject, allScores[i], i + 1);
            spawnedScoreBlocks.Add(scoreObject);
        }
    }

    private void ClearScoreBlocks()
    {
        for (int i = spawnedScoreBlocks.Count - 1; i >= 0; i--)
        {
            if (spawnedScoreBlocks[i] != null)
            {
                Destroy(spawnedScoreBlocks[i]);
            }
        }

        spawnedScoreBlocks.Clear();
    }

    private void PopulateScoreBlock(GameObject scoreObject, ScoreEntry entry, int rank)
    {
        if (entry == null)
            return;

        TextMeshProUGUI[] texts = scoreObject.GetComponentsInChildren<TextMeshProUGUI>(true);

        if (texts.Length == 0)
            return;

        if (texts.Length > 0)
        {
            texts[0].text = $"{rank}. {entry.username}";
        }

        if (texts.Length > 1)
        {
            texts[1].text = entry.points.ToString();
        }

        if (texts.Length > 2)
        {
            texts[2].text = entry.time.ToString() + "s";
        }
    }
}

[System.Serializable]
public class ScoreEntry
{
    public string rank;
    public string username;
    public float points;
    public float time;
}
