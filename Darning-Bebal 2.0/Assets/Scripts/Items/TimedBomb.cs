using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TimedBomb : MonoBehaviour, ItemInterface
{
    public bool is_active;
    public Sprite bomb_sprtie;
    public GameObject timer_object;

    public float timeCount;
    public float explosionRadius;
    public float explosionForce;
    public LayerMask playerMask;
    public float stun_time;

    public GameObject recover_target;

    public bool is_exploded = false;
    public int remian_recover = -1;


    // Start is called before the first frame update
    void Start()
    {
        //is_active = false;
        //timer_object = this.transform.Find("Canvas").Find("Timer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_active) 
        {
            if (!timer_object.GetComponent<timer>().get_is_counting()) 
            {
                Debug.Log("Exploded");
                explode_detect();
                is_active = false;
            }
        }
        if (is_exploded && remian_recover == 0) 
        {
            Destroy(this.gameObject);
        }
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TO DO: Check if isMine
        if (collision.gameObject.CompareTag("Player") && !is_active)
        {
            //activate bomb
            set_active(true);
        }
    }*/

    [PunRPC]
    public void set_active(bool state) 
    {
        if (state)
        {
            //turn on bomb
            is_active = true;

            //enable visualization
            timer_object.SetActive(true);
            GetComponent<Renderer>().enabled = true;

            //set timer
            timer_object.GetComponent<timer>().set_counttime(timeCount);
            timer_object.GetComponent<timer>().start_countdown();


        }
        else 
        {
            is_active = false;
            //disable time bomb when added to inventory
        }
    }

    public void explode_detect() 
    {
        remian_recover = 0;
           Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody2D targetRigidbody = colliders[i].GetComponent<Rigidbody2D>();
            if (!targetRigidbody)
                continue;
            //apply force
            targetRigidbody.AddForce(explosionForce * (targetRigidbody.position - new Vector2(transform.position.x, transform.position.y)), ForceMode2D.Impulse);
            //disable control
            PlayerManager targetPlayer = colliders[i].GetComponent<PlayerManager>();
            if (!targetPlayer)
                continue;
            //stun all collided player object
            targetPlayer.DisablePlayer(true);
            Debug.Log("Stun");
            //delay recover
            StartCoroutine(delay_recover(targetPlayer.gameObject));
            remian_recover++;
        }
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<PhotonView>().RPC("pun_hide", RpcTarget.All);
        is_exploded = true;
    }

    IEnumerator delay_recover(GameObject target)
    {
        yield return new WaitForSeconds(stun_time);
        target.gameObject.GetComponent<PlayerManager>().EnablePlayer();
        remian_recover--;
    }

    [PunRPC]
    void pun_hide()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    [PunRPC]
    void pun_destory()
    {
        Destroy(this.gameObject);
    }

    public void Use()
    {
        GameObject player = GameObject.Find("Game Manager").GetComponent<GameManager>().player_holder;
        GameObject new_object = PhotonNetwork.Instantiate("TimedBomb", player.transform.position + new Vector3(1 * player.GetComponent<PlayerManager>().GetDirection(), 0, 0), Quaternion.identity, 0);
        new_object.GetComponent<PhotonView>().RPC("set_active", RpcTarget.All, true);
        Destroy(this.gameObject);
    }

    public void Disable()
    {
        is_active = false;
    }
}
