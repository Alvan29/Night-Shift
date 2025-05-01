using DG.Tweening;
using UnityEngine;

public class QTESucces : MonoBehaviour
{
    [SerializeField] private GameObject successZone;
    [SerializeField] private SkillCheckQTE skillCheck;
    public bool isOverlaping;
    private Tween successZoneTween;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 180f), 0.8f)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
        RandomizeSafeZone();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOverlaping && Input.GetKeyDown(KeyCode.Space))
        {
            skillCheck.CheckQTE();
            RandomizeSafeZone();
        }
        else if(!isOverlaping && Input.GetKeyDown(KeyCode.Space))
        {
            skillCheck.QTEFailed();
        }
    }
    private void RandomizeSafeZone()
    {
        float z = Random.Range(2f, 10f);
        successZoneTween = successZone.transform.DOLocalRotate(new Vector3(0, 0, -45f * z), 1);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Success Zone")
        {
            isOverlaping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Success Zone")
        {
            isOverlaping = false;
        }
    }
}
