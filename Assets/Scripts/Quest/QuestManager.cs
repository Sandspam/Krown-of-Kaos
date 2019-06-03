using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public QuestObject[] quests;
    public bool[] questCompleted;

    public DialogueManager dMan;

    private void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
        questCompleted = new bool[quests.Length];
    }

    public void ShowQuestText(Dialogue dialogue, Text instanceText, GameObject requestedObject)
    {
        dMan.StartDialogue(dialogue, instanceText, requestedObject);
        /*for (int i = 0; i < questText.Length; i++)
        {
            //Adds the quest text string array to the sentences queue
            dMan.sentences.Enqueue(questText[i]);
            dMan.StartDialogue(
        }*/
    }
}
