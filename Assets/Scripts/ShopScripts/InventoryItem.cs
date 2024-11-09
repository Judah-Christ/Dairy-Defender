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

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEngine.UI.Image image;
    public SlotController slotController;
    private bool isTowerPlaced = false;
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
        isTowerPlaced = true;

        if (dragImage != null)
        {
            Destroy(dragImage);
        }

        if (spawnLocationController.canPlace == true && slotController.isFull == true)
        {
            slotController.isFull = false;
            Time.timeScale = 0;
            isTowerPlaced = false;
            var createImage = Instantiate(towerObject, spawnLocationController.spawnPointLocation.transform.position,
                Quaternion.identity) as GameObject;
            image.sprite = null;
            towerObject = null;
            Time.timeScale = 1;
        }
        else 
        {
            transform.SetParent(parentAfterDrag);
        }

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
    }

    void Update()
    {
        if(isDragging == true)
        {
            image.transform.position = mousePosition.ReadValue<Vector2>();
        }
    }
}
