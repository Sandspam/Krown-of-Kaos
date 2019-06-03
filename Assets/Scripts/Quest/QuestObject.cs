using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{
    //Make sure to deactivate the game object on launch

    public int questID;

    public Dialogue startText;
    public Dialogue endText;
    public Text startDialogueText;
    public Text endDialogueText;
    public GameObject rewardNPC;

    public QuestManager QM;

    public GameObject itemObject;
    public bool givesReward;
    public int rewardItemID;

    private void Start()
    {
        //dialogueText = transform.parent.GetChild(0).GetComponentInChildren<Text>();
        QM = FindObjectOfType<QuestManager>();
    }

    public void StartQuest(GameObject requestedObject)
    {
        QM.ShowQuestText(startText, startDialogueText, requestedObject);
    }

    public void EndQuest(GameObject requestedObject)
    {
        QM.questCompleted[questID] = true;
        QM.ShowQuestText(endText, endDialogueText, requestedObject);
        if(givesReward)
        {
            GameObject questReward = Instantiate(itemObject, new Vector3(rewardNPC.transform.position.x + 1, rewardNPC.transform.position.y), Quaternion.identity);
            questReward.GetComponent<PickUp>().itemID = rewardItemID;
        }
        gameObject.SetActive(false);
    }
}
