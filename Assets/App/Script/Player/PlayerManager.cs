using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Camera cam;
    public bool canMove;
    public bool canLook;
    public bool isUsingComputer = false;
    public Animator animator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canMove = true;
        canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ViewFocus(bool focus,float duration, float viewFocus, float defaultView)
    {
        if (focus == true)
        {
            cam.DOFieldOfView(viewFocus, duration).SetEase(Ease.InOutQuad);
        }
        else
        {
            cam.DOFieldOfView(defaultView, duration).SetEase(Ease.InOutQuad);
        }
    }
}
