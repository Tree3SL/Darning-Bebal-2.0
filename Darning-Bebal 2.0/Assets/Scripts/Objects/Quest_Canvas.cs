using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Canvas : MonoBehaviour
{

    public GameObject Chest_Holder;

    public void Destory_Quest() 
    {
        if (Chest_Holder != null) 
        {
            Chest_Holder.GetComponent<QuestHolder>().close_quest();
        }
    }

}
