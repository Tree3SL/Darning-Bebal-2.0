using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ItemHolder : MonoBehaviour
{
    public GameObject player;
    public GameObject item_holder;
    public GameObject sprite_holder;
    private Sprite default_sprite;
    // Start is called before the first frame update
    void Start()
    {
        default_sprite = sprite_holder.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) 
        {
            //recheck player
            if (GameObject.Find("Game Manager").GetComponent<GameManager>().player_holder != null)
            {
                player = GameObject.Find("Game Manager").GetComponent<GameManager>().player_holder;
            }
            else
            {
                return;
            }
        }

        if (item_holder != null)
        {
            sprite_holder.GetComponent<Image>().sprite = item_holder.GetComponent<SpriteRenderer>().sprite;
        }
        else 
        {
            sprite_holder.GetComponent<Image>().sprite = default_sprite;
        }

        //check item usage
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (item_holder != null)
            {
                    item_holder.GetComponent<ItemInterface>().Use();
            }
            else 
            {
                //report invalid
            }
        }

    }
}

public interface ItemInterface
{
    void Use();
    void Disable();
}
