using Unity.AppUI.UI;
using UnityEngine;

public class ClientProblem : MonoBehaviour
{
    public ProblemServerData data;

    [Header("Ui Setup")]
    [SerializeField] private TMPro.TextMeshPro quetionTask;
    [SerializeField] private Button[] optionsButton;

    [Header("Index data")]
    [SerializeField] int problem;
    [SerializeField] int question;
    [SerializeField] int answer;
    [SerializeField] int options;
    
    public void CheckAnswer(int ans)
    {
        if (ans == answer)
        {
            ClientServerManager.instance.RightAnswer();
        }
        else
        {
            ClientServerManager.instance.BadAnswer();
        }
    }
}
