using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    // Referência para o objeto que contém o script PlayerControl
    [SerializeField] public List<GameObject> playerControlObject; 
    // Cria uma lista de objetos para receber todas as janelas informativas do jogo
    [SerializeField] public List<ScriptableObject> janelas;
    [SerializeField] public TextMeshProUGUI textMeshPro;
    [SerializeField] public GameObject ImageObject;
    
    // Variável para controle de quando as cartas são compatíveis para mostrar a janela informativa
    [HideInInspector] public bool hasMatched;

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
        // Deixa como padrão a inatividade das janelas informativas
        janelaFundo.SetActive(false);
    }

    void Update()
    {
        WindowControl();
    }

    // Função para controlar a ativação da janela informativa
    void WindowControl()
    {
        if (hasMatched == true)
        {
            StartCoroutine(WindowTimer());
        }
    }
    
    void WindowCall()
    {
        ScriptableObject janelaEscolhida = janelas[0];
        // Aqui você pode adicionar a lógica para exibir a janela escolhida
    }


    
    IEnumerator WindowTimer(){
        yield return new WaitForSeconds(1f);
        janelaFundo.SetActive(true);
        // Desativa o objeto do PlayerControl
        foreach (GameObject obj in playerControlObject)
        {
            obj.SetActive(false);
        }
        if (hasMatched == false)
        {
            janelaFundo.SetActive(false);
            // Ativa o objeto do PlayerControl
            foreach (GameObject obj in playerControlObject)
            {
                obj.SetActive(true);
            }
        }    
    }


}
