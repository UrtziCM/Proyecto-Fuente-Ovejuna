using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PTQuizUI : MonoBehaviour
{
    [SerializeField] private PTQuizManager quizManager;
    [SerializeField] private Text questionText;
    [SerializeField] private GameObject inputFieldObject; // Cambiado a GameObject
    [SerializeField] private Sprite correct;
    [SerializeField] private Button botonRespuesta;
    [SerializeField] private Sprite incorrect;
    [SerializeField] private Sprite normal;
    [SerializeField] private GameObject imagenPregunta;

    private Question2 question;
    private bool answered;
    private float incorrectAnswerVibrationDuration = 0.2f;


    public void SetQuestion(Question2 question)
    {
        this.question = question;
        questionText.text = question.questionInfo;
        botonRespuesta.image.sprite = normal;

        // Asigna el sprite asociado
        if (question.questionSprite != null)
        {
            imagenPregunta.GetComponent<Image>().sprite = question.questionSprite;
        }

        // Obtener el componente InputField directamente del GameObject
        TMP_InputField inputField = inputFieldObject.GetComponent<TMP_InputField>();
        inputField.text = ""; // Limpiar el inputField
        answered = false;
    }


    public void OnSubmit()
    {
        if (!answered)
        {
            answered = true;

            // Obtener el componente InputField directamente del GameObject
            TMP_InputField inputField = inputFieldObject.GetComponent<TMP_InputField>();
            string inputText = inputField.text;

            bool val = quizManager.Answer(inputText, question.correctAns);

            if (val)
            {
                botonRespuesta.image.sprite = correct;
            }
            else
            {
                botonRespuesta.image.sprite = incorrect;
                StartCoroutine(VibrateOnIncorrectAnswer());
            }
        }
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

