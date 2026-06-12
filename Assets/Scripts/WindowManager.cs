using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    //Variável que bloqueia as cartas durante a ativação da janela
    public bool isWindowActive = false;
    public int finaleActivator = 0;
    public bool changer = true;
    private bool hasIncrementedThisActivation = false; // Rastreia se já incrementou nesta ativação
    [SerializeField] public GameObject TelaFinal;
    Animator anim;
    
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
        anim = janelaFundo.GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica todo frame se a janela foi ativada
        if (hasMatched == true)
        {
            StartCoroutine(WindowControl());
        }
        
        // Incrementa finaleActivator apenas uma vez quando a janela fica ativa
        // Este código roda todos os frames, mas só incrementa quando as condições são atendidas
        if (isWindowActive == true && hasIncrementedThisActivation == false)
        {
            finaleActivator++;
            hasIncrementedThisActivation = true; // Bloqueia futuras ativações
        }
    }

    

    
    IEnumerator WindowControl(){
        
        // Espera 0,5 segundos para mostrar a janela
        yield return new WaitForSeconds(0.5f);

        // Ativa a janela
        janelaFundo.SetActive(true);
        anim.SetBool("Activated", true);
        isWindowActive = true;

        // Coloca no texto da janela o valor do texto do respectivo scriptable object
        infoTexto.text = janelas[matchedTypeNumber].textoCurso;

        // Verifica se existe uma imagem para ser colocada do card na janela
        if (imagemObjeto != null)
        {
            imagemObjeto.sprite = janelas[matchedTypeNumber].imagemCurso;
        }
        else
        {
            Debug.LogWarning("WindowManager imagemObjeto não contém uma imagem atribuida.");
        }

        // Se não deu match, a janela continua desativada e os players podem mexer nas cartas
        if (hasMatched == false)
        {
            anim.SetBool("Activated", false);
            yield return new WaitForSeconds(1.3f);
            janelaFundo.SetActive(false);
            isWindowActive = false;
            hasIncrementedThisActivation = false; // Reseta a flag para a próxima ativação
            if (finaleActivator == 13)
            {
                TelaFinal.SetActive(true);
            }
            yield return new WaitForSeconds(1.3f);
            changer = true;
        }    
    }


}
