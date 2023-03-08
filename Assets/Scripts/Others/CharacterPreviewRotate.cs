using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterPreviewRotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float sceneWidth;
    private Vector3 pressPoint;
    private Quaternion startRotation;
    private bool buttonPressed;

    public Transform characterPreviewTransform;

    void Start()
    {
        sceneWidth = Screen.width;
    }

    void Update()
    {
        if (!buttonPressed)
            return;

        float currentDistanceBetweenMousePositions = (Input.mousePosition - pressPoint).x;
        characterPreviewTransform.rotation = startRotation * Quaternion.Euler(Vector3.down * (currentDistanceBetweenMousePositions / sceneWidth) * 360);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        pressPoint = Input.mousePosition;
        startRotation = characterPreviewTransform.rotation;

        buttonPressed = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        buttonPressed = false;
    }
}
