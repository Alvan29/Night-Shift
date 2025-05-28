using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchClickTrigger : MonoBehaviour
{
    [SerializeField] private LightOnOff[] targetLights;
    //private Animator animator;
    //private bool trigger;

    private void Start()
    {
       // animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //trigger = animator.GetBool("Trigger");

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryActivateByPointer(Mouse.current.position.ReadValue());
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            TryActivateByPointer(touchPos);
        }
#endif
    }

    private void TryActivateByPointer(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                ToggleSwitch();
            }
        }
    }

    private void ToggleSwitch()
    {
        foreach (var light in targetLights)
        {
            if (light != null)
                light.Toggle();
        }

        //animator.SetBool("First", true);
        //animator.SetBool("Trigger", !trigger);
    }
}
