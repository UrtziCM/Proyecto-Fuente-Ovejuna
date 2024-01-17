using UnityEngine;
using UnityEngine.UI;

public class ScriptImgTexto : MonoBehaviour
{
    private GameObject fondoBlanco;
    private GameObject texto;
    private GameObject boton;
    private GameObject imagen1;
    private GameObject imagen2;
    private GameObject imagen3;

    private int indiceImagenActual = 0;
    private bool juegoTerminado = false;

    private void Start()
    {
        // Obtén las referencias a los elementos hijos directos del Canvas
        fondoBlanco = gameObject.transform.Find("FondoBlanco").gameObject;
        texto = gameObject.transform.Find("Texto").gameObject;
        boton = gameObject.transform.Find("Button").gameObject;
        imagen1 = gameObject.transform.Find("Imagen1").gameObject;
        imagen2 = gameObject.transform.Find("Imagen2").gameObject;
        imagen3 = gameObject.transform.Find("Imagen3").gameObject;

        // Asegúrate de que el GameObject del botón contenga el componente Button
        Button botonComponent = boton.GetComponent<Button>();
        if (botonComponent != null)
        {
            botonComponent.onClick.AddListener(OnBotonClick);
        }
    }

    private void OnBotonClick()
    {
        // Si el juego está terminado, no hagas nada
        if (juegoTerminado)
        {
            return;
        }

        // Oculta fondo blanco y texto
        fondoBlanco.SetActive(false);
        texto.SetActive(false);

        // Oculta la imagen actual
        switch (indiceImagenActual)
        {
            case 1:
                imagen1.SetActive(false);
                break;
            case 2:
                imagen2.SetActive(false);
                break;
            case 3:
                imagen3.SetActive(false);
                break;
        }

        // Muestra la siguiente imagen
        MostrarSiguienteImagen();
    }

    private void MostrarSiguienteImagen()
    {
        // Incrementa el índice y reinicia si se llega al final
        indiceImagenActual++;

        // Muestra la imagen actual
        switch (indiceImagenActual)
        {
            case 1:
                imagen1.SetActive(true);
                break;
            case 2:
                imagen2.SetActive(true);
                break;
            case 3:
                imagen3.SetActive(true);
                JuegoTerminado();
                break;
        }
    }

    public void JuegoTerminado()
    {
        juegoTerminado = true;
    }
}
