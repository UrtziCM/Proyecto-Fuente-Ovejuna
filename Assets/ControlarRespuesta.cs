using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlarRespuesta : MonoBehaviour
{
    private GameObject padreIzquierda;
    private GameObject padreDerecha;
    [SerializeField] private Sprite spriteNeutral;
    [SerializeField] private Sprite spriteCorrecto;
    [SerializeField] private Sprite spriteIncorrecto;
    private Button[] botonesIzquierda;
    private Button[] botonesDerecha;
    private string[] palabrasEuskera;
    private string[] palabrasPidgin;
    private List<Button> botonesSeleccionados;
    private bool ultimoClicEnIzquierda;
    private float incorrectAnswerVibrationDuration = 0.2f;
    private int contadorCorrectas = 0;
    [SerializeField] private AudioSource audioIncorrecto;
    [SerializeField] private AudioSource audioCorrecto;


    // Start is called before the first frame update
    void Start()
    {
        palabrasEuskera = new String[] {"Balea", "Buztana", "Lana", "Ongi Etorri", "Esnea", "Ez dakit", "Neska", "Gizona"};
        palabrasPidgin = new String[] {"Balia", "Bustana", "Travala", "Ungetorre", "Usnea", "Ez tacit", "Nesca", "Gissuna"};
        String[] copiaPidgin = new String[] {"Usnea","Travala","Gissuna","Balia","Nesca","Bustana","Ungetorre","Ez tacit"};
        padreIzquierda = this.transform.GetChild(0).gameObject;
        padreDerecha = this.transform.GetChild(1).gameObject;

        botonesIzquierda = padreIzquierda.GetComponentsInChildren<Button>();
        botonesDerecha = padreDerecha.GetComponentsInChildren<Button>();

        int i = 0;
        foreach (Button b in botonesIzquierda)
        {
            b.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = palabrasEuskera[i];
            PonerImagenPorDefecto(b);
            b.onClick.AddListener(() => OnBotonIzquierdoClicado(b));
            i++;
        }
        i = 0;
        foreach (Button b in botonesDerecha)
        {
            b.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = copiaPidgin[i];
            PonerImagenPorDefecto(b);
            b.onClick.AddListener(() => OnBotonDerechoClicado(b));
            i++;
        }

        botonesSeleccionados = new List<Button>();
        ultimoClicEnIzquierda = true;
    }

    public void Comprobar()
    {
        if (botonesSeleccionados.Count == 2)
        {
            string palabraCorrecta = ObtenerPalabraCorrecta();
            bool esCorrecto = botonesSeleccionados[0].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == palabraCorrecta;

            foreach (Button boton in botonesSeleccionados)
            {
                MostrarResultado(boton, esCorrecto);
            }

            // Realiza acciones adicionales según sea necesario para comprobar
            // Puedes añadir aquí lógica adicional según tus necesidades

            // Limpiar la lista de botones seleccionados después de la comprobación
            botonesSeleccionados.Clear();
        }
    }

    public void SeleccionarBoton(Button boton)
    {
        if (!botonesSeleccionados.Contains(boton))
        {
            botonesSeleccionados.Add(boton);
        }
    }

    private string ObtenerPalabraCorrecta()
    {
        // Puedes implementar lógica para obtener la palabra correcta según tus reglas
        // En este ejemplo, se asume que la palabra correcta está en el array de palabrasPidgin
        return palabrasPidgin[Array.IndexOf(palabrasEuskera, botonesSeleccionados[0].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)];
    }

  private void MostrarResultado(Button boton, bool esCorrecto)
  { 
    Image img = boton.GetComponentInChildren<Image>();
    img.sprite = esCorrecto ? spriteCorrecto : spriteIncorrecto;
  }


    void OnBotonIzquierdoClicado(Button boton)
    {
        if (!ultimoClicEnIzquierda)
        {
            return; // Ignorar clics en la izquierda consecutivos
        }

        SeleccionarBoton(boton);

        // Lógica para manejar el clic en el botón izquierdo
        if (botonesSeleccionados.Count == 2)
        {
            Comprobar();
        }
        else
        {
            // Cambiar el sprite del primer botón a SpriteCorrecto
            CambiarSpriteBoton(boton, spriteCorrecto);
            ultimoClicEnIzquierda = false;
        }
    }


   void OnBotonDerechoClicado(Button boton)
    {
         if (ultimoClicEnIzquierda)
        {
            return; // Ignorar clics en la derecha consecutivos
        }

        // Lógica para manejar el clic en el botón derecho
        SeleccionarBoton(boton);

        // Cambiar el sprite del segundo botón a SpriteCorrecto si la palabra coincide con la primera selección
        if (botonesSeleccionados.Count == 2)
        {
            if (SonParejaCorrecta())
            {
                // Si la pareja es correcta, cambiar el sprite de ambos a SpriteCorrecto y luego desaparecer
                StartCoroutine(MostrarYDesaparecer(botonesSeleccionados[0], botonesSeleccionados[1]));
            }
            else
            {
                // Si las palabras no coinciden, agitar los botones y cambiar el sprite al incorrecto
                StartCoroutine(AgitacionYRestauracion(botonesSeleccionados[0], botonesSeleccionados[1]));
            }

            // Limpiar la lista después de la comprobación
            botonesSeleccionados.Clear();
            ultimoClicEnIzquierda = true;
        }
    }

    IEnumerator MostrarYDesaparecer(Button boton1, Button boton2)
    {
        // Cambiar el sprite de ambos a SpriteCorrecto
        CambiarSpriteBoton(boton1, spriteCorrecto);
        CambiarSpriteBoton(boton2, spriteCorrecto);
        audioCorrecto.Play();

        // Esperar un segundo
        yield return new WaitForSeconds(1f);

        // Desaparecer los botones
        boton1.gameObject.SetActive(false);
        boton2.gameObject.SetActive(false);
    }

    IEnumerator AgitacionYRestauracion(Button boton1, Button boton2)
    {
        // Guardar las posiciones iniciales
        Vector3 posicionInicial1 = boton1.transform.position;
        Vector3 posicionInicial2 = boton2.transform.position;

        // Cambiar el sprite al incorrecto
        CambiarSpriteBoton(boton1, spriteIncorrecto);
        CambiarSpriteBoton(boton2, spriteIncorrecto);
        audioIncorrecto.Play();
        StartCoroutine(VibrateOnIncorrectAnswer());

        // Agitar los botones durante un tiempo
        float duracionAgitacion = 0.5f; // Ajusta la duración de la agitación según sea necesario
        float tiempoInicio = Time.time;

        while (Time.time - tiempoInicio < duracionAgitacion)
        {
            // Agitar los botones moviéndolos de manera aleatoria
            boton1.transform.position = posicionInicial1 + UnityEngine.Random.onUnitSphere * 5f;
            boton2.transform.position = posicionInicial2 + UnityEngine.Random.onUnitSphere * 5f;

            yield return null;
        }

        // Volver a las posiciones iniciales
        boton1.transform.position = posicionInicial1;
        boton2.transform.position = posicionInicial2;

        yield return new WaitForSeconds(0.5f);

        // Restaurar los sprites a SpriteNeutral
        CambiarSpriteBoton(boton1, spriteNeutral);
        CambiarSpriteBoton(boton2, spriteNeutral);
    }

    bool SonParejaCorrecta()
    {
        int indice1 = Array.IndexOf(palabrasEuskera, botonesSeleccionados[0].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        int indice2 = Array.IndexOf(palabrasPidgin, botonesSeleccionados[1].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);

        if(indice1==indice2){
            contadorCorrectas++;
        }
        if (contadorCorrectas == 8){
            JuegoTerminado();
        }
        return indice1 == indice2;
    }

    public void JuegoTerminado()
    {
        SceneManager.LoadSceneAsync("Assets/Scenes/WinScreen.unity",LoadSceneMode.Additive);
    }
    void CambiarSpriteBoton(Button boton, Sprite nuevoSprite)
    {
        Image img = boton.GetComponentInChildren<Image>();
        img.sprite = nuevoSprite;
    }
    public void PonerImagenPorDefecto(Button boton)
    {
        Image img = boton.GetComponentInChildren<Image>();
        img.sprite = spriteNeutral;
    }
    private IEnumerator VibrateOnIncorrectAnswer()
    {
        // Verifica si el dispositivo es compatible con la vibración
        if (SystemInfo.supportsVibration)
        {
            // Vibra durante la duración especificada
            Handheld.Vibrate();
            yield return new WaitForSeconds(incorrectAnswerVibrationDuration);
            // Detiene la vibración
            Handheld.Vibrate();
        }
    }
}
