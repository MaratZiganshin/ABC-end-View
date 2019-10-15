using UnityEngine;

//переход к следующей странице статистики
public class NextStats : MonoBehaviour
{
    public GameObject next, previos;

    void OnMouseUpAsButton()
    {
         next.SetActive(true);
         previos.SetActive(false);
    }
}
