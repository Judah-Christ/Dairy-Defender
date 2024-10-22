using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLevelChange : MonoBehaviour
{

    private NavMeshAgent agent;
    private Transform enemyUseLadder;
    private SpriteRenderer sr;
    public Vector3 originalSize = Vector3.one;
    public Vector3 shrunkSize = new Vector3(0.75f, 0.75f, 0.75f);
    private float sizeChangeSpeed = 1.5f;
    private bool isUsingLadder = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyUseLadder = transform;
        sr = GetComponentInChildren<SpriteRenderer>();
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
            sr.sortingLayerName = "OnCounter";
            sr.sortingOrder = 0;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Floor");
            sr.sortingLayerName = "OnFloor";
            sr.sortingOrder = 0;
        }

        isUsingLadder = false;
    }

    private IEnumerator ChangeSize(Vector3 targetSize)
    {
        Vector3 startSize = enemyUseLadder.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < 1f / sizeChangeSpeed)
        {
            elapsedTime += Time.deltaTime * sizeChangeSpeed;
            enemyUseLadder.localScale = Vector3.Lerp(startSize, targetSize, elapsedTime);
            yield return null;
        }

        enemyUseLadder.localScale = targetSize;
    }
}
