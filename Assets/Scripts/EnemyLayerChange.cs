using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLevelChange : MonoBehaviour
{

    private NavMeshAgent agent;
    private Transform enemyTransform;
    private SpriteRenderer spriteRenderer;
    public Vector3 originalSize = Vector3.one;
    public Vector3 shrunkSize = new Vector3(0.75f, 0.75f, 0.75f);
    private float sizeChangeSpeed = 2f;
    private bool isUsingLadder = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyTransform = transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        if (agent.isOnOffMeshLink && !isUsingLadder)
        {
            isUsingLadder = true;
            StartCoroutine(LayerSwitch());
        }
    }

    private IEnumerator LayerSwitch()
    {
        bool movingToCounter = gameObject.layer == LayerMask.NameToLayer("Floor");

        if (movingToCounter)
        {
            StartCoroutine(ChangeSize(originalSize));
        }
        else
        {
            StartCoroutine(ChangeSize(shrunkSize));
        }

        while (agent.isOnOffMeshLink)
        {
            yield return null;
        }

        if (movingToCounter)
        {
            gameObject.layer = LayerMask.NameToLayer("Counter");
            spriteRenderer.sortingLayerName = "OnCounter";
            spriteRenderer.sortingOrder = 0;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Floor");
            spriteRenderer.sortingLayerName = "OnFloor";
            spriteRenderer.sortingOrder = 0;
        }

        isUsingLadder = false;
    }

    private IEnumerator ChangeSize(Vector3 targetSize)
    {
        Vector3 startSize = enemyTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < 1f / sizeChangeSpeed)
        {
            elapsedTime += Time.deltaTime * sizeChangeSpeed;
            enemyTransform.localScale = Vector3.Lerp(startSize, targetSize, elapsedTime * sizeChangeSpeed);
            yield return null;
        }

        enemyTransform.localScale = targetSize;
    }
}
