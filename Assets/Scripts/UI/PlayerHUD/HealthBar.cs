using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool isPlayer;
    public bool isEnemy;

    private Transform bar;
    private Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer)
        {
            bar = transform.Find("HealthBar").transform.Find("Bar");
            hpText = transform.Find("HPText").GetComponent<Text>();
        }

        if(isEnemy)
        {
            hpText = transform.Find("HPText").GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (isPlayer)
        {
            if (GameObject.Find("Player") != null)
            {
                bar.localScale = new Vector3(GameObject.Find("Player").GetComponent<PlayerHealthManager>().currentHealth / GameObject.Find("Player").GetComponent<PlayerHealthManager>().maxHealth, 1f);
                hpText.text = GameObject.Find("Player").GetComponent<PlayerHealthManager>().currentHealth + "/" + GameObject.Find("Player").GetComponent<PlayerHealthManager>().maxHealth;
            }
        }

        if (isEnemy)
        {
            hpText.text = gameObject.transform.parent.parent.GetComponent<EnemyHealthManager>().currentHealth + "/" + gameObject.transform.parent.parent.GetComponent<EnemyHealthManager>().maxHealth;
        }
    }
}
