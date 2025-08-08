using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string weapon;

    void Update()
    {
        string getItemName = PickupObj.currentlyHeldItem?.ItemName;
        if (getItemName == weapon && PlayerController.interact.WasPressedThisFrame())
        {
            //animator.SetBool("Attack", true);
            animator.Play("Attack");
        }
    }
}
