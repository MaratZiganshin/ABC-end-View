using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//проверка решения игрока
public class check4x4 : MonoBehaviour {

    public GenerateField CanvasField;

    public Sprite ready, readyActive;


    void Start()
    {
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            gameObject.GetComponentInChildren<Text>().text = "Check";
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            gameObject.GetComponentInChildren<Text>().text = "Проверить";
        }
    }

    void OnMouseDown()
    {
        GetComponent<Image>().sprite = readyActive;
    }
   /*void OnMouseUp()
    {
        GetComponent<Image>().sprite = ready;
    }*/
    void OnMouseUpAsButton()
    {
        char[][,] a = CanvasField.Field.OutSolution;
        bool isRight=false;
        int temp = CanvasField.userField.GetLength(0);
        for (int i = 0; i < a.GetLength(0); i++)
        {
            bool b = false;
            for (int r = 0; r < temp; r++)
            {
                for (int c = 0; c < temp; c++)
                {
                    if (CanvasField.userField[r, c] != a[i][r, c]) b = true;
                }
            }
            if (b == false) isRight = true;
        }
        if (isRight == true)
        {
            if (PlayerPrefs.HasKey("GamesWon" + temp.ToString()))
            {
                int t = PlayerPrefs.GetInt("GamesWon" + temp.ToString());
                PlayerPrefs.SetInt("GamesWon" + temp.ToString(), t + 1);
            }
            else PlayerPrefs.SetInt("GamesWon" + temp.ToString(), 1);
            int k = PlayerPrefs.GetInt("GamesLost" + temp.ToString());
            PlayerPrefs.SetInt("GamesLost" + temp.ToString(), k-1);
            PlayerPrefs.SetFloat("Time", (Time.realtimeSinceStartup - CanvasField.currentTime));
            PlayerPrefs.SetInt("Size", temp);
            PlayerPrefs.Save();
            SceneManager.LoadScene("MessageWin");          
        }
        else SceneManager.LoadScene("MessageLost");
    }
}
