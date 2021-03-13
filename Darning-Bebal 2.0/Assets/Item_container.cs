using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item_container : MonoBehaviour
{

    public GameObject container_open;

    public float container_timer;

    public GameObject timer;
    public bool ready;
    public bool triggerStay;
    public GameObject colliderObject;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
        timer.SetActive(false);

        triggerStay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.activeSelf)
        {
            if (!timer.GetComponent<timer>().get_is_counting())
            {
                timer.SetActive(false);
                container_open.SetActive(false);
                ready = true;
            }
        }

        if (colliderObject != null && triggerStay)
        {
            if (Input.GetKeyDown(KeyCode.Q) && ready)
            {
                if (TakeItem()) 
                {
                    GetComponent<PhotonView>().RPC("start_countdown", RpcTarget.All);
                }  
            }
        }
    }

    bool TakeItem() 
    {
        GameManager gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        ItemHolder ih = gm.ih;
        if (ih.item_holder != null) return false;
        else 
        {
            int rand = Random.Range(0, gm.item_list.Length);
            ih.item_holder = Instantiate(gm.item_list[rand], ih.transform);
            ih.item_holder.GetComponent<ItemInterface>().Disable();

            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != GameObject.Find("Game Manager").GetComponent<GameManager>().player_holder) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerStay = true;
            colliderObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != GameObject.Find("Game Manager").GetComponent<GameManager>().player_holder) return;
        if (collision.gameObject == colliderObject)
        {
            triggerStay = false;
        }
    }

    [PunRPC]
    public void start_countdown()
    {
        ready = false;
        timer.GetComponent<timer>().set_counttime(container_timer);
        timer.transform.parent.gameObject.SetActive(true);
        timer.SetActive(true);
        timer.GetComponent<timer>().start_countdown();

        container_open.SetActive(true);
    }
}
