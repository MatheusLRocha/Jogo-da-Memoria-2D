using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WindowBasic", menuName = "Scriptable Objects/WindowBasic")]
public class WindowBasic : ScriptableObject{
    [SerializeField] public string titulo;

}
