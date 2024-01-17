using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTextoSinImg : MonoBehaviour
{

    private GameObject boton;

    // Start is called before the first frame update
    void Start()
    {
        boton= gameObject.transform.Find("Button").gameObject;
        Button botonComponent = boton.GetComponent<Button>();
        botonComponent.onClick.AddListener(JuegoTerminado);

    }

    public void JuegoTerminado()
    {
        
    }
}
