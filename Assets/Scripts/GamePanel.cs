using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private Transform squareTransform;
    [SerializeField] private RectTransform panelTransform;
    [SerializeField] private UnityEngine.UI.Image topPoint;
    [SerializeField] private UnityEngine.UI.Image middlePoint;
    [SerializeField] private UnityEngine.UI.Image bottomPoint;


    public void AdjustSquarePositions()
    {
        float panelTransformCenterX = panelTransform.rect.center.x;

        Vector3 panelTopPoint = panelTransform.TransformPoint(new Vector2(panelTransformCenterX, panelTransform.rect.yMax));
        Vector3 panelMiddlePoint = panelTransform.TransformPoint(panelTransform.rect.center);
        Vector3 panelBottomPoint = panelTransform.TransformPoint(new Vector2(panelTransformCenterX, panelTransform.rect.yMin));

        topPoint.transform.position = panelTopPoint;
        middlePoint.transform.position = panelMiddlePoint;
        bottomPoint.transform.position = panelBottomPoint;

        Vector3 panelCenterPointWorld = Camera.main.ScreenToWorldPoint(panelTopPoint);
        squareTransform.position = panelCenterPointWorld;

    }
    // Start is called before the first frame update
    void Start()
    {
        AdjustSquarePositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
