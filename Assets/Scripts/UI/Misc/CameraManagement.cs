using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    public GameObject player;
    public Vector2 velocity;

    public float smoothTimeX;
    public float smoothTimeY;

    private void Start()
    {
        GameObject.Find("Player");
    }

    void Update()
    {
        float posx = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posy = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posx, posy, transform.position.z);
        if (player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
