using System.ComponentModel.Design;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WindowBasic", menuName = "Scriptable Objects/WindowBasic")]
public class WindowBasic : ScriptableObject{
    [SerializeField]
    public string textoCurso = "Texto curso";
    [SerializeField]
    public Sprite imagemCurso;

    private void OnEnable()
    {
        if (imagemCurso == null)
        {
            imagemCurso = Resources.Load<Sprite>("Images/Default");
        }
    }
}


