using UnityEngine;
using UnityEngine.SceneManagement;

//выход на главный экран
public class Home : MonoBehaviour {

    void OnMouseUpAsButton()
    {
        if (gameObject.name == "Home")
        {
            SceneManager.LoadScene("main");
        }
    }
}
