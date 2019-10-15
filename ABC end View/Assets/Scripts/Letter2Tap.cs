using UnityEngine;
using UnityEngine.UI;

//обработка нажатия на клетки по краю таблицы
public class Letter2Tap : MonoBehaviour {

	public DLScript Canvas;
	void OnMouseUpAsButton()
	{
		Text Letter = GetComponentInChildren<Text>();
		if (Letter.text == " "||Letter.text=="") Letter.text = "A";
		else if ((Letter.text[0] - 65) < Canvas.numberOfLetters - 1) { char temp = Letter.text[0]; temp++; Letter.text = temp.ToString(); }
		else Letter.text = " ";
		int i;
		int.TryParse (gameObject.name [gameObject.name.Length - 1].ToString(), out i);
		switch(gameObject.name.Substring(0,2))
		{
			case "up": Canvas.upperRow [i] = Letter.text[0];	break;
			case "le": Canvas.leftColumn [i] = Letter.text[0];	break;
			case "lo": Canvas.lowerRow [i] = Letter.text[0];	break;
			case "ri": Canvas.rightColumn [i] = Letter.text[0];	break;
		}
	}
}
