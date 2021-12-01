using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private int doorCounter = 0;
    public bool locked = true;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key") && doorCounter == 0)
        {
            locked = false;
            Destroy(other.gameObject);
            doorCounter++;
            player.GetComponent<BoxCollider>().size = new Vector3(1f, 2.06f, 1f);
            player.GetComponent<BoxCollider>().center = new Vector3(0f, 1.28f, 0f);
        }
    }
}
