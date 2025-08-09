using DG.Tweening;
using FIMSpace.FLook;
using UnityEngine;
using UnityEngine.Splines;

public class UseComputer : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private FLookAnimator lookAnimator;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    [SerializeField] private Transform chair;
    [SerializeField] private Transform lookTarget;

    [Header("Settings")]
    [SerializeField] private float tweenDuration = 1f;
    [SerializeField] private float sitHeightOffset = -0.5f;

    private Vector3 originalPlayerPosition;
    private Vector3 originalCamPosition;
    private Quaternion originalPlayerRotation;
    private Quaternion originalCamRotation;

    public void Interact()
    {
        if (PlayerManager.instance.isUsingComputer)
        {
            StopUsingComputer();
        }
        else
        {
            StartUsingComputer();
        }
    }

    private void StartUsingComputer()
    {
        // Save original transforms
        originalPlayerPosition = player.position;
        originalPlayerRotation = player.rotation;
        originalCamRotation = cam.localRotation;
        originalCamPosition = cam.transform.position;

        // Disable controls
        PlayerManager.instance.canLook = false;
        PlayerManager.instance.canMove = false;
        lookAnimator.enabled = false;

        // Move to chair and look at computer
        Vector3 sitPosition = chair.position + Vector3.up * sitHeightOffset;

        player.DOMove(chair.position, tweenDuration).SetEase(Ease.InOutQuad);
        cam.DOMove(sitPosition, tweenDuration).SetEase(Ease.InOutQuad);
        player.DODynamicLookAt(lookTarget.position, tweenDuration, AxisConstraint.None);
        cam.DODynamicLookAt(lookTarget.position, tweenDuration, AxisConstraint.None);

        // Set animation and cursor
        PlayerManager.instance.animator.SetBool("Typing", true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PlayerManager.instance.isUsingComputer = true;
        PlayerManager.instance.ViewFocus(true, tweenDuration, 30f, 60f);
    }

    private void StopUsingComputer()
    {
        // Return to original position
        player.DOMove(originalPlayerPosition, tweenDuration).SetEase(Ease.InOutQuad);
        cam.DOMove(originalCamPosition, tweenDuration).SetEase(Ease.InOutQuad);
        player.DORotateQuaternion(originalPlayerRotation, tweenDuration);
        cam.DOLocalRotateQuaternion(originalCamRotation, tweenDuration)
            .OnComplete(() => {
                // Re-enable controls
                PlayerManager.instance.canLook = true;
                PlayerManager.instance.canMove = true;
                lookAnimator.enabled = true;

                // Reset animation and cursor
                PlayerManager.instance.animator.SetBool("Typing", false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                PlayerManager.instance.isUsingComputer = false;
                PlayerManager.instance.ViewFocus(false, tweenDuration, 30f, 60f);
            });
    }
}