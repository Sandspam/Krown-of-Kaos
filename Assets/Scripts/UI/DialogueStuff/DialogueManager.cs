using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public bool dialogueGoing;
    public float timeBetweenDialogue;
    private float dialogueTimer;
    private float dootTimer = 0.1f;
    public Text dialogueText;
    public GameObject instanceGameObject { get; set; }

    private void Start()
    {
        sentences = new Queue<string>();
        dialogueTimer = timeBetweenDialogue;
    }

    public void Update()
    {
        if(dialogueGoing)
        {
            dialogueTimer -= Time.deltaTime;
            if(dialogueTimer <= 0)
            {
                DisplayNextSentence();
                dialogueTimer = timeBetweenDialogue;
            }
        }
    }

    public void StartDialogue (Dialogue dialogue, Text instanceText, GameObject requestedObject)
    {
        //EndDialogue();
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialogueText = instanceText;
        DisplayNextSentence();
        dialogueGoing = true;
        instanceGameObject = requestedObject;
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //How to check that there is no more letters to be typed out. Put the sound effect code in that check
    /*            
     * if(dialogueTimer >= 0.1f)
            {
                dootTimer -= Time.deltaTime;
                if (dootTimer <= 0)
                {
                    instanceAus.Play();
                    instanceAus.pitch = Random.Range(0.9f, 1.1f);
                    dootTimer = 0.1f;
                }
            }
    */

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue ()
    {
        dialogueText.text = "";
        dialogueGoing = false;
        if (instanceGameObject.GetComponent<DialogueTrigger>())
            instanceGameObject.GetComponent<DialogueTrigger>().talking = false;

        if (instanceGameObject.GetComponent<QuestTrigger>())
            instanceGameObject.GetComponent<QuestTrigger>().isTalking = false;
    }
}
