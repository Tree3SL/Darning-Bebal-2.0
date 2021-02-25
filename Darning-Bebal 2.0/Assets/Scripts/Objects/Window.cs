using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject spawn_point;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger used.");
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerMovement>().stun) 
            {
                collision.gameObject.transform.position = spawn_point.transform.position;
                Debug.Log("Window used.");
            }   
        }
    }
}
