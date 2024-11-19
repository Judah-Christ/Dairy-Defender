using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using FMODUnity;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEngine.UI.Image image;
    public SlotController slotController;
    //private bool isTowerPlaced = false;
    public Transform imageLocation;
    public GameObject towerObject;
    public Transform towerLocation;
    public GameObject towerLocationObject;
    public Vector3 offsetTower;
    [HideInInspector] public Transform parentAfterDrag;
    public GameObject[] inventory = new GameObject[8];
    [SerializeField] private ItemSpawnLocationController spawnLocationController;
    [SerializeField] private PlayerInput playerInput;
    private InputAction mousePosition;
    private bool isDragging = false;
    private GameObject dragImage;
    public Canvas invCanvas;
    public GameObject turretTowerZones;
    public GameObject sodaTowerZones;
    private bool raycastForTurretZones = false;
    private bool raycastForSodaZones = false;
    private bool obstruction = false;
    private StudioEventEmitter emitter;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);

        dragImage = new GameObject("DragImage");
        dragImage.transform.SetParent(invCanvas.transform);
        var imageComponent = dragImage.AddComponent<Image>();
        imageComponent.sprite = image.sprite;
        imageComponent.raycastTarget = false;
        dragImage.GetComponent<RectTransform>().sizeDelta = image.rectTransform.sizeDelta;

        if (towerObject.name == "TurretOne" || towerObject.name == "TurretSteel" || towerObject.name == "TurretRust")
        {
            turretTowerZones.SetActive(true);
            raycastForTurretZones = true;
        }
        else if (towerObject.name == "SodaTower" || towerObject.name == "LemonadeTower" || towerObject.name == "TeaTower")
        {
            sodaTowerZones.SetActive(true);
            raycastForSodaZones = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        if (dragImage != null)
        {
            dragImage.transform.position = mousePosition.ReadValue<Vector2>();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        isDragging = false;
        transform.SetParent(parentAfterDrag);

        if (dragImage != null)
        {
            Destroy(dragImage);
        }

        bool isOverTurretZone = false;
        bool isOverSodaZone = false;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
        Collider2D[] hits = Physics2D.OverlapPointAll(worldPoint);

        foreach (Collider2D hit in hits)
        {
            if (raycastForTurretZones && hit.CompareTag("PCTZone"))
            {
                isOverTurretZone = true;
            }
            else if (raycastForSodaZones && hit.CompareTag("SodaTowerZone"))
            {
                isOverSodaZone = true;
            }
        }

        foreach (Collider2D hit in hits)
        {
            if (isOverTurretZone)
            {
                if (hit.CompareTag("PlayerTurretObstructionWithPlayerTurret") || hit.CompareTag("PlayerTurretObstructionWithSodaTower"))
                {
                    obstruction = true;
                    break;
                }
            }
            else if (isOverSodaZone)
            {
                if (hit.CompareTag("SodaTowerObstructionWithSodaTower") || hit.CompareTag("SodaTowerObstructionWithPlayerTurret"))
                {
                    obstruction = true;
                    break;
                }
            }
        }

        if (!obstruction && isOverTurretZone && spawnLocationController.canPlace == true && slotController.isFull == true)
        {
            slotController.isFull = false;
            Time.timeScale = 0;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.towerPlace, this.transform.position);
            var createImage = Instantiate(towerObject, spawnLocationController.spawnPointLocation.transform.position,
                Quaternion.identity) as GameObject;
            image.sprite = null;
            towerObject = null;
            Time.timeScale = 1;
        }
        else if (!obstruction && isOverSodaZone && spawnLocationController.canPlace == true && slotController.isFull == true)
        {
            slotController.isFull = false;
            Time.timeScale = 0;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.towerPlace, this.transform.position);
            var createImage = Instantiate(towerObject, spawnLocationController.spawnPointLocation.transform.position,
                Quaternion.identity) as GameObject;
            SpriteRenderer sr = createImage.GetComponent<SpriteRenderer>();
            float towerY = createImage.transform.position.y;
            float maxY = 15.0f;
            float minY = -16.0f;
            int maxSortingLayer = 999;
            float relativeY = Mathf.Clamp01((maxY - towerY) / (maxY - minY));
            int sortOrder = Mathf.RoundToInt(relativeY * maxSortingLayer);
            sr.sortingOrder = sortOrder;
            image.sprite = null;
            towerObject = null;
            Time.timeScale = 1;
        }
        else 
        {
            transform.SetParent(parentAfterDrag);
            
            if (!isOverSodaZone || !isOverTurretZone)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.errorFeedback, this.transform.position);
            }
            else if (obstruction)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.errorFeedback, this.transform.position);
            }
        }

        raycastForTurretZones = false;
        raycastForSodaZones = false;
        turretTowerZones.SetActive(false);
        sodaTowerZones.SetActive(false);
        obstruction = false;

    }


    // Start is called before the first frame update
    void Start()
    {
        inventory[0] = GameObject.Find("InventorySlot1");
        inventory[1] = GameObject.Find("InventorySlot2");
        inventory[2] = GameObject.Find("InventorySlot3");
        inventory[3] = GameObject.Find("InventorySlot4");
        inventory[4] = GameObject.Find("InventorySlot5");
        inventory[5] = GameObject.Find("InventorySlot6");
        inventory[6] = GameObject.Find("InventorySlot7");
        inventory[7] = GameObject.Find("InventorySlot8");
        playerInput.currentActionMap.Enable();
        mousePosition = playerInput.currentActionMap.FindAction("MousePosition");
        image.enabled = false;
    }

    void Update()
    {
        if(isDragging == true)
        {
            image.transform.position = mousePosition.ReadValue<Vector2>();
        }
        if(slotController.isFull == true)
        {
            image.enabled = true;
        }
        if (slotController.isFull == false)
        {
            image.enabled = false;
        }

    }

}
