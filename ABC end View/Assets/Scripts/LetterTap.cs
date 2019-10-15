using UnityEngine;
using UnityEngine.UI;

//обработка нажатия на клетки поля
public class LetterTap : MonoBehaviour {
    public Text Letter;
    public Sprite Letter2, activeLetter;
    public GenerateField CanvasGen;
    void OnMouseDown()
    {
        GetComponent<Image>().sprite = activeLetter;
    }
    void OnMouseUp()
    {
        GetComponent<Image>().sprite = Letter2;
    }
    void OnMouseUpAsButton()
    {
        if (Letter.text == " "||Letter.text=="") Letter.text = "A";
        else if ((Letter.text[0] - 65) < CanvasGen.Field.numberOfLetters - 1) { char temp = Letter.text[0]; temp++; Letter.text = temp.ToString(); }
        else Letter.text = " ";
        int i, j;
        int.TryParse(gameObject.name.Substring(1, 1), out i);
        int.TryParse(gameObject.name.Substring(3, 1), out j);
        CanvasGen.userField[i, j] = Letter.text[0];
    }
}
