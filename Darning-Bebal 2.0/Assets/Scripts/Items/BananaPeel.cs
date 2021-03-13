using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BananaPeel : MonoBehaviour, ItemInterface
{
    public bool active = false;
    public float stun_time = 0;
    public float throw_force;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            //stun player
            collision.gameObject.GetComponent<PlayerManager>().DisablePlayer(true);
            //reflection
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(3 * collision.gameObject.GetComponent<PlayerManager>().GetDirection(), 0), ForceMode2D.Impulse);


            GetComponent<PhotonView>().RPC("pun_hide", RpcTarget.All);
            //delay recover from stun
            StartCoroutine(delay_recover(collision.gameObject));
        }
    }

    IEnumerator delay_recover(GameObject target) 
    {
        yield return new WaitForSeconds(stun_time);
        target.gameObject.GetComponent<PlayerManager>().EnablePlayer();

        GetComponent<PhotonView>().RPC("pun_destory", RpcTarget.All);
        target.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void Use() 
    {
        GameObject player = GameObject.Find("Game Manager").GetComponent<GameManager>().player_holder;
        GameObject new_object = PhotonNetwork.Instantiate("BananaPeel", player.transform.position + new Vector3(1 * player.GetComponent<PlayerManager>().GetDirection(), 0,0), Quaternion.identity, 0);
        new_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(throw_force * player.GetComponent<PlayerManager>().GetDirection(), 0), ForceMode2D.Impulse);
        Destroy(this.gameObject);
    }

    public void Disable()
    {
        active = false;
        
    }


    [PunRPC]
    void pun_hide() 
    {
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }

    [PunRPC]
    void pun_destory() 
    {
        Destroy(this.gameObject);
    }
}
