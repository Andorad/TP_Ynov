using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridCreator : MonoBehaviour
{
    [Header("Initialisation du niveau")]

    public List<Transform> deplacementPoints;

    [SerializeField]
    private List<Transform> winConditions;
    [SerializeField]
    private List<Transform> walls;
    [SerializeField]
    private List<Transform> floors;

    [HideInInspector]
    public List<GameObject> boxes;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject floor;
    [SerializeField]
    private GameObject endFloor;
    [SerializeField]
    private GameObject playerGO;
    [SerializeField]
    private GameObject box;

    [Header("InGame Variables")]
    [SerializeField]
    private int nbOfBoxToSpawn;
    [HideInInspector]
    public SaveData saveData;
    [HideInInspector]
    public GameObject player;
    int finisher = 0;

    [Header("UI")]
    [SerializeField]
    private GameObject winText;


    private void Awake()
    {
        //On initialise les floors comme positions autorisées
        for(int i = 0; i < floors.Count; i++)
        {
            deplacementPoints.Add(floors[i]);
        }
    }

    private void Start()
    {
        winText.SetActive(false);
        saveData = GetComponent<SaveData>();

        //Initialisation des murs
        for (int i = 0; i < walls.Count; i++)
        {
            GameObject instantiated = Instantiate(wall, walls[i].position, walls[i].rotation);
        }

        //Initialisation des sols
        for (int y = 0; y < floors.Count; y++)
        {
            GameObject instantiated = Instantiate(floor, floors[y].position, floors[y].rotation);
        }

        //Initialisation des sols où mettre les caisses
        for(int z = 0; z < winConditions.Count; z++)
        {
            GameObject instantiated = Instantiate(endFloor, winConditions[z].position, winConditions[z].rotation);
        }

        //Initialisation du joueur
        if(deplacementPoints.Count != 0)
        {
            int index = Random.Range(0, deplacementPoints.Count - 1);
            Transform rdmPos = deplacementPoints[index];
            player = Instantiate(playerGO, rdmPos.position, rdmPos.rotation);
            player.tag = "Player";
            UnAuthorizePoint(player.transform.position);
            saveData.objPlayer = player;
            saveData.posPlayer.Add(player.transform.position);
        }

        //Initialisation des Boîtes
        if(deplacementPoints.Count > nbOfBoxToSpawn)
        {
            for(int i = 0; i < nbOfBoxToSpawn; i++)
            {
                int index = Random.Range(0, deplacementPoints.Count - 1);
                while(deplacementPoints[index].tag !="Floor")
                {
                    index = Random.Range(0, deplacementPoints.Count - 1);
                }
                Transform rdmPos = deplacementPoints[index];
                GameObject boxGO = Instantiate(box, rdmPos.position, rdmPos.rotation);
                boxes.Add(boxGO);
                UnAuthorizePoint(boxGO.transform.position);
                boxGO.GetComponent<BoxData>().positions.Add(boxGO.transform.position);
            }
        }
    }

    public void CheckBox()
    {
        for(int i = 0; i < boxes.Count; i++)
        {
            for(int j = 0; j < winConditions.Count; j++)
            {
                if(boxes[i].transform.position == winConditions[j].transform.position && boxes[i].tag != "Finish")
                {
                    boxes[i].tag = "Finish";
                    finisher++;
                }
            }
            
        }

        if(finisher == nbOfBoxToSpawn)
        {
            player.GetComponent<PlayerController>().canMove = false;
            Time.timeScale = 0f;
            winText.SetActive(true);
        }
    
    }

    public void AuthorizePoint(Vector3 actualPos)
    {
        for(int i = 0; i < deplacementPoints.Count; i++)
        {
            if(deplacementPoints[i].position == actualPos)
            {
                deplacementPoints[i].tag = "Floor";
                return;
            }
        }
    }

    public void UnAuthorizePoint(Vector3 newPos)
    {
        for(int i = 0; i < deplacementPoints.Count; i++)
        {
            if(deplacementPoints[i].position == newPos)
            {   
                deplacementPoints[i].tag = "Wall";
                return;
            }
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
