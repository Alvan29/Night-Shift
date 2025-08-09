using UnityEngine;
using DG.Tweening;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float tweenDuration;
    [SerializeField] private float zoomView;
    private float defaultView;
    private void Start()
    {
        defaultView = cam.fieldOfView;
    }
    void Update()
    {
        if (!PlayerManager.instance.isUsingComputer)
        {
            cam.DOFieldOfView(zoomView, tweenDuration).SetEase(Ease.InOutQuad);
        }
        else
        {
            cam.DOFieldOfView(defaultView, tweenDuration).SetEase(Ease.InOutQuad);
        }
    }
}
