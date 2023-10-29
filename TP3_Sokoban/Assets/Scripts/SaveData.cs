using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{   
    [HideInInspector]
    public GameObject objPlayer;
    [HideInInspector]
    public List<Vector3> posPlayer;

    

    private GridCreator grid;

    private void Start()
    {
        grid = GetComponent<GridCreator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            RollBack();
        }
    }

    private void RollBack()
    {
        if(posPlayer.Count > 0)
        {
            //On Rollback le joueur
            grid.AuthorizePoint(grid.player.transform.position);
            grid.player.transform.position = posPlayer[posPlayer.Count - 1];
            grid.UnAuthorizePoint(posPlayer[posPlayer.Count - 1]);
            posPlayer.RemoveAt(posPlayer.Count - 1);

            for(int i = 0; i < grid.boxes.Count; i++)
            {
                //On rollback toute les caisses
                grid.AuthorizePoint(grid.boxes[i].transform.position);
                grid.boxes[i].transform.position = grid.boxes[i].GetComponent<BoxData>().positions[grid.boxes[i].GetComponent<BoxData>().positions.Count - 1];
                grid.UnAuthorizePoint(grid.boxes[i].GetComponent<BoxData>().positions[grid.boxes[i].GetComponent<BoxData>().positions.Count - 1]);
                grid.boxes[i].GetComponent<BoxData>().positions.RemoveAt(grid.boxes[i].GetComponent<BoxData>().positions.Count - 1);
            }
        }
    }
}

