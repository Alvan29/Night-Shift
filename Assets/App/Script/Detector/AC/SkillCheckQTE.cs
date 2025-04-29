using UnityEngine;
using DG.Tweening;

public class SkillCheckQTE : MonoBehaviour
{
    [SerializeField] private Transform player;

    private int successCount = 0;
    public int requiredSuccess = 5;

    private void Start()
    {
        gameObject.SetActive(true);
        transform.DORotate(new Vector3(0, 0, 0), 1.5f);
        transform.DOScale(new Vector3(1, 1, 1), 1.5f);
    }
    void Update()
    {
        transform.LookAt(player);
    }

    public void CheckQTE()
    {
        Debug.Log("Nice");
        successCount++;
        if (successCount == requiredSuccess)
        {
            Debug.Log("QTE Finished!");
            gameObject.SetActive(false);
        }
    }

    public void QTEFailed()
    {
        successCount = 0;
        Debug.Log("Lol");
    }
}
