using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    public TMP_Text messageText;
    private float timePassed;
    private float duration;
    private bool isVisible;

    public void DisplayMessage(string messageTxt, float durationTime)
    {
        messageText.text = messageTxt;
        duration = durationTime;
        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        timePassed = 0;
        isVisible = true;
    }

    void OnDisable()
    {
        isVisible = false;
    }

    void Update()
    {
        if(isVisible)
            timePassed += Time.deltaTime;

        if (timePassed >= duration)
            gameObject.SetActive(false);
    }
}
