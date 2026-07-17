using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGChanger : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgroundImages;
    [SerializeField] private GameObject background;
    Sprite backgroundImage;
    Sprite actualSprite;
    void Start()
    {
        actualSprite = background.GetComponent<Sprite>();
        BackGroundChanger();
    }

    void Update()
    {
        
    }
    IEnumerator BackGroundChanger()
    {
        Sprite backgroundImage = backgroundImages[0];
        yield return new WaitForSeconds(20.0f);
        actualSprite = backgroundImage;
    }
}
