using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUi;
    [SerializeField] private AudioSource audioCorrecta;
    [SerializeField] private AudioSource audioIncorrecta;
    [SerializeField] private TextMeshProUGUI correctAnswersText;
    [SerializeField] private TextMeshProUGUI wrongAnswersText;
    [SerializeField]
    private List<Question> questions;
    private Question selectedQuestion;
    private int questionsAnswered = 0;
    private List<Question> askedQuestions = new List<Question>(); // Lista de preguntas ya realizadas
    private int correctAnswers = 0;
    private int wrongAnswers = 0;
    private int totalQuestions = 6; // Set the total number of questions

    // Start is called before the first frame update
    void Start()
    {
        totalQuestions = questions.Count;
        UpdateResultText(); // Set initial text
        SelectQuestion();
    }

    void SelectQuestion()
    {
          if (questionsAnswered < totalQuestions)
        {
            // Filtra las preguntas que aún no se han realizado
            List<Question> availableQuestions = new List<Question>(questions);
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
                JuegoTerminado();
            }
        }
        else
        {
            // End the game and show the final result
            ShowResult();
            JuegoTerminado();
        }
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;

        if (answered == selectedQuestion.correctAns)
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

        UpdateResultText(); // Update the text after each question

        Invoke("SelectQuestion", 0.4f);
        return correctAns;
    }

    void ShowResult()
    {
        // Display the final result
        correctAnswersText.text = correctAnswers+"";
        wrongAnswersText.text = wrongAnswers+"";
    }

    void UpdateResultText()
    {
        // Update the result text in real-time
        correctAnswersText.text = correctAnswers+"";
        wrongAnswersText.text = wrongAnswers+"";
    }

    public void JuegoTerminado()
    {
        Debug.Log("aaa");
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public List<string> options;
    public string correctAns;
}
