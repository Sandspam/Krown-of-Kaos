using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public bool cutsceneGoing;
    public GameObject audioSource;

    // Start is called before the first frame update
    void Start()
    {
        /*cutsceneGoing = true;
        audioSource.GetComponent<AudioSource>().Stop();
        Time.timeScale = 0f;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(cutsceneGoing)
        {
            /*
            Time.timeScale = 0f;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(1).transform.GetChild(2).GetComponent<AudioSource>().Stop();
            */
        }
    }

    public void StopCutscene()
    {
        SceneManager.LoadScene("SampleScene");
        /*Time.timeScale = 1f;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        audioSource.GetComponent<AudioSource>().Play();
        cutsceneGoing = false;*/
    }
}
