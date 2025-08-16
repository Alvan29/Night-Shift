using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ProblemServerData data;
    [SerializeField] private int currentQuestionIndex = 0;

    [Header("UI References")]
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TMPro.TextMeshProUGUI questionProblem;
    [SerializeField] private TMPro.TextMeshProUGUI questionText;

    //private TMPro.TextMeshPro optionTexts;
    private Image questionImage;

    void Start()
    {
        ProblemServerData.Question question = data.questions[currentQuestionIndex];

        questionProblem.text = question.quetionProblem;
        questionText.text = question.questionText;

        // questionImage.sprite = question.questionImage;
        // questionImage.gameObject.SetActive(question.questionImage != null);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            TMPro.TextMeshProUGUI optionTexts =
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            optionTexts.fontSize = ClientServerManager.instance.textSize;
            optionTexts.text = question.options[i];
        }
    }

    public void CheckAnswer(int answer)
    {
        ProblemServerData.Question question = data.questions[currentQuestionIndex];
        if (answer == question.correctOptionIndex)
        {
            Debug.Log("Benar");
            ClientServerManager.instance.ProcessTask(true);
        }
        else
        {
            Debug.Log("Salah");
            ClientServerManager.instance.ProcessTask(false);
        }
    }
}