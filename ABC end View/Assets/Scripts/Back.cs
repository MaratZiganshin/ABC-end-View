using UnityEngine;
using UnityEngine.SceneManagement;

//выход на начальный экран
public class Back : MonoBehaviour {
    void OnMouseUpAsButton()
    {
        if (gameObject.name=="Back")
        {
            SceneManager.LoadScene("main");
        }
    }
}
