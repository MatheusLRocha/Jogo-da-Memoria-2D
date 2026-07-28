using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Unity.UI;

public class BGChanger : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgroundSprites;
    [SerializeField] private Image backgroundImages;
    private int currentIndex = 0;

    private void Start()
    {
        if (backgroundSprites == null || backgroundSprites.Count == 0 || backgroundImages == null)
        {
            Debug.LogWarning("BGChanger: Missing background sprites or image reference.");
            return;
        }

        backgroundImages.sprite = backgroundSprites[currentIndex];
        StartCoroutine(BackgroundChangerCoroutine());
    }

    private IEnumerator BackgroundChangerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20.0f);
            currentIndex = (currentIndex + 1) % backgroundSprites.Count;
            backgroundImages.sprite = backgroundSprites[currentIndex];
        }
    }
}

