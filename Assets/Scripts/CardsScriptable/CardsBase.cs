using UnityEngine;

/// <summary>
/// CardsBase é um Scriptable Object que armazena DADOS de um curso específico.
/// 
/// Responsabilidades:
/// - Armazenar a DESCRIÇÃO do curso como texto
/// - Armazenar a IMAGEM do curso como Sprite
/// - NÃO se preocupar com renderização (isso é responsabilidade da UI/TextMeshPro)
/// - Ser um "banco de dados" que qualquer script pode acessar
/// </summary>

[CreateAssetMenu(fileName = "Curso_", menuName = "Scriptable Objects/CardsBase")]
public class CardsBase : ScriptableObject
{
    [Header("Informações do Curso")]
    [SerializeField] private string descricao;         // Descrição completa do curso
    [SerializeField] private Sprite imagemBase;        // Imagem/Ícone do curso

    // ======================== GETTERS (Leitura de Dados) ========================

    /// <summary>
    /// Retorna a descrição do curso como STRING PURA
    /// (O TextMeshPro será responsável por renderizar essa string depois)
    /// </summary>
    public string GetDescricao() => descricao;

    /// <summary>
    /// Retorna a imagem do curso
    /// </summary>
    public Sprite GetImagemBase() => imagemBase;

    // ======================== VALIDAÇÃO ========================
    
    private void OnValidate()
    {
        // Valida se há imagem atribuída
        if (imagemBase == null)
        {
            Debug.LogWarning($"[{name}] Nenhuma imagem atribuída! Carregando imagem padrão...");
            imagemBase = Resources.Load<Sprite>("Images/Default");
        }

        // Valida se há descrição
        if (string.IsNullOrEmpty(descricao))
        {
            Debug.LogWarning($"[{name}] Nenhuma descrição atribuída!");
        }
    }
}


