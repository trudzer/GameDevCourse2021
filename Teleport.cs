using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("teleporter1"))
        {
            other.gameObject.transform.position = new Vector3(577.71f, 18.867f, 266.50f);
        }

        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("teleporter2"))
        {
            other.gameObject.transform.position = new Vector3(572.8f, 17.38f, 556.5f);
        }
    }
}
