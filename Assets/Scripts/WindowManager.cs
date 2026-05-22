using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WindowManager : MonoBehaviour{
    // Cria instância do script, o que permite com que outros scripts possam mexer aqui
    public static WindowManager instance;

     // Apenas cria um cabeçalho no Unity para melhor organização
    [Header("Configurações das Janelas")]

    // Permite conectar o molde da janela no seu respectivo gerenciador
    [SerializeField] public GameObject janelaFundo;

    // Referência para os objetos que contém o script PlayerControl
    [SerializeField] public List<GameObject> playerControlObject; 

    // Cria uma lista de objetos para receber todos os scriptable objects para usar nas janelas
    [SerializeField] public List<WindowBasic> janelas;

    // Referência para mexer no texto da janela(WindowText)
    [SerializeField] public TextMeshProUGUI infoTexto;

    // Referência para mexer na imagem do curso da janela(WindowImage)
    [SerializeField] public UnityEngine.UI.Image imagemObjeto;

    // Variável para controle de quando as cartas são compatíveis para mostrar a janela informativa
    [HideInInspector] public bool hasMatched;

    // Variável para pegar o índice da carta matched
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
        // Verifica todo frame se a janela foi ativada
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
        // Espera 0,5 segundos para mostrar a janela
        yield return new WaitForSeconds(0.5f);

        // Ativa a janela
        janelaFundo.SetActive(true);

        // Desativa os objetos do PlayerControl
        foreach (GameObject obj in playerControlObject)
        {
            // Não permite mexer nas cartas enquanto a janela estiver habilitada
            obj.SetActive(false);
        }

        // Coloca no texto da janela o valor do texto do respectivo scriptable object
        infoTexto.text = janelas[matchedTypeNumber].textoCurso;

        // Verifica se existe uma imagem para ser colocada do card na janela
        if (imagemObjeto != null)
        {
            imagemObjeto.sprite = janelas[matchedTypeNumber].imagemCurso;
        }
        else
        {
            Debug.LogWarning("WindowManager imagemObjeto is not assigned or is missing the Image component.");
        }

        // Se não deu match, a janela continua desativada e os players podem mexer nas cartas
        if (hasMatched == false)
        {
            janelaFundo.SetActive(false);

            // Ativa os objetos do PlayerControl
            foreach (GameObject obj in playerControlObject)
            {
                obj.SetActive(true);
            }
        }    
    }


}
