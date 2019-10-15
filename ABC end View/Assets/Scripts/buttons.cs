using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//скрипт всех кнопок и переходов между сценами
public class buttons : MonoBehaviour
{

    public Sprite layerBlue, layerDark;

    void Start()
    {
        if (gameObject.name == "Settings")
        {
            if (!PlayerPrefs.HasKey("Language")) gameObject.GetComponentInChildren<Text>().text = "EN"; else
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0: gameObject.GetComponentInChildren<Text>().text = "EN"; break;
                case 1: gameObject.GetComponentInChildren<Text>().text = "RU"; break;
            }
        }
    }
    void OnMouseDown()
    {
        GetComponent<Image>().sprite = layerDark;
    }
    void OnMouseUp()
    {
        GetComponent<Image>().sprite = layerBlue;
    }
    void OnMouseUpAsButton()
    {
        GetComponent<Image>().sprite = layerBlue;
        switch (gameObject.name)
        {
            case "Play":
                SceneManager.LoadScene("ChooseSize");
                break;
            case "Home":
                SceneManager.LoadScene("main");
                break;
            case "4x4":
                SceneManager.LoadScene("Scene4x4");
                break;
            case "5x5":
                SceneManager.LoadScene("Scene5x5");
                break;
            case "6x6":
                SceneManager.LoadScene("Scene6x6");
                break;
            case "7x7":
                SceneManager.LoadScene("Scene7x7");
                break;
            case "Solver":
                SceneManager.LoadScene("Solver");
                break;
            case "Solver4x4":
                SceneManager.LoadScene("Solver4x4");
                break;
            case "Solver5x5":
                SceneManager.LoadScene("Solver5x5");
                break;
            case "Solver6x6":
                SceneManager.LoadScene("Solver6x6");
                break;
            case "Solver7x7":
                SceneManager.LoadScene("Solver7x7");
                break;
            case "Help":
                SceneManager.LoadScene("Help");
                break;
            case "Stats":
                SceneManager.LoadScene("Stats");
                break;
            case "Settings":
                if (PlayerPrefs.HasKey("Language"))
                {
                    if (PlayerPrefs.GetInt("Language") == 0) PlayerPrefs.SetInt("Language", 1);
                    else PlayerPrefs.SetInt("Language", 0);             
                }
                else PlayerPrefs.SetInt("Language", 1);
                PlayerPrefs.Save();
                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0: gameObject.GetComponentInChildren<Text>().text = "EN"; break;
                    case 1: gameObject.GetComponentInChildren<Text>().text = "RU"; break;
                }
                break;
        }
    }
}
