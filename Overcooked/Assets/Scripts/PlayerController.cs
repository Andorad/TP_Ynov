using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController characterController;
    private GameManager gameManager;
    private Vector3 direction;


    public Item actualItemInHands;

    [SerializeField]
    private Detector detector;
    [SerializeField]
    private Animator animator;

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
    [SerializeField]
    private float cookTime;

    private void Awake()
    {
        canMove = true;
        characterController = GetComponent<CharacterController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            characterController.Move(move * speed * Time.deltaTime);
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
                else if(detector.gameObjectsInRange.Count == 0 && detector.cuttingBoards.Count > 0 && detector.hotPlates.Count == 0 && detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem != null)
                {
                    StartCoroutine(Cut());
                }
                else if(detector.gameObjectsInRange.Count == 0 && detector.hotPlates.Count > 0 && detector.cuttingBoards.Count == 0 && detector.hotPlates[0].GetComponent<HotPlate>()._actualItem != null)
                {
                    StartCoroutine(Cook());
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
            detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem = null;
            detector.cuttingBoards[0].GetComponent<CuttingBoard>().UpdateVisual();
            detector.cuttingBoards.RemoveAt(0);
        }
        else if(detector.hotPlates.Count > 0)
        {
            actualItemInHands = detector.hotPlates[0].GetComponent<HotPlate>()._actualItem;
            detector.cuttingBoards[0].GetComponent<HotPlate>()._actualItem = null;
            detector.hotPlates[0].GetComponent<HotPlate>().UpdateVisual();
            detector.hotPlates.RemoveAt(0);
        }
        else if(detector.gameObjectsInRange.Count == 1)
        {
            actualItemInHands = detector.gameObjectsInRange[0].GetComponent<Item>();
            detector.gameObjectsInRange[0].SetActive(false);
            detector.gameObjectsInRange.RemoveAt(0);
        }
        else
        {
            actualItemInHands = detector.gameObjectsInRange[0].GetComponent<Item>();
            detector.gameObjectsInRange[0].SetActive(false);
            detector.gameObjectsInRange.RemoveAt(0);
            //Prendre le plus proche
        }
        UpdateVisual();
    }

    private void Throw()
    {
        //On lance sur une planche à découper
        if(detector.cuttingBoards.Count > 0 && detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem == null && (actualItemInHands.itemData.itemType == ItemType.Cut || actualItemInHands.itemData.itemType == ItemType.CutAndCook) && !actualItemInHands.isCut)
        {
            detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem = actualItemInHands;
            detector.cuttingBoards[0].GetComponent<CuttingBoard>().UpdateVisual(actualItemInHands.itemData);
        }
        //On lance sur une plaque de cuisson
        else if(detector.hotPlates.Count > 0 && detector.hotPlates[0].GetComponent<HotPlate>()._actualItem == null && (actualItemInHands.itemData.itemType == ItemType.Cook || (actualItemInHands.itemData.itemType == ItemType.CutAndCook && actualItemInHands.isCut)) && !actualItemInHands.isCooked)
        {
            detector.hotPlates[0].GetComponent<HotPlate>()._actualItem = actualItemInHands;
            detector.hotPlates[0].GetComponent<HotPlate>().UpdateVisual(actualItemInHands.itemData);
        }
        //On vend une assiette
        else if(detector.plateGiver.Count > 0 && actualItemInHands.itemData.itemType == ItemType.Plate)
        {
            Sell();
        }
        //On lance sur une assiette
        else if(detector.gameObjectsInRange.Count > 0)
        {
            if(detector.gameObjectsInRange[0].GetComponent<Plate>())
            {
                if(actualItemInHands.itemData.itemType == ItemType.Default || (actualItemInHands.itemData.itemType == ItemType.Cut && actualItemInHands.isCut) || (actualItemInHands.itemData.itemType == ItemType.Cook && actualItemInHands.isCooked) || (actualItemInHands.itemData.itemType == ItemType.CutAndCook && actualItemInHands.isCut && actualItemInHands.isCooked))
                {
                detector.gameObjectsInRange[0].GetComponent<Plate>().itemsInsidePlate.Add(actualItemInHands);
                detector.gameObjectsInRange[0].GetComponent<Plate>().UpdateVisual(actualItemInHands.itemData);
                }
                else
                {
                actualItemInHands.gameObject.transform.position = spawnPoint.transform.position;
                actualItemInHands.gameObject.transform.rotation = spawnPoint.transform.rotation;
                actualItemInHands.gameObject.SetActive(true);
                }
            }
        }
        //On lance par terre
        else
        {
            actualItemInHands.gameObject.transform.position = spawnPoint.transform.position;
            actualItemInHands.gameObject.transform.rotation = spawnPoint.transform.rotation;
            actualItemInHands.gameObject.SetActive(true);
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
             switch(actualItemInHands.itemData.name)
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
                case "Plate":
                    visuals[4].SetActive(true);
                    break;
            }
        }
    }

    private void Sell()
    {
        int goodItems = 0;
        if(actualItemInHands.itemData.name == "Plate")
        {
            foreach(ItemData i in gameManager.actualRecipeList[0].items)
            {
                foreach(Item y in actualItemInHands.gameObject.GetComponent<Plate>().itemsInsidePlate)
                {
                    if(i.name == y.itemData.name)
                    {
                        goodItems++;
                    }
                }
            }

            if(goodItems == gameManager.actualRecipeList[0].items.Length)
            {
                goodItems = 0;
                gameManager.recipesToDestroy.RemoveAt(1);
                gameManager.AddScore();
            }
        }
    }

    IEnumerator Cut()
    {
        canMove = false;
        animator.SetTrigger("Cut");
        yield return new WaitForSeconds(cutTime);
        detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem.isCut = true;
        actualItemInHands = detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem;
        detector.cuttingBoards[0].GetComponent<CuttingBoard>()._actualItem = null;
        detector.cuttingBoards[0].GetComponent<CuttingBoard>().UpdateVisual();
        UpdateVisual();
        canMove = true;
    }

    IEnumerator Cook()
    {
         canMove = false;
        //PlayAnimation
        yield return new WaitForSeconds(cookTime);
        detector.hotPlates[0].GetComponent<HotPlate>()._actualItem.isCooked = true;
        actualItemInHands = detector.hotPlates[0].GetComponent<HotPlate>()._actualItem;
        detector.hotPlates[0].GetComponent<HotPlate>()._actualItem = null;
        detector.hotPlates[0].GetComponent<HotPlate>().UpdateVisual();
        UpdateVisual();
        canMove = true;
    }
}
