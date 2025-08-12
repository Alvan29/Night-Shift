using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerScreenController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Reference")]
    [SerializeField] private Canvas serverGamesCanvas;

    private bool isMouseOver = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0))
        {
            ExecuteEvents.Execute(serverGamesCanvas.gameObject,
                new PointerEventData(EventSystem.current),
                ExecuteEvents.pointerClickHandler);
        }
    }
}
