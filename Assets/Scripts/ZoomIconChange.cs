using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomIconChange : MonoBehaviour
{
    public Sprite ZoomIn;
    public Sprite ZoomOut;
    public Image zoomSprite;
    public bool isZoomedOut = false;

    // Start is called before the first frame update
    void Start()
    {
        zoomSprite = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    public void zoomClicked()
    {
        if (isZoomedOut)
        {
            zoomSprite.sprite = ZoomOut;
            isZoomedOut = false;
        }
        else
        {
            zoomSprite.sprite = ZoomIn;
            isZoomedOut = true;
        }
    }
}
