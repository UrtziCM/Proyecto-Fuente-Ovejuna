using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private Text questionText;
    [SerializeField] private List<Button> options;
    [SerializeField] private Sprite correct;
    [SerializeField] private Sprite incorrect;
    [SerializeField] private Sprite normal;

    private Question question;
    private bool answered;
    private float incorrectAnswerVibrationDuration = 0.2f;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBt = options[i];
            localBt.onClick.AddListener(() => OnClick(localBt));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        questionText.text = question.questionInfo;

        List<string> answerList = (question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.sprite = normal;
        }

        answered = false;
    }

    private void OnClick(Button bt)
    {
        if (!answered)
        {
            answered = true;
            bool val = quizManager.Answer(bt.name);

            if (val)
            {
                bt.image.sprite = correct;
            }
            else
            {
                bt.image.sprite = incorrect;
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
