using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool talking;
    private Text dialogueText;
    private UnityAction<GameObject> interactionListener;
    private AudioSource aus;
    private float dootTimer;

    private void Start()
    {
//        interactionListener = new UnityAction<GameObject>(PlayAudio); // Calls the function specified with a GameObject parameter (Function MUST have a GameObject parameter)
        EventManager.StartListening("DialoguePlay", interactionListener); // Initializes the listening behavior with the previously assigned listener (typeof Action)
        dialogueText = transform.parent.GetChild(0).GetChild(0).GetComponent<Text>();
        aus = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Debug.Log(talking);
        if (talking)
        {
            dootTimer -= Time.deltaTime;
            if (dootTimer <= 0)
            {
                aus.Play();
                aus.pitch = Random.Range(0.9f, 1.1f);
                dootTimer = 0.1f;
            }
        }

        if(!talking)
        {
            aus.Stop();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.Space) /*&& !FindObjectOfType<DialogueManager>().dialogueGoing*/)
            {
                Debug.Log("Triggering Dialogue");
                //PlayAudio();
                TriggerDialoge();
            }
        }
    }

    public void TriggerDialoge ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dialogueText, gameObject);
        talking = true;
    }

    private void PlayAudio ()
    {
        aus.Play();
        aus.pitch = Random.Range(0.9f, 1.1f);
    }
}
