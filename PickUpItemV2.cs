using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpItemV2 : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public GameObject uiObject;
    public TextMeshProUGUI uiText;
    public GameObject textScreen;
    public Transform itemContainer;
    public GameObject cameraObj;
    public GameObject player;
    private GameObject item;
    bool equipped = false;
    public int maxDistance = 4;
    bool slotEmpty = true;
    private int counter = 0;

    void Start()
    {
        item = this.gameObject;
        Physics.IgnoreLayerCollision(3, 8);
        equipped = false;
        slotEmpty = true;
        counter = 0;
        player.GetComponent<BoxCollider>().size = new Vector3(1f, 2.06f, 1f);
        player.GetComponent<BoxCollider>().center = new Vector3(0f, 1.28f, 0f);
        textScreen.SetActive(false);
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float mx = Input.GetAxis("Mouse X");

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("items") && hit.distance < maxDistance && hit.collider.gameObject == item)
            {
                if (!equipped && slotEmpty && counter < 1)
                {
                    uiObject.SetActive(true);
                    uiText.text = "Press F to pickup";
                    if (Input.GetKeyDown("f") && counter < 1)
                    {
                        if (item.gameObject.CompareTag("key") || item.gameObject.CompareTag("bigKey"))
                        {
                            item.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                        }
                        if (item.gameObject.CompareTag("paper"))
                        {
                            Time.timeScale = 0;
                            textScreen.SetActive(true);
                        }
                        player.GetComponent<BoxCollider>().size = new Vector3(1f, 2.06f, 2f);
                        player.GetComponent<BoxCollider>().center = new Vector3(0f, 1.28f, 0.5f);
                        slotEmpty = false;
                        uiObject.SetActive(false);
                        gameObject.GetComponent<Rigidbody>().useGravity = false;
                        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        equipped = true;
                        this.gameObject.layer = 8;
                        this.transform.localPosition = new Vector3(0, 0, 0);
                        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        this.transform.position = itemContainer.position;
                        this.transform.parent = cameraObj.transform;
                        this.transform.parent = player.transform;
                        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        counter++;
                    }
                }
            }
        }

        if (equipped && !slotEmpty && counter < 1)
        {
            if (Input.GetKeyDown("f") && counter < 1)
            {
                if (item.gameObject.CompareTag("key") || item.gameObject.CompareTag("bigKey"))
                {
                    item.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                if (item.gameObject.CompareTag("paper"))
                {
                    Time.timeScale = 1;
                    textScreen.SetActive(false);
                }
                slotEmpty = true;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                equipped = false;
                this.gameObject.layer = 7;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                this.transform.SetParent(null);
                player.GetComponent<BoxCollider>().size = new Vector3(1f, 2.06f, 1f);
                player.GetComponent<BoxCollider>().center = new Vector3(0f, 1.28f, 0f);
                counter++;
            }
        }

        if (equipped && !slotEmpty && counter < 1)
        {
            if (Input.GetButtonDown("Fire1") && counter < 1)
            {
                if (item.gameObject.CompareTag("key"))
                {
                    item.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                if (item.gameObject.CompareTag("paper"))
                {
                    Time.timeScale = 1;
                    textScreen.SetActive(false);
                }
                Vector3 dir = ray.GetPoint(1) - ray.GetPoint(0);
                slotEmpty = true;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                equipped = false;
                this.gameObject.layer = 7;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                this.gameObject.transform.position = ray.GetPoint(2);
                this.gameObject.transform.rotation = Quaternion.LookRotation(dir);
                this.transform.SetParent(null);
                player.GetComponent<BoxCollider>().size = new Vector3(1f, 2.06f, 1f);
                player.GetComponent<BoxCollider>().center = new Vector3(0f, 1.28f, 0f);
                this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.transform.forward * 10;
                counter++;
            }
        }
        counter = 0;
    }
}
