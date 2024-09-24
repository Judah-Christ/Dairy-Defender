using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEngine.UI.Image image;
    public SlotController slotController;
    private bool isTowerPlaced = false;
    public RectInspector imageRectTransform;
    public Transform imageLocation;
    public GameObject towerObject;
    public Transform towerLocation;
    public Vector3 offsetTower;
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 mousePosition;
    [SerializeField] private GameObject spawnPoint;




    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        if (slotController.isFull == true)
        {
            slotController.isFull = false;
            isTowerPlaced = true;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        //imageLocation.transform.position = Input.mousePosition;
       
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if(isTowerPlaced == true)
        {
            Time.timeScale = 0;
            isTowerPlaced = false;
            GetComponent<UnityEngine.UI.Image>().sprite = null;
            UpdateTransform();
            var createImage = Instantiate(towerObject, imageLocation) as GameObject;
            Time.timeScale = 1;
        }

    }

    private void UpdateTransform()
    {
        imageLocation.transform.position += offsetTower;
    }


    // Start is called before the first frame update
    void Start()
    {
        
        //imageRectTransform.
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnPoint.transform.childCount > 0)
        {
            spawnPoint.transform.DetachChildren();
        }
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            imageLocation.transform.position = Vector2.Lerp(transform.position, mousePosition, 10f);
            
            //transform.position = Vector2.Lerp(transform.position, mousePosition);
        }
    }
}
