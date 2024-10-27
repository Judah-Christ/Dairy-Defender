using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgMenuBGChange : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public Image backgroundSprite;

    private PlayerController playerController;
    private RectTransform rt;

    private Vector2 inactiveScale = new Vector2(3.112218f, 3.112218f);
    private Vector2 activeScale = Vector2.one;

    private Vector2 inactivePosition = new Vector2(-733.9058f, -444.0552f);
    private Vector2 activePosition = new Vector2(-759.8f, 0f);

    private Vector2 inactiveSize = new Vector2(76f, 44f);
    private Vector2 activeSize = new Vector2(400f, 1080f);

    private float duration = 0.04f;

    void Start()
    {
        backgroundSprite = gameObject.GetComponent<Image>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        rt = gameObject.GetComponent<RectTransform>();
    }

    public void upgradeIconClicked()
    {
        playerController.OpenUpgradeMenu();
        gameObject.GetComponent<Button>().interactable = false;
        StartCoroutine(BGStateSwitch());
    }

    public void upgradeMenuCloseButtonHandler()
    {
        gameObject.GetComponent<Button>().interactable = true;
        StartCoroutine(BGStateSwitch());

    }

    private IEnumerator BGStateSwitch()
    {
        Vector2 initialScale = rt.localScale;
        Vector2 initialPosition = rt.anchoredPosition;
        Vector2 initialSize = rt.sizeDelta;

        Vector2 targetScale = (backgroundSprite.sprite == inactiveSprite) ? activeScale : inactiveScale;
        Vector2 targetPosition = (backgroundSprite.sprite == inactiveSprite) ? activePosition : inactivePosition;
        Vector2 targetSize = (backgroundSprite.sprite == inactiveSprite) ? activeSize : inactiveSize;

        backgroundSprite.sprite = (backgroundSprite.sprite == inactiveSprite) ? activeSprite : inactiveSprite;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            rt.localScale = Vector2.Lerp(initialScale, targetScale, t);
            rt.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);
            rt.sizeDelta = Vector2.Lerp(initialSize, targetSize, t);

            yield return null;
        }

        rt.localScale = targetScale;
        rt.anchoredPosition = targetPosition;
        rt.sizeDelta = targetSize;
    }
}
