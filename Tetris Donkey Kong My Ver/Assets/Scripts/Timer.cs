using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float time;
    public static float startTime = 5;
    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        time = startTime;
        StartCoroutine(LoseTime());
        Time.timeScale = 1;
        countdownText.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = time.ToString();
        //print(time);

        if (time <= 0) {
            time = startTime;
        }
    }

    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }
}
