using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetInitialFocus : MonoBehaviour
{
    public Button initialButton; // El botón que recibirá el enfoque inicialmente

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(initialButton.gameObject);
    }

}