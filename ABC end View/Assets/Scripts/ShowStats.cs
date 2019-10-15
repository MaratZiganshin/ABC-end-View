using UnityEngine;
using UnityEngine.UI;

//вывод статистики
public class ShowStats : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int size = int.Parse(gameObject.name.Substring(0,1));
        GameObject best= gameObject.transform.FindChild("Best").gameObject;
        string seconds, minutes;
        int temp;
        if (!PlayerPrefs.HasKey("Best" + size)) { seconds = "--"; minutes = "--"; }
        else {
            temp = (int)PlayerPrefs.GetFloat("Best" + size);
            seconds = (temp % 60).ToString();
            minutes = (temp / 60).ToString();
            if (seconds.Length == 1) seconds = "0" + seconds;
            if (minutes.Length == 1) minutes = "0" + minutes;
        }
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            best.GetComponent<Text>().text = "Best: " + minutes + ":" + seconds;
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            best.GetComponent<Text>().text = "Рекорд: " + minutes + ":" + seconds;
        }

        GameObject win = gameObject.transform.FindChild("Win").gameObject;
        temp = PlayerPrefs.GetInt("GamesWon" + size);
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            win.GetComponent<Text>().text = "Win: " +temp;
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            win.GetComponent<Text>().text = "Верно: " + temp;
        }

        GameObject lost = gameObject.transform.FindChild("Lost").gameObject;
         temp = PlayerPrefs.GetInt("GamesLost" + size);
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            lost.GetComponent<Text>().text = "Lost: " + temp;
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            lost.GetComponent<Text>().text = "Неверно: " + temp;
        }
    }
}
