using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeratorCommands : MonoBehaviour
{
    public GameObject swampTeleport;
    public GameObject beachTeleport;
    public GameObject bossTeleport;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject.Find("CutsceneManager").GetComponent<Cutscene>().StopCutscene();
        }

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            player.transform.position = swampTeleport.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            player.transform.position = beachTeleport.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            player.transform.position = bossTeleport.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.GetComponent<PlayerHealthManager>().RestoreHealth(5);
        }
    }
}
