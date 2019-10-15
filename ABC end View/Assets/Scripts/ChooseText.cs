using UnityEngine;
using UnityEngine.UI;

//выбор надписи в зависимости от проигрыша или выигрыша
public class ChooseText : MonoBehaviour {
	
	void Start () {
        string en="";
        string ru="";
        switch (gameObject.name)
        {
            case "Lost":
                en = "Wrong:(\nTry Again";
                ru = "Неверно:(\nПопробуйте еще раз";
                break;
            case "Win":
                en = "Right:)";
                ru = "Верно:)";
                break;
        }
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            gameObject.GetComponent<Text>().text = en;
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            gameObject.GetComponent<Text>().text = ru;
        }
    }
}
