using UnityEngine;
using Generator;

public class DLScript : MonoBehaviour {

	public int N;
	public int numberOfLetters;
	public char[] upperRow;
	public char[] leftColumn;
	public char[] lowerRow;
	public char[] rightColumn;

	void Awake()
	{
        upperRow = new char[N];
        leftColumn = new char[N];
        lowerRow= new char[N];
        rightColumn = new char[N];
        for (int i = 0; i < N; i++) 
		{
			upperRow[i]=' ';
			leftColumn[i]=' ';
			lowerRow[i]=' ';
			rightColumn[i]=' ';
		}
	}
		
}
