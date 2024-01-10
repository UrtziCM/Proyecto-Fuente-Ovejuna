using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PTQuizManager : MonoBehaviour
{
    [SerializeField] private PTQuizUI quizUi;
    [SerializeField] private AudioSource audioCorrecta;
    [SerializeField] private AudioSource audioIncorrecta;
    [SerializeField] private TextMeshProUGUI correctAnswersText;
    [SerializeField] private TextMeshProUGUI wrongAnswersText;
    [SerializeField]
    private List<Question2> questions;
    private Question2 selectedQuestion;
    private int questionsAnswered = 0;
    private List<Question2> askedQuestions = new List<Question2>(); // Lista de preguntas ya realizadas
    private int correctAnswers = 0;
    private int wrongAnswers = 0;
    private int totalQuestions = 6; // Set the total number of questions

    // Start is called before the first frame update
    void Start()
    {
        UpdateResultText(); // Set initial text
        SelectQuestion();
    }

    void SelectQuestion()
    {
        if (questionsAnswered < totalQuestions)
        {
            // Filtra las preguntas que aún no se han realizado
            List<Question2> availableQuestions = new List<Question2>(questions);
            availableQuestions.RemoveAll(q => askedQuestions.Contains(q));

            if (availableQuestions.Count > 0)
            {
                int val = Random.Range(0, availableQuestions.Count);
                selectedQuestion = availableQuestions[val];
                askedQuestions.Add(selectedQuestion);
                quizUi.SetQuestion(selectedQuestion);
            }
            else
            {
                // Todas las preguntas se han hecho, finaliza el juego
                ShowResult();
            }
        }
        else
        {
            // End the game and show the final result
            ShowResult();
        }
    }

    public bool Answer(string answeredText, string correctAnswer)
    {
        bool correctAns = false;

        if (answeredText.Trim().ToLower() == correctAnswer.Trim().ToLower())
        {
            correctAns = true;
            audioCorrecta.Play();
            correctAnswers++;
        }
        else
        {
            audioIncorrecta.Play();
            wrongAnswers++;
        }

        questionsAnswered++;

        UpdateResultText(); // Actualizar el texto después de cada pregunta

        Invoke("SelectQuestion", 0.4f);
        return correctAns;
    }


    void ShowResult()
    {
        // Display the final result
        correctAnswersText.text = correctAnswers + "";
        wrongAnswersText.text = wrongAnswers + "";
    }

    void UpdateResultText()
    {
        // Update the result text in real-time
        correctAnswersText.text = correctAnswers + "";
        wrongAnswersText.text = wrongAnswers + "";
    }
}

[System.Serializable]
public class Question2
{
    public string questionInfo;
    public string correctAns;
    public Sprite questionSprite; // Sprite asociado a la pregunta
}


