using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GiantBoot : MonoBehaviour, ItemInterface
{
    public bool active = false;
    public float stun_time = 0;
    public float throw_force;
    public float throw_torque;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            //stun player
            collision.gameObject.GetComponent<PlayerManager>().DisablePlayer(true);
            //reflection-stop travel
            //finished by rigidbody2d →large damping
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;


            GetComponent<PhotonView>().RPC("pun_hide", RpcTarget.All);
            //delay recover from stun
            StartCoroutine(delay_recover(collision.gameObject));
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background") && active)
        {
            Destroy(this.gameObject);
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
        GameObject new_object = PhotonNetwork.Instantiate("GiantBoot", player.transform.position + new Vector3(1 * player.GetComponent<PlayerManager>().GetDirection(), 0, 0), Quaternion.identity, 0);
        new_object.GetComponent<Rigidbody2D>().AddForce(new Vector2(throw_force * player.GetComponent<PlayerManager>().GetDirection(), 0), ForceMode2D.Impulse);
        new_object.GetComponent<Rigidbody2D>().AddTorque(throw_torque);
        new_object.GetComponent<GiantBoot>().active = true;

        Destroy(this.gameObject);
    }

    public void Disable()
    {
        active = false;
    }


    [PunRPC]
    void pun_hide()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }

    [PunRPC]
    void pun_destory()
    {
        Destroy(this.gameObject);
    }
}
