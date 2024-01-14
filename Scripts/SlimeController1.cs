using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeController1 : MonoBehaviour
{
    public Text dialogueText;
    public int damageAmount;
    public int moedasDropped; // Quantidade de moedas que o Slime solta ao ser derrotado.

    private bool isDialogActive = false;
    private float questionDelay = 2f; // Tempo de atraso entre as perguntas.
    private float questionTimer = 0f;
    private Image dialogueBackgroundImage; // Referência à imagem de fundo da barra de diálogo.
    private bool playerDetected = false;

    // Variáveis para a pergunta de adição.
    private int num1;
    private int num2;
    private int correctAnswer;

    // Variável para armazenar a resposta do jogador.
    private string playerResponse = "";

    private void Start()
    {
        // Inicializa o temporizador da pergunta.
        questionTimer = questionDelay;

        // Obtém a referência à imagem de fundo da barra de diálogo.
        dialogueBackgroundImage = GetComponentInChildren<Image>();

        // Desativa a imagem de fundo da barra de diálogo no início.
        if (dialogueBackgroundImage != null)
        {
            dialogueBackgroundImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerDetected && !isDialogActive)
        {
            // Controle automático das perguntas com temporizador.
            questionTimer -= Time.deltaTime;

            if (questionTimer <= 0f)
            {
                // É hora de fazer uma pergunta.
                AskQuestion();
            }
        }
        else if (isDialogActive)
        {
            // Aguarda a resposta do jogador e verifica se Enter/Return foi pressionado.
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Verifica se a resposta do jogador está correta.
                if (playerResponse.Trim().Equals(correctAnswer.ToString()))
                {
                    // Resposta correta, elimina o Slime.
                    Destroy(gameObject);

                    // Adicione moedas ao jogador após derrotar o Slime.
                    PlayerController player = FindObjectOfType<PlayerController>();
                    if (player != null)
                    {
                        player.AddMoedas(moedasDropped);
                    }
                }
                else
                {
                    // Resposta incorreta, aplica dano ao jogador.
                    PlayerController player = FindObjectOfType<PlayerController>();
                    if (player != null)
                    {
                        player.SetHealth(-damageAmount);
                    }
                }

                // Limpa a resposta do jogador e desativa o diálogo.
                playerResponse = "";
                dialogueText.text = "";
                isDialogActive = false;

                // Desativa a imagem de fundo da barra de diálogo.
                if (dialogueBackgroundImage != null)
                {
                    dialogueBackgroundImage.gameObject.SetActive(false);
                }

                // Reinicia o temporizador da próxima pergunta.
                questionTimer = questionDelay;
            }
            else
            {
                // Captura a entrada do jogador e atualiza a resposta.
                playerResponse += Input.inputString;
                dialogueText.text = $"Qual é a resposta para {num1} + {num2}? ( aperte o botão de attack)\n (Digite a resposta e pressione Enter)\nResposta: {playerResponse}";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    private void AskQuestion()
    {
        isDialogActive = true;

        if (dialogueText != null)
        {
            // Gerar uma pergunta de adição aleatória.
            num1 = Random.Range(1, 21); // Números de 1 a 20.
            num2 = Random.Range(1, 21); // Números de 1 a 20.
            correctAnswer = num1 + num2;

            dialogueText.text = $"Qual é a resposta para {num1} + {num2}? (Digite a resposta e pressione Enter)";

            // Ativa a imagem de fundo da barra de diálogo.
            if (dialogueBackgroundImage != null)
            {
                dialogueBackgroundImage.gameObject.SetActive(true);
            }
        }
    }
}
