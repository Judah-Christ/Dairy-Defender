using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuSlide : MonoBehaviour
{
    public float activeXPosition = -753.6f;
    public float activeYPosition = 344.1f;
    public float inactiveXPosition = -733.4f;
    public float inactiveYPosition = -443.6f;
    public float inactiveSize = 0.5058114f;
    public float activeSize = 1f;
    private float duration = 0.03f;

    private RectTransform iconButton;
    private Vector2 activePos;
    private Vector2 inactivePos;
    private Vector2 activeScale;
    private Vector2 inactiveScale;

    private PlayerController playerController;

    void Start()
    {
        iconButton = GetComponent<RectTransform>();
        
        inactivePos = new Vector2(inactiveXPosition, inactiveYPosition);
        activePos = new Vector2(activeXPosition, activeYPosition);

        inactiveScale = new Vector2(inactiveSize, inactiveSize);
        activeScale = new Vector2(activeSize, activeSize);

        iconButton.anchoredPosition = inactivePos;
        iconButton.localScale = inactiveScale;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void UpgradeButtonClick()
    {
        Vector2 endPos;
        Vector2 endScale;

            playerController.OpenUpgradeMenu();
            endPos = activePos;
            endScale = activeScale;
            StartCoroutine(UpgradeMenuTransition(endPos, endScale));
            gameObject.GetComponent<Button>().interactable = false;
    }

    public void CloseButtonHandler()
    {
        Vector2 endPos;
        Vector2 endScale;

        endPos = inactivePos;
        endScale = inactiveScale;
        StartCoroutine(UpgradeMenuTransition(endPos, endScale));
        gameObject.GetComponent<Button>().interactable = true;
    }

    public IEnumerator UpgradeMenuTransition(Vector2 endP, Vector2 endS)
    {
        Vector2 startPos = iconButton.anchoredPosition;
        Vector2 startScale = iconButton.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            iconButton.anchoredPosition = Vector2.Lerp(startPos, endP, t);
            iconButton.localScale = Vector2.Lerp(startScale, endS, t);

            yield return null;
        }

        iconButton.anchoredPosition = endP;
        iconButton.localScale = endS;
    }
}
