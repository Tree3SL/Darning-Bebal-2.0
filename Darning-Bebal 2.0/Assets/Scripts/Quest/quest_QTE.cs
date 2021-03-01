using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quest_QTE : MonoBehaviour
{
    Slider ProgressBar;
    public GameObject input_Group;
    public GameObject key_prefab;

    public string[] test_key;
    public Sprite[] key_sprite;
    public int[] keys_per_level;

    public List<string> current_keys;
    public List<GameObject> current_key_objects;
    public int current_index = 0;
    public int current_level = 0;

    public Color success_input;
    public Color fail_input;

    bool isDone = false;

    void Start()
    {
        ProgressBar = GetComponent<Slider>();
        if (ProgressBar != null)
        {
            ProgressBar.value = 0;
        }
        current_keys = new List<string>();

        CreateKeyList();
    }

    void CreateKeyList()
    {
        for (int i = 0; i < keys_per_level[current_level]; i++)
        {
            int index = Random.Range(0, test_key.Length - 1);
            current_keys.Add(test_key[index]);

            GameObject new_key_object = Instantiate(key_prefab);
            new_key_object.transform.SetParent(input_Group.transform);
            new_key_object.GetComponent<Image>().sprite = key_sprite[index];

            current_key_objects.Add(new_key_object);
        }
    }

    void ClearKeyList() 
    {
        for (int i = 0; i < current_key_objects.Count; i++)
        {
            if(current_key_objects[i]!=null)  Destroy(current_key_objects[i]);
        }
        current_key_objects.Clear();
        current_keys.Clear();

    }

    void Update()
    {
        if (!isDone) 
        {
            if (current_index < keys_per_level[current_level])
            {
                if (Input.anyKey)
                {
                    //input correct
                    if (Input.GetKeyDown(current_keys[current_index]))
                    {
                        Input.ResetInputAxes();
                        current_key_objects[current_index].GetComponent<Image>().color = success_input;
                        current_index++;
                        CheckState();
                    }
                    //input fail
                    else
                    {
                        for (int i = 0; i <= current_index; i++)
                        {
                            current_key_objects[i].GetComponent<Image>().color = fail_input;
                        }
                        current_index = 0;
                        Input.ResetInputAxes();
                    }
                }
            }
        }
        
    }

    public void CheckState()
    {
        if (ProgressBar != null)
        {
            if (current_index >= keys_per_level[current_level])
            {
                current_index = 0;
                current_level++;
                BarIncrement();
                if (current_level >= keys_per_level.Length)
                {
                    ClearKeyList();
                    finish();
                    isDone = true;
                }
                else
                {
                    ClearKeyList();
                    CreateKeyList();
                }
            }
        }
    }

    public void BarIncrement() 
    {
        if (ProgressBar != null)
        {
            ProgressBar.value += ProgressBar.maxValue/ keys_per_level.Length;
        }
        
    }
    void finish()
    {
        this.transform.parent.parent.GetComponent<Quest_Canvas>().Chest_Holder.GetComponent<QuestHolder>().finish_quest();
    }

}
