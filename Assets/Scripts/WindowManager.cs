using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WindowManager : MonoBehaviour{
    public static WindowManager instance;
     // Apenas cria um cabeçalho no Unity para melhor organização
    [Header("Configurações das Janelas")]
    [SerializeField] public GameObject janelaFundo;
    // Referência para o objeto que contém o script PlayerControl
    [SerializeField] public List<GameObject> playerControlObject; 
    // Cria uma lista de objetos para receber todas as janelas informativas do jogo
    [SerializeField] public List<WindowBasic> janelas;
    [SerializeField] public TextMeshProUGUI infoTexto;
    [SerializeField] public UnityEngine.UI.Image imagemObjeto;
    // Variável para controle de quando as cartas são compatíveis para mostrar a janela informativa
    [HideInInspector] public bool hasMatched;
    public int matchedTypeNumber;
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
        if (hasMatched == true)
        {
            StartCoroutine(WindowControl());
        }
    }

    
    void WindowCall()
    {
        ScriptableObject janelaEscolhida = janelas[0];
        // Aqui você pode adicionar a lógica para exibir a janela escolhida
    }


    
    IEnumerator WindowControl(){
        yield return new WaitForSeconds(1f);
        janelaFundo.SetActive(true);
        // Desativa o objeto do PlayerControl
        foreach (GameObject obj in playerControlObject)
        {
            obj.SetActive(false);
        }
        infoTexto.text = janelas[matchedTypeNumber].textoCurso;
        if (imagemObjeto != null)
        {
            imagemObjeto.sprite = janelas[matchedTypeNumber].imagemCurso;
        }
        else
        {
            Debug.LogWarning("WindowManager imagemObjeto is not assigned or is missing the Image component.");
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
