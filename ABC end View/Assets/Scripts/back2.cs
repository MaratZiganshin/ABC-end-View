using UnityEngine;
using UnityEngine.SceneManagement;

//выход на экран с выбором размера
public class back2 : MonoBehaviour
{
    void OnMouseUpAsButton(){
          if (gameObject.name=="Back")
          {
          SceneManager.LoadScene("ChooseSize");
          }
        if (gameObject.name == "BackSolve")
        {
            SceneManager.LoadScene("Solver");
        }
    }
}
