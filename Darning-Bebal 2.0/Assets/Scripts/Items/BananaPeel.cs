using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPeel : MonoBehaviour
{
    public bool active;
    public float stun_time = 0;
    public float fall_force = 1;
    // Start is called before the first frame update
    void Start()
    {
        //active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TO DO: Check if isMine
        if (collision.gameObject.CompareTag("Player") && active)
        {
            //stun player
            collision.gameObject.GetComponent<PlayerManager>().DisablePlayer(true);
            //animate falling
            //float facing = collision.gameObject.GetComponent<PlayerManager>().GetDirection();
            //collision.attachedRigidbody.AddForce(new Vector2(fall_force*facing, 0));
            //delay recover from stun
            StartCoroutine(delay_recover(collision.gameObject));
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator delay_recover(GameObject target) 
    {
        yield return new WaitForSeconds(stun_time);
        target.gameObject.GetComponent<PlayerManager>().EnablePlayer();
        Destroy(this.gameObject);
    }
}
