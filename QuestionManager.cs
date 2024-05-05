using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class QuestionManager : MonoBehaviour
{
    public GameObject[] questionObjects; // Array of question objects
    public SoundManager soundManager;
    public GameObject GameWinScreen;
    public GameObject GameLoseScreen;

    
    private int[] correctAnswers = { 3, 2, 1, 1, 1, 1, 3, 1, 2, 2 }; // Index of correct answer for each question

    private int currentQuestionIndex = 0;
    private bool questionAnswered = false;
    public  TMP_Text scoreText;
    public  TMP_Text userNameTxt;
    private int score = 0;
    public PlayerController playerController;

    private void Start()
    {
        Time.timeScale = 1f;
        userNameTxt.text = PlayerPrefs.GetString("username", "");
        // Disable all question objects except the first one
        for (int i = 1; i < questionObjects.Length; i++)
        {
            questionObjects[i].SetActive(false);
        }
        //  DisplayQuestion();
    }

    public void CheckAnswer(int answerIndex)
    {
        if (!questionAnswered)
        {
            questionAnswered = true;
            if (answerIndex == correctAnswers[currentQuestionIndex])
            {
                // Correct answer
                score += 100;
                scoreText.text = "Score : " + score;
                Debug.Log("Correct!");
                Time.timeScale = 1f;
                soundManager.PlayrightSoundEffect();
                questionObjects[currentQuestionIndex].SetActive(false);
                currentQuestionIndex++;
                playerController.Jump();
                // Add your score handling logic here
            }
            else
            {
                soundManager.PlaywrongSoundEffect();
                score -= 0;
                scoreText.text = "Score : " + score;

                // Wrong answer
                Debug.Log("Wrong!");
                Time.timeScale = 1f;

                questionObjects[currentQuestionIndex].SetActive(false);
                currentQuestionIndex++;
                playerController.Jump();
                // Add your score handling logic here
            }
        }
    }

    private void DisplayQuestion()
    {
        //questionObjects[currentQuestionIndex].SetActive(true);
        //questionText.text = questions[currentQuestionIndex];
        //for (int i = 0; i < options[currentQuestionIndex].Length; i++)
        //{
        //    answerButtons[i].GetComponentInChildren<Text>().text = options[currentQuestionIndex][i];
        //}
    }

    public void NextQuestion()
    {
        questionAnswered = false;
        
        if (currentQuestionIndex < questionObjects.Length)
        {
            // Enable the next question object
            questionObjects[currentQuestionIndex].SetActive(true);
          //  currentQuestionIndex++;
        }
        else
        {
            Debug.Log("All questions answered!");
            Time.timeScale = 0f;
            if(score>=900)
            {
                GameWinScreen.SetActive(true);
            }
            else
            {
                GameLoseScreen.SetActive(true);
            }
           

            // Handle end of questions
        }

        //DisplayQuestion();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
