using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController characterController;
    private Vector3 direction;


    public ItemData actualItemInHands;

    [SerializeField]
    private Detector detector;

    [SerializeField]
    private List<GameObject> visuals;

    [SerializeField]
    private float speed;

    [SerializeField]
    private KeyCode interact;

    [SerializeField]
    private Transform spawnPoint;

    private bool canMove;

    [SerializeField]
    private float cutTime;

    private void Awake()
    {
        canMove = true;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(canMove)
        {
            characterController.Move(direction * speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(interact))
        {
            if(actualItemInHands != null)
            {
                Throw();
            }
            else
            {
                if(detector.gameObjectsInRange.Count == 0 && detector.cuttingBoards.Count == 0 && detector.hotPlates.Count == 0)
                {
                    return;
                }
                else if(detector.gameObjectsInRange.Count > 0)
                {
                    PickUp();
                }
                else if(detector.gameObjectsInRange.Count == 0 && detector.cuttingBoards.Count > 0 && detector.hotPlates.Count == 0 && !detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem.isCut)
                {
                    StartCoroutine(Cut());
                }
                else if(detector.gameObjectsInRange.Count == 0 && detector.cuttingBoards.Count > 0 && detector.hotPlates.Count == 0)
                {
                    //Cook;
                }

            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        direction = new Vector3(_input.x, -9.81f, _input.y);
    }

    private void PickUp()
    {
        if(detector.cuttingBoards.Count > 0)
        {
            actualItemInHands = detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem;
            detector.cuttingBoards[0].GetComponent<CuttingBoard>().UpdateVisual();
            detector.cuttingBoards.RemoveAt(0);
        }
        else if(detector.hotPlates.Count > 0)
        {
            actualItemInHands = detector.hotPlates[0].GetComponent<HotPlate>()._actualItem;
            detector.hotPlates[0].GetComponent<HotPlate>().UpdateVisual();
            detector.hotPlates.RemoveAt(0);
        }
        else if(detector.gameObjectsInRange.Count == 1)
        {
            actualItemInHands = detector.gameObjectsInRange[0].GetComponent<Item>().itemData;
            Destroy(detector.gameObjectsInRange[0]);
            detector.gameObjectsInRange.RemoveAt(0);
        }
        else
        {
            actualItemInHands = detector.gameObjectsInRange[0].GetComponent<Item>().itemData;
            Destroy(detector.gameObjectsInRange[0]);
            detector.gameObjectsInRange.RemoveAt(0);
            //Mettre le plus près ?
        }
        UpdateVisual();
    }

    private void Throw()
    {
        if(detector.cuttingBoards.Count > 0 && detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem == null && actualItemInHands.itemType == ItemType.Cut && !actualItemInHands.isCut)
        {
            detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem = actualItemInHands;
            detector.cuttingBoards[0].GetComponent<CuttingBoard>().UpdateVisual(actualItemInHands);
        }
        else if(detector.hotPlates.Count > 0 && detector.hotPlates[0].GetComponent<HotPlate>()._actualItem == null && actualItemInHands.itemType == ItemType.Cook &&!actualItemInHands.isCooked)
        {
            detector.hotPlates[0].GetComponent<HotPlate>()._actualItem = actualItemInHands;
            detector.hotPlates[0].GetComponent<HotPlate>().UpdateVisual(actualItemInHands);
        }
        else
        {
            Instantiate(actualItemInHands.prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        actualItemInHands = null;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        for(int i = 0; i < visuals.Count; i++)
        {
            visuals[i].SetActive(false);
        }

        if(actualItemInHands != null)
        {
             switch(actualItemInHands.name)
            {
                case "Meat":
                    visuals[0].SetActive(true);
                    break;
                case "Rice":
                    visuals[1].SetActive(true);
                    break;
                case "Pancake":
                    visuals[2].SetActive(true);
                    break;
                case "Salad":
                    visuals[3].SetActive(true);
                    break;
            }
        }
    }

    IEnumerator Cut()
    {
        canMove = false;
        //PlayAnimation
        yield return new WaitForSeconds(cutTime);
        detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem.isCut = true;
        actualItemInHands = detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem;
        detector.cuttingBoards[0].GetComponent<CuttingBoard>().UpdateVisual();
        UpdateVisual();
        canMove = true;
    }
}
