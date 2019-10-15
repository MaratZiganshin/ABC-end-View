using UnityEngine;
using UnityEngine.UI;

//выбор языка интерфейса
public class chooseLanguge : MonoBehaviour {

    public Text rules0;
    public Text rules1; 

    void Start()
    {
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            rules0.gameObject.SetActive(true);
            rules1.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            rules1.gameObject.SetActive(true);
            rules0.gameObject.SetActive(false);
        }
    }
}
