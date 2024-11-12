using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteDynamicLayering : MonoBehaviour
{
    private PlayerController pc;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerY = transform.position.y;
        float maxY = 15.0f;
        float minY = -16.0f;
        int maxSortingLayer = 999;
        float relativeY = Mathf.Clamp01((maxY - playerY) / (maxY - minY));
        int sortOrder = Mathf.RoundToInt(relativeY * maxSortingLayer);
        sr.sortingOrder = sortOrder;
    }
}
