using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quest_fill_water : MonoBehaviour
{
    Slider ProgressBar;
    public float Fill_Speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        ProgressBar = GetComponent<Slider>();
        if (ProgressBar != null) 
        {
            ProgressBar.value = 0;
        }
    }
    void Update()
    {
        if (ProgressBar == null) return;

        if (Input.GetKey(KeyCode.W))
        {
            ProgressBar.value += Fill_Speed * Time.deltaTime;
        }
        else 
        {
            if (ProgressBar.value >= ProgressBar.maxValue)
            {
                Debug.Log("Water Fill Condition Met");
            }
        }
    }
}
