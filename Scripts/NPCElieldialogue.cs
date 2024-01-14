using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCElieldialogue : MonoBehaviour
{
    public string npcName;
    public string[] dialogueNpc;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;

    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;

    public bool readyToSpeak;
    public bool startDialogue;

    private bool choiceMode = false; // Indica se o diálogo está em modo de escolha.

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && readyToSpeak)
        {
            if (!startDialogue)
            {
                FindObjectOfType<PlayerController>()._playerSpeed = 0f;
                StartDialogue();
            }
            else
            {
                if (choiceMode)
                {
                    HandleChoice();
                }
                else if (dialogueText.text == dialogueNpc[dialogueIndex])
                {
                    NextDialogue();
                }
            }
        }
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueNpc.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;

            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.RestoreOriginalSpeeds();

            FindObjectOfType<PlayerController>()._playerSpeed = 3f;
        }
    }

    void StartDialogue()
    {
        nameNpc.text = npcName; 
        imageNpc.sprite = spriteNpc;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.StoreOriginalSpeeds();
        playerController._playerRunSpeed = 0f;
        StartCoroutine(ShowDialogue());
    }

    void StartChoiceDialogue()
    {
        dialogueText.text = dialogueText.text.Replace("[Choice]", "\n[1] Entender\n[2] Não Entender");
        choiceMode = true;
    }

    void HandleChoice()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogueText.text = dialogueText.text.Replace("[1] Entender\n[2] Não Entender", "Entendi!");
            choiceMode = false;
            NextDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogueText.text = dialogueText.text.Replace("[1] Entender\n[2] Não Entender", "Não entendi. Explique novamente.");
            choiceMode = false;
            NextDialogue();
        }
    }

    IEnumerator ShowDialogue()
    {
        dialogueText.text = "";
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        if (dialogueText.text.Contains("[Choice]"))
        {
            StartChoiceDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = false;
        }
    }
}
