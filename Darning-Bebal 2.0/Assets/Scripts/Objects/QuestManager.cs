using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject[] quest_list;
    public GameObject[] chest_list;
    // Start is called before the first frame update
    void Start()
    {
        if (quest_list.Length <= 0) 
        {
            Debug.Log("ERROR! No Quest in List");
        }
        if (chest_list.Length <= 0)
        {
            Debug.Log("ERROR! No Chest in List");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Assign_All_Chest() 
    {
        for (int i = 0; i < chest_list.Length; i++) 
        {
            int random_index = Random.Range(0, quest_list.Length);
            chest_list[i].GetComponent<QuestHolder>().set_quest(random_index, quest_list[random_index], 10);
        }
    }

    public void Syn_Chest() 
    {
        //update chest based on server
    }
}
