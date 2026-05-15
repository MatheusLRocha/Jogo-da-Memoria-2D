using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WindowManager : MonoBehaviour{
    public static WindowManager instance;
     // Apenas cria um cabeçalho no Unity para melhor organização
    [Header("Configurações das Janelas")]
    [SerializeField] public GameObject janelaFundo;
    // Cria uma lista de objetos para receber todas as cartas do jogo
    [SerializeField] public List<ScriptableObject> janelas;
    
    public bool hasMatched;

     void Awake()
    {
        // Verifica se já existe uma cópia da instância ou não
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        janelaFundo.SetActive(false);
    }

    void Update()
    {
        WindowControl();
    }

    void WindowControl()
    {
        if (hasMatched == true)
        {
            janelaFundo.SetActive(true);
        }
    }




}
