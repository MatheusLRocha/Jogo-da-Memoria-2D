using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowManager : MonoBehaviour{
    public static WindowManager instance;

    [Header("Configurações das Janelas")]

    [SerializeField] public GameObject windowBackgroundReference;

    [SerializeField] public List<GameObject> playerControlObject; 

    [SerializeField] public List<WindowBasic> windows;

    [SerializeField] public TextMeshProUGUI informationText;

    [SerializeField] public UnityEngine.UI.Image objectImage;

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
        windowBackgroundReference.SetActive(false);
        anim = windowBackgroundReference.GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica todo frame se a janela foi ativada
        if (IsMatchedCards())
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
        ActivateWindow();

        // Coloca no texto da janela o valor do texto do respectivo scriptable object
        SetDescriptionText();

        // Verifica se existe uma imagem para ser colocada do card na janela
        SetImageOnWindow();

        // Se não deu match, a janela continua desativada e os players podem mexer nas cartas
        if (!IsMatchedCards())
        {
            StartCoroutine(DesactivateWindow());
        }    
    }

    private void ActivateWindow()
    {
        windowBackgroundReference.SetActive(true);
        anim.SetBool("Activated", true);
        isWindowActive = true;
    }

    IEnumerator DesactivateWindow()
    {
        anim.SetBool("Activated", false);

        yield return new WaitForSeconds(1.3f);

        windowBackgroundReference.SetActive(false);

        isWindowActive = false;
        hasIncrementedThisActivation = false; // Reseta a flag para a próxima ativação

        if (finaleActivator == 13)
        {
            TelaFinal.SetActive(true);
        }

        yield return new WaitForSeconds(1.3f);

        changer = true;
    }

    private void SetDescriptionText()
    {
        informationText.text = windows[matchedTypeNumber].textoCurso;
    }

    private void SetImageOnWindow()
    {
        if (objectImage != null)
            objectImage.sprite = windows[matchedTypeNumber].imagemCurso;
        else
            Debug.LogWarning("WindowManager objectImage não contém uma imagem atribuida.");
    }

    private bool IsMatchedCards()
    {
        return hasMatched;
    }
}
