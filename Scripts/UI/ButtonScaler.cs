using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button targetButton;
    public float scaleFactor = 1.2f;
    public float animationDuration = 0.1f;

    private Vector3 originalScale;
    private Coroutine currentCoroutine;

    void Start()
    {
        if (targetButton != null)
        {
            originalScale = targetButton.transform.localScale;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleTo(originalScale * scaleFactor));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleTo(originalScale));
    }

    IEnumerator ScaleTo(Vector3 targetScale)
    {
        float currentTime = 0f;
        Vector3 startingScale = targetButton.transform.localScale;

        while (currentTime < animationDuration)
        {
            currentTime += Time.deltaTime;
            targetButton.transform.localScale = Vector3.Lerp(startingScale, targetScale, currentTime / animationDuration);
            yield return null;
        }

        targetButton.transform.localScale = targetScale;
    }
}
