using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ScriptAudio : MonoBehaviour
{
    public AudioClip audioClip;
    public Slider audioSlider;
    public Button playPauseButton;

    private AudioSource audioSource;
    private Button botonJarraitu;
    private bool isPlaying = false;
    private bool isDragging = false;

    void Start()
    {
        botonJarraitu = gameObject.transform.Find("BotonJarraitu").gameObject.GetComponent<Button>();
        botonJarraitu.onClick.AddListener(JuegoTerminado);
        audioSource = GetComponent<AudioSource>();
        playPauseButton.onClick.AddListener(TogglePlayPause);
        audioSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Carga el audio correspondiente a la escena actual
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource.clip = audioClip;
        
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
        
    }
}