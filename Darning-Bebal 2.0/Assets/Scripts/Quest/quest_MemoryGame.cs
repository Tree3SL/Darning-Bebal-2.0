using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quest_MemoryGame : MonoBehaviour
{
    public Sprite[] key_list;
    public Sprite target_key;
    public GameObject key_prefab;
    public bool gameStart = false;

    public List<GameObject> key_objects;
    // Start is called before the first frame update
    void Start()
    {
        //assign key
        if (key_list.Length != 0) {
            int index = Random.Range(0, key_list.Length-1);
            target_key = key_list[index];
        }
        //create list
        key_objects = new List<GameObject>();
        for (int i = 0; i < key_list.Length - 1; i++) {
            GameObject new_key = Instantiate(key_prefab, this.transform.Find("Grid"));
            new_key.GetComponent<quest_MemoryGameKey>().show_sprite = key_list[i];
            if (key_list[i] == target_key) {
                new_key.GetComponent<quest_MemoryGameKey>().isRight = true;
            }
            key_objects.Add(new_key);
        }
        GameObject key = Instantiate(key_prefab, this.transform.Find("Grid"));
        key.GetComponent<quest_MemoryGameKey>().show_sprite = target_key;
        key.GetComponent<quest_MemoryGameKey>().isRight = true;
        key_objects.Add(key);

        //shuffle
        for (int i = 0; i < key_objects.Count; i++) {
            int newIndex = Random.Range(i, key_objects.Count - 1);
            if (i != newIndex) {
                GameObject temp = key_objects[i];
                key_objects[i] = key_objects[newIndex];
                key_objects[newIndex] = temp;
            }
            
        }

        StartCoroutine(waitAndStart(3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart) return;
        int right_num = 0;
        for (int i = 0; i < this.transform.Find("Grid").childCount; i++) {
            if (this.transform.Find("Grid").GetChild(i).GetComponent<quest_MemoryGameKey>().isRight) {
                if (!this.transform.Find("Grid").GetChild(i).GetComponent<quest_MemoryGameKey>().isHide)
                    right_num++;
            }
        }
        if (right_num >= 2) {
            Done();
        }
    }

    IEnumerator waitAndStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < key_objects.Count; i++) {
            key_objects[i].GetComponent<quest_MemoryGameKey>().Hide();
        }
        gameStart = true;
    }

    public void Done() {
        gameStart = false;
        finish();
    }

    void finish() 
    {
        this.transform.parent.GetComponent<Quest_Canvas>().Chest_Holder.GetComponent<QuestHolder>().finish_quest();
    }

}
