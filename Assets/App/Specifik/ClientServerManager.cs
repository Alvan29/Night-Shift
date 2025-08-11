using UnityEngine;

public class ClientServerManager : MonoBehaviour
{
    public static ClientServerManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void RightAnswer() {

    }
    public void BadAnswer() {

    }
}
