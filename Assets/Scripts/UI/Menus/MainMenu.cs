using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource aus;
    //private float audioLength;
    private bool playButton;
    private GameObject cutscene;

    private void Start()
    {
        if (GameObject.Find("Main Menu Song") != null)
            aus = GameObject.Find("Main Menu Song").GetComponent<AudioSource>();
        //audioLength = aus.clip.length;
        cutscene = GameObject.Find("CutsceneManager");

    }

    private void Update()
    {
        if (playButton)
        {
            /*audioLength -= Time.deltaTime;
            if (audioLength <= 0)
            {
                SceneManager.LoadScene("SampleScene");
            }*/
        }
    }

    public void PlayButton()
	{
        //aus.Play();
        aus.Stop();
        playButton = true;
        cutscene.GetComponent<Animator>().enabled = true;
        cutscene.transform.GetChild(0).gameObject.SetActive(true);
	}

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
}
