using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class ScriptAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    public Slider audioSlider;
    public Button playPauseButton;

    private AudioSource audioSource;
    private Button botonJarraitu;
    private bool isPlaying = false;
    private bool isDragging = false;

    // Diccionario que mapea los nombres de escena a índices de audio
    private Dictionary<string, int> sceneAudioIndexMapping;

    void Start()
    {
        // Inicializar el diccionario de mapeo
        InitializeSceneAudioMapping();

        botonJarraitu = gameObject.transform.Find("BotonJarraitu").gameObject.GetComponent<Button>();
        botonJarraitu.onClick.AddListener(JuegoTerminado);
        audioSource = GetComponent<AudioSource>();
        playPauseButton.onClick.AddListener(TogglePlayPause);
        audioSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Recupera el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Verifica si el nombre de la escena existe en el diccionario
        if (sceneAudioIndexMapping.ContainsKey(currentSceneName))
        {
            int audioIndex = sceneAudioIndexMapping[currentSceneName];

            if (audioIndex < audioClips.Length)
            {
                audioSource.clip = audioClips[audioIndex];
            }
        }
        else
        {
            Debug.LogError("No se encontró un índice de audio para la escena: " + currentSceneName);
        }
    }

    void Update()
    {
        if (isPlaying && !isDragging)
        {
            // Actualiza la posición del slider para que coincida con el tiempo de reproducción del audio
            audioSlider.value = audioSource.time / audioSource.clip.length;
        }
    }

    public void OnSliderValueChanged(float value)
    {
        // Cambia la posición de reproducción del audio cuando se mueve el slider
        audioSource.time = value * audioSource.clip.length;
    }

    public void OnSliderDragStart()
    {
        isDragging = true;
    }

    public void OnSliderDragEnd()
    {
        isDragging = false;
    }

    private void TogglePlayPause()
    {
        if (isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
        isPlaying = !isPlaying;
    }

    public void JuegoTerminado()
    {
        // Almacena el índice de la escena actual como la escena anterior
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("PreviousSceneIndex", currentSceneIndex);
    }

    // Método para inicializar el diccionario de mapeo
    void InitializeSceneAudioMapping()
    {
        sceneAudioIndexMapping = new Dictionary<string, int>();

        // Asocia nombres de escena con índices de audio
        sceneAudioIndexMapping.Add("PreguntasTexto", 4);
        sceneAudioIndexMapping.Add("PuzzleOntziolak", 1);
        sceneAudioIndexMapping.Add("PuzzleAbuelas", 3);
        sceneAudioIndexMapping.Add("EscenaJuntarBotones", 2);
        // Agrega más según sea necesario
    }
}
