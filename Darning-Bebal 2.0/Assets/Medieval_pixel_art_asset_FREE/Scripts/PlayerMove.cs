using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5.0f;
    public GameObject target;
    public bool isIn = false;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isIn)
        {
            this.transform.localPosition = target.transform.localPosition;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        Vector3 dx = new Vector3(inputX, 0.0f, 0.0f);
        transform.position += dx * speed * Time.deltaTime; 
     

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            switch (collision.GetComponent<Door>().GetDoorEnum())
            {
                case Door.DoorEnum.Left:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                case Door.DoorEnum.Right:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                default:
                    break;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            switch (collision.GetComponent<Door>().GetDoorEnum())
            {
                case Door.DoorEnum.Left:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                case Door.DoorEnum.Right:
                    target = collision.GetComponent<Door>().GetObjEnd();
                    isIn = true;
                    break;
                default:
                    break;
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            target = null;
            isIn = false;
        }
    }
}
