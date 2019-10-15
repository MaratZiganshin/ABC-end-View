using UnityEngine;
using UnityEngine.UI;
using Generator;

//заполнение букв по краю
public class script4x4 : MonoBehaviour {
    public Text Letter;
    public Sprite Letter2, activeLetter;
    public GenerateField CanvasGen;
    // Use this for initialization
    void Start () {
        Gen a = CanvasGen.Field;
        switch (gameObject.name.Substring(0,2))
        {
            case "up":
                int temp;
                int.TryParse(gameObject.name[gameObject.name.Length-1].ToString(),out temp);
                Letter.text = a.upperRow[temp].ToString();
                break;
            case "le":
                int.TryParse(gameObject.name[gameObject.name.Length - 1].ToString(), out temp);
                Letter.text = a.leftColumn[temp].ToString();
                break;
            case "lo":
                int.TryParse(gameObject.name[gameObject.name.Length - 1].ToString(), out temp);
                Letter.text = a.lowerRow[temp].ToString();
                break;
            case "ri":
                int.TryParse(gameObject.name[gameObject.name.Length - 1].ToString(), out temp);
                Letter.text = a.rightColumn[temp].ToString();
                break;
            default: break;
        }
	}
    
    // Update is called once per frame
    void Update () {
		
	}
}
