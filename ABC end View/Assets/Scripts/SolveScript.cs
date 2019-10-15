using UnityEngine;
using Generator;
using UnityEngine.UI;

//решение головоломки по заданным пользователем входным данным
public class SolveScript : MonoBehaviour {

    public DLScript Canvas;

    public GameObject Field;

    public Sprite ready, readyActive;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Language") || PlayerPrefs.GetInt("Language") == 0)
        {
            gameObject.GetComponentInChildren<Text>().text = "Solve";
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            gameObject.GetComponentInChildren<Text>().text = "Решить";
        }
    }

    void OnMouseDown()
    {
        GetComponent<Image>().sprite = readyActive;
    }
    void OnMouseUp()
     {
         GetComponent<Image>().sprite = ready;
     }
    void OnMouseUpAsButton()
    {
        char[] upperRow = Canvas.upperRow;
        char[] leftColumn = Canvas.leftColumn;
        char[] lowerRow = Canvas.lowerRow;
        char[] rightColumn = Canvas.rightColumn;
        Matrix m = new Matrix(Canvas.N, Canvas.numberOfLetters, upperRow, leftColumn,lowerRow, rightColumn);
        DancingLinks dl = new DancingLinks(m);
        dl.Solver3();
        char[,] solution = dl.OutSolution();
        for (int i = 0; i < Canvas.N; i++)
        {
            for(int j = 0; j < Canvas.N; j++)
            {
                string s = "(" + i.ToString() + "," + j.ToString() + ")";
                //GameObject Letter = GameObject.Find("Canvas/Field/(1,1)");
                GameObject Letter = Field.transform.Find(s).gameObject;
                Letter.GetComponentInChildren<Text>().text = solution[i, j].ToString();
            }
        }
    }
}
