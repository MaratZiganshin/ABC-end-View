using UnityEngine;
using UnityEngine.UI;

//вывод времени, затраченного на решение и лучшего времени
public class ShowTime : MonoBehaviour {

	// Use this for initialization
	void Start () {       
        float time = PlayerPrefs.GetFloat("Time");
        string seconds;
        string minutes;
        if (gameObject.name == "Best")
        {
            int best = (int)PlayerPrefs.GetFloat("Best" + PlayerPrefs.GetInt("Size"));
            if (best == 0||best>(int)time) PlayerPrefs.SetFloat("Best" + PlayerPrefs.GetInt("Size"), time);
            PlayerPrefs.Save();
            best = (int)PlayerPrefs.GetFloat("Best" + PlayerPrefs.GetInt("Size"));
            seconds = (best % 60).ToString();
            minutes = (best / 60).ToString();
        }
        else
        {
            int temp = (int)time;
            seconds = (temp % 60).ToString();
            minutes = (temp / 60).ToString();
        }
        string en="";
        string ru="";
        switch (gameObject.name)
        {
            case "Time":
                en = "Time";
                ru = "Время";
                break;
            case "Best":
                en = "Best";
                ru = "Рекорд";
                break;
        }
        if (seconds.Length == 1) seconds = "0" + seconds;
        if (minutes.Length == 1) minutes = "0" + minutes;
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            gameObject.GetComponent<Text>().text = en + ": " + minutes + ":" + seconds;
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            gameObject.GetComponent<Text>().text = ru + ": " + minutes + ":" + seconds;
        }
        
	}

}
