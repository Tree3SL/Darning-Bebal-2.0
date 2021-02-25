using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{

    public float total_time;
    [SerializeField] private float current_time = 0.0f;
    [SerializeField] bool is_counting = false;
    public TextMeshProUGUI timer_text;
    public Image fill;

    public Color start_color;
    public Color end_color;
    [SerializeField] bool is_coloring = false;

    // Start is called before the first frame update
    void Start()
    {
        current_time = total_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_counting) 
        {
            current_time -= 1 * Time.deltaTime;
            timer_text.text = current_time.ToString("0.0");
            if (current_time <= 0) 
            {
                current_time = 0;
                is_counting = false;
            }
            fill.GetComponent<Image>().fillAmount = 1 - current_time/total_time;

            if (is_coloring) 
            {
                float current_portion = current_time / total_time;

                Color new_color = (start_color - end_color) * current_portion + end_color;
                timer_text.color = new_color;
                fill.color = new_color;

                if (current_time <= 0)
                {
                    timer_text.color = Color.white;
                    fill.color = Color.white;
                }
            }
        }

    }

    public void start_countdown() 
    {
        current_time = total_time;
        is_counting = true;
    }

    public void set_counttime(float new_time) 
    {
        total_time = new_time;
        current_time = total_time;
    }

    public bool get_is_counting() 
    {
        return is_counting;
    }
}
