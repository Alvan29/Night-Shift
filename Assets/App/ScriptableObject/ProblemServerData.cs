using UnityEngine;

[CreateAssetMenu(fileName = "ProblemServerData", menuName = "Scriptable Objects/ProblemServerData")]
public class ProblemServerData : ScriptableObject
{
    public string[] problemName;

    public Sprite[] notifyIcon;
    public string[] question;
    public string[] answer;
}
