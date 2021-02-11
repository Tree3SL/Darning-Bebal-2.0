using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quest_MemoryGameKey : MonoBehaviour
{
    public bool isRight = false;
    public bool isHide = false;
    public Sprite hidden_sprite;
    public Sprite show_sprite;


    public bool gameStart = false;

    private IEnumerator coroutine;

    void Start()
    {
        this.GetComponent<Image>().sprite = show_sprite;
    }


    public IEnumerator WaitAndHide(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Hide();
    }

    public void Reveal() {
        isHide = false;
        this.GetComponent<Image>().sprite = show_sprite;
        if (!isRight) {
            coroutine = WaitAndHide(0.5f);
            StartCoroutine(coroutine);
        }
    }

    public void Hide() {
        isHide = true;
        this.GetComponent<Image>().sprite = hidden_sprite;
    }

}
