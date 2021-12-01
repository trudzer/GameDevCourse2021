using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BigDoorInfront : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public GameObject uiObject;
    public TextMeshProUGUI uiText;
    [SerializeField] private Animator door = null;
    private bool open = false;
    private bool close = true;
    public Transform player;
    private int counter = 0;
    public int maxDistance = 2;
    public bool locked = true;

    void Start()
    {
        uiObject.SetActive(false);
    }

    void Update()
    {
        GameObject theKey = GameObject.FindWithTag("big_door");
        BigKeyScript theKeyScript = theKey.GetComponent<BigKeyScript>();
        locked = theKeyScript.locked;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (locked)
            {
                if (locked && hit.collider.gameObject.layer == LayerMask.NameToLayer("big_door") && hit.distance < maxDistance)
                {
                    uiObject.SetActive(true);
                    uiText.text = "Door is locked";
                }
            }

            else
            {
                uiObject.SetActive(false);
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("big_door") && hit.distance < maxDistance)
            {
                if (!locked)
                {
                    uiObject.SetActive(true);
                    if (close && counter < 1)
                    {
                        uiText.text = "Press F to open";
                        if (Input.GetKeyDown("f") && counter < 1)
                        {
                            door.Play("open_door", 0, 0.0f);
                            hit.collider.GetComponent<BoxCollider>().size = new Vector3(4.594944f, 3.538213f, 0.5f);
                            open = true;
                            close = false;
                            hit.collider.gameObject.layer = 0;
                            counter++;
                        }
                    }
                    counter = 0;
                }
            }
            else
            {
                uiObject.SetActive(false);
            }
        }
    }
}
