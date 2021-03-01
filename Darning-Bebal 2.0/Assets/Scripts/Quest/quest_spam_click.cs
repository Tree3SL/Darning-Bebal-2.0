using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quest_spam_click : MonoBehaviour
{
    Slider ProgressBar;
    void Start()
    {
        ProgressBar = GetComponent<Slider>();
        if (ProgressBar != null)
        {
            ProgressBar.value = 0;
        }
        
    }
    public void OnClick() 
    {
        if (ProgressBar != null)
        {
            ProgressBar.value += Random.Range(1f, 20f);
        }
        
    }

    public void CheckState() 
    {
        if (ProgressBar != null) 
        {
            if (ProgressBar.value >= 100) 
            {
                finish();
            }
        }
    }

    void finish()
    {
        this.transform.parent.parent.GetComponent<Quest_Canvas>().Chest_Holder.GetComponent<QuestHolder>().finish_quest();
    }
}
