using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    int static_Time = 100;
    int time_int;
    
    // Start is called before the first frame update
    void Start()
    {
        time_int = static_Time;
        InvokeRepeating("PlayTimer", 0, 1);
    }
    
    void PlayTimer()
    {
        if(time_int > 10)
        {
            timerText.color = Color.green;
        }
        else
        {
            timerText.color = Color.red;
            if (time_int <= 0)
            {
                GameManager.Instance.DeletePlayer();
                time_int = static_Time;
            }
        }

        timerText.text = time_int.ToString();
        time_int--;
    }

}
