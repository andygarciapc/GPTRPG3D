using UnityEngine;
using UnityEngine.UI; // Use TMPro if using Text Mesh Pro
using System.Collections;
using TMPro;

public class ErrorMessage : MonoBehaviour
{
    public TMP_Text errorText; // Change this to TMP_Text if using Text Mesh Pro
    public float displayTime = 2.0f;

    void Start()
    {
        errorText.gameObject.SetActive(false);
    }

    public void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
        PlaceTextRandomly();
        StartCoroutine(HideAfterTime());
    }

    private void PlaceTextRandomly()
    {
        // Position the text randomly on the screen within the canvas
        RectTransform canvasRect = errorText.canvas.GetComponent<RectTransform>();
        Vector2 randomPosition = new Vector2(
            Random.Range(0, canvasRect.sizeDelta.x - errorText.rectTransform.sizeDelta.x),
            Random.Range(0, canvasRect.sizeDelta.y - errorText.rectTransform.sizeDelta.y) - 500
        );

        errorText.rectTransform.anchoredPosition = randomPosition;
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        errorText.gameObject.SetActive(false);
    }
}
