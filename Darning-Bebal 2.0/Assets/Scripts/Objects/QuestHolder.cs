using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestHolder : MonoBehaviour
{
    public int quest_index;
    public GameObject quest_prefab;
    public float quest_timer;

    public GameObject timer;
    public bool ready;
    public bool active; //active status for quest object
    public GameObject quest_holder;

    public Transform quest_canvas;
    public bool triggerStay;
    public GameObject colliderObject;

    public GameObject ProgressBar;


    // Start is called before the first frame update
    void Start()
    {
        ready = true;
        timer.SetActive(false);

        active = false;
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
                ready = true;
            }
        }

        if (colliderObject != null && triggerStay) 
        {
            if (Input.GetKeyDown(KeyCode.W) && !active)
            {
                open_quest(colliderObject);
            }
            if (active && colliderObject.GetComponent<PlayerMovement>().stun)
            {
                close_quest();
            }
            
        }
    }


    public void start_countdown() 
    {
        ready = false;
        timer.GetComponent<timer>().set_counttime(quest_timer);
        timer.transform.parent.gameObject.SetActive(true);
        timer.SetActive(true);
        timer.GetComponent<timer>().start_countdown();
    }

    public void set_quest(int index,GameObject quest, float cooldown) 
    {
        quest_index = index;
        quest_prefab = quest;
        quest_timer = cooldown;
        timer.GetComponent<timer>().set_counttime(cooldown);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TO DO: Check if isMine
        if (collision.gameObject.CompareTag("Player")) 
        {
            triggerStay = true;
            colliderObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == colliderObject)
        {
            triggerStay = false;
        }
    }

    public void open_quest(GameObject player)
    {
        //skip open if the timer is in cooldown
        if (!ready) return;
        active = true;
        //activate canvas
        quest_canvas = player.transform.Find("Canvas");
        quest_canvas.gameObject.SetActive(true);
        //create quest
        quest_holder = Instantiate<GameObject>(quest_prefab, quest_canvas);
        quest_canvas.GetComponent<Quest_Canvas>().Chest_Holder = this.gameObject;
        //disable player
        player.GetComponent<PlayerManager>().DisablePlayer(false);
    }

    //call when quest is close without finishing
    public void close_quest() 
    {
        active = false;
        //reactivate player
        quest_canvas.transform.parent.gameObject.GetComponent<PlayerManager>().EnablePlayer();
        //close canvas
        quest_canvas.gameObject.SetActive(false);
        //delete quest
        Destroy(quest_holder);
    }

    //call when quest is close after finish
    public void finish_quest()
    {
        //TO DO : check isMine
        if (ProgressBar != null) 
        {
            ProgressBar.GetComponent<Slider>().value += quest_timer;
        }
        start_countdown();
        close_quest();
    }
}
