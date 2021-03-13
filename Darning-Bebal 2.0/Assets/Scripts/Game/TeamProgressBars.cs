using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TeamProgressBars : MonoBehaviour
{
    public float max;

    private Slider slider;

    private float targetProgress = 0;

    public float FillSpeed = 10f;

    private ParticleSystem particleSys;

    public void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        //particleSys = GameObject.Find("Progress Bar Particles").GetComponent<ParticleSystem>();
        slider.maxValue = max;
    }

    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
            /*if (particleSys != null) 
            {
                if (!particleSys.isPlaying)
                    particleSys.Play();
            } */
        }
        else
        {
            /*if (particleSys != null)
            {
                particleSys.Stop();
            }*/
        }

        //check if game end
        if (slider.value >= slider.maxValue)
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().GameOver();
        }
          
    }

    [PunRPC]
    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

}
