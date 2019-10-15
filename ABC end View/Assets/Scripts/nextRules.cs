using UnityEngine;
using UnityEngine.UI;

//переход к следующий странице правил
public class nextRules : MonoBehaviour {

    public Text previos;
    public Text next;
    void OnMouseUpAsButton()
    {
        previos.gameObject.SetActive(false);
        next.gameObject.SetActive(true);
    }
}
