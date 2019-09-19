using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public ability enumValue;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 1, 0));
        GetComponentInChildren<TextMesh>().transform.LookAt(Camera.main.transform);
        GetComponentInChildren<TextMesh>().transform.Rotate( new Vector3(0,180, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            player.GetComponent<Player>().changeAbilityScript(enumValue);
            Destroy(this.gameObject);
        }
    }
}
