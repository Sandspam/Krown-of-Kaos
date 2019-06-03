using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    //General Variables
    private QuestManager QM;
    public int questID;
    public bool startQuest;
    public bool endQuest;
    public string questType;
    private Sprite questMarker;
    public Sprite newQuest;
    public Sprite activeQuest;
    public Sprite completedQuest;
    public bool isNPC;
    public bool isTalking;
    private AudioSource aus;
    private float dootTimer = 0.1f;

    //Fetch Quest Variables
    public int fetchQuestID;
    private HotbarManager hbM;

    private void Start()
    {
        aus = gameObject.transform.GetComponent<AudioSource>();
        if(isNPC)
            questMarker = gameObject.transform.parent.GetChild(2).GetComponent<Sprite>();
        hbM = FindObjectOfType<HotbarManager>();
        QM = FindObjectOfType<QuestManager>();
    }

    private void Update()
    {
        if(isTalking)
        {
            dootTimer -= Time.deltaTime;
            if (dootTimer <= 0)
            {
                aus.Play();
                aus.pitch = Random.Range(0.9f, 1.1f);
                dootTimer = 0.1f;
            }
        }
        if(isTalking == false)
        {
            aus.Stop();
        }

        if(!QM.questCompleted[questID])
        {
            if(startQuest && !QM.quests[questID].gameObject.activeSelf && isNPC)
            {
                questMarker = newQuest;
            }

            if(startQuest && QM.quests[questID].gameObject.activeSelf && isNPC)
            {
                questMarker = activeQuest;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //This is what happens if it's a location quest
            if (questType == "Location")
            {
                if (!QM.questCompleted[questID])
                {
                    if (Input.GetKeyDown(KeyCode.Space) && startQuest && !QM.quests[questID].gameObject.activeSelf)
                    {
                        QM.quests[questID].gameObject.SetActive(true);
                        QM.quests[questID].StartQuest(gameObject);
                    }

                    if (Input.GetKeyDown(KeyCode.Space) && endQuest && QM.quests[questID].gameObject.activeSelf)
                    {
                        QM.quests[questID].EndQuest(gameObject);
                    }
                }
            }

            //This is what happens if it's a fetch quest
            if (questType == "Fetch")
            {
                if (!QM.questCompleted[questID])
                {
                    if (Input.GetKeyDown(KeyCode.Space) && startQuest && !QM.quests[questID].gameObject.activeSelf)
                    {
                        isTalking = true;
                        QM.quests[questID].gameObject.SetActive(true);
                        QM.quests[questID].StartQuest(gameObject);
                    }

                    if (Input.GetKeyDown(KeyCode.Space) && endQuest && QM.quests[questID].gameObject.activeSelf)
                    {
                        for (int i = 0; i < hbM.hotbars.Length; i++)
                        {
                            if (hbM.hotbars[i].GetComponent<Hotbar>().itemID == fetchQuestID)
                            {
                                if (!QM.questCompleted[questID])
                                {
                                    hbM.RemoveItem(fetchQuestID);
                                    QM.quests[questID].EndQuest(gameObject);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(questType == "Fetch")
            {
                for (int i = 0; i < hbM.hotbars.Length; i++)
                {
                    if (hbM.hotbars[i].GetComponent<Hotbar>().itemID == fetchQuestID)
                    {
                        if (!QM.questCompleted[questID])
                        {
                            if (endQuest && QM.quests[questID].gameObject.activeSelf)
                            {
                                hbM.RemoveItem(fetchQuestID);
                                QM.quests[questID].EndQuest();
                            }
                        }
                    }
                }
            }
        }
    }
}*/
