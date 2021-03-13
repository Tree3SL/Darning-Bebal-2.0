using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowCandy : MonoBehaviour, ItemInterface
{
    public float speed_amount;
    public float duration;
    public bool is_used = false;

    public Color in_use_color;


/*    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //increase speed
            activate(collision.gameObject);

            //delay recover
            StartCoroutine(delay_recover(collision.gameObject));
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }*/

    void activate(GameObject target) 
    {
        target.GetComponent<PlayerManager>().BoostSpeed(speed_amount);
        is_used = true;
    }

    IEnumerator delay_recover(GameObject target)
    {
        yield return new WaitForSeconds(duration);
        target.gameObject.GetComponent<PlayerManager>().ResetSpeed();
        Destroy(this.gameObject);
    }


    public void Use()
    {
        if (is_used) return;

        GameObject player = GameObject.Find("Game Manager").GetComponent<GameManager>().player_holder;

        activate(player);

        //delay recover
        StartCoroutine(delay_recover(player));
        this.gameObject.GetComponent<SpriteRenderer>().color = in_use_color;

    }

    public void Disable()
    {
    }
}
