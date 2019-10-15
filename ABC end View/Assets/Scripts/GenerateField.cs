using UnityEngine;
using Generator;

//генерация поля выбранного размера
public class GenerateField : MonoBehaviour {
    public int N;
    public int numberOfLetters;
    public Gen Field;
    public char[,] userField;
    public float currentTime;
    // Use this for initialization
    void Awake () {

        Field = new Gen(N, numberOfLetters);
        userField = new char[N, N];
        for(int i = 0; i < N; i++)
        {
            for(int j = 0; j < N; j++)
            {
                userField[i, j] = ' ';
            }
        }
        if (PlayerPrefs.HasKey("GamesLost" + N.ToString()))
        {
            int temp=PlayerPrefs.GetInt("GamesLost" + N.ToString());
            PlayerPrefs.SetInt("GamesLost" + N.ToString(), temp + 1);
        }
        else PlayerPrefs.SetInt("GamesLost" + N.ToString(), 1);
        PlayerPrefs.Save();
        currentTime = Time.realtimeSinceStartup;
    }
}
