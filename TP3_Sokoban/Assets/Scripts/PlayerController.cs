using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GridCreator gridCreator;
    [HideInInspector]
    public bool canMove;

    private void Start()
    {
        gridCreator = GameObject.FindGameObjectWithTag("GridCreator").GetComponent<GridCreator>();
        canMove = true;
    }


    void Update()
    {
        if(canMove)
        {
            //Si on veut aller à droite
            if(Input.GetKeyDown(KeyCode.D))
            {
                for(int i = 0;  i < gridCreator.deplacementPoints.Count; i++)
                {
                    //Si on peut aller à droite
                    if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x + 1, transform.position.y, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                    {
                        gridCreator.AuthorizePoint(transform.position);
                        transform.position = gridCreator.deplacementPoints[i].position;
                        gridCreator.UnAuthorizePoint(transform.position);
                        gridCreator.saveData.posPlayer.Add(transform.position);

                        for(int z = 0; z < gridCreator.boxes.Count; z++)
                        {
                            gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                        }
                        return;
                    }
                    //Si il y a une boîte devant nous et qu'on peut la pousser
                    for(int j = 0; j < gridCreator.boxes.Count; j++)
                    {
                        if(gridCreator.boxes[j].transform.position == new Vector3(transform.position.x + 1, transform.position.y, transform.position.z))
                        {
                            if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x + 2, transform.position.y, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                            {
                                gridCreator.AuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.boxes[j].transform.position = gridCreator.deplacementPoints[i].position;
                                gridCreator.UnAuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.CheckBox();
                                gridCreator.AuthorizePoint(transform.position);
                                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                                gridCreator.UnAuthorizePoint(transform.position);
                                gridCreator.saveData.posPlayer.Add(transform.position);

                                for(int z = 0; z < gridCreator.boxes.Count; z++)
                                {
                                    gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                                }
                                return;
                            }
                        }
                    }
                }
            }
            
            if(Input.GetKeyDown(KeyCode.Q))
            {
                for(int i = 0;  i < gridCreator.deplacementPoints.Count; i++)
                {
                    if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x - 1, transform.position.y, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                    {
                        gridCreator.AuthorizePoint(transform.position);
                        transform.position = gridCreator.deplacementPoints[i].position;
                        gridCreator.UnAuthorizePoint(transform.position);
                        gridCreator.saveData.posPlayer.Add(transform.position);
                        
                        for(int z = 0; z < gridCreator.boxes.Count; z++)
                        {
                            gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                        }
                        return;
                    }

                    for(int j = 0; j < gridCreator.boxes.Count; j++)
                    {
                        if(gridCreator.boxes[j].transform.position == new Vector3(transform.position.x - 1, transform.position.y, transform.position.z))
                        {
                            if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x - 2, transform.position.y, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                            {
                                gridCreator.AuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.boxes[j].transform.position = gridCreator.deplacementPoints[i].position;
                                gridCreator.UnAuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.CheckBox();
                                gridCreator.AuthorizePoint(transform.position);
                                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                                gridCreator.UnAuthorizePoint(transform.position);
                                gridCreator.saveData.posPlayer.Add(transform.position);
                                for(int z = 0; z < gridCreator.boxes.Count; z++)
                                {
                                    gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                                }
                                return;
                            }
                        }
                    }
                }
            }
            
            if(Input.GetKeyDown(KeyCode.S))
            {
                for(int i = 0;  i < gridCreator.deplacementPoints.Count; i++)
                {
                    if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x, transform.position.y - 1, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                    {
                        gridCreator.AuthorizePoint(transform.position);
                        transform.position = gridCreator.deplacementPoints[i].position;
                        gridCreator.UnAuthorizePoint(transform.position);
                        gridCreator.saveData.posPlayer.Add(transform.position);

                        for(int z = 0; z < gridCreator.boxes.Count; z++)
                        {
                            gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                        }
                        return;
                    }

                    for(int j = 0; j < gridCreator.boxes.Count; j++)
                    {
                        if(gridCreator.boxes[j].transform.position == new Vector3(transform.position.x, transform.position.y - 1, transform.position.z))
                        {
                            if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x, transform.position.y - 2, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                            {
                                gridCreator.AuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.boxes[j].transform.position = gridCreator.deplacementPoints[i].position;
                                gridCreator.UnAuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.CheckBox();
                                gridCreator.AuthorizePoint(transform.position);
                                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                                gridCreator.UnAuthorizePoint(transform.position);
                                gridCreator.saveData.posPlayer.Add(transform.position);

                                for(int z = 0; z < gridCreator.boxes.Count; z++)
                                {
                                    gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                                }
                                return;
                            }
                        }
                    }
                }
            }
            
            if(Input.GetKeyDown(KeyCode.Z))
            {
                for(int i = 0;  i < gridCreator.deplacementPoints.Count; i++)
                {
                    if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                    {
                        gridCreator.AuthorizePoint(transform.position);
                        transform.position = gridCreator.deplacementPoints[i].position;
                        gridCreator.UnAuthorizePoint(transform.position);
                        gridCreator.saveData.posPlayer.Add(transform.position);

                        for(int z = 0; z < gridCreator.boxes.Count; z++)
                        {
                            gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                        }
                        return;
                    }

                    for(int j = 0; j < gridCreator.boxes.Count; j++)
                    {
                        if(gridCreator.boxes[j].transform.position == new Vector3(transform.position.x, transform.position.y + 1, transform.position.z))
                        {
                            if(gridCreator.deplacementPoints[i].position == new Vector3(transform.position.x, transform.position.y + 2, transform.position.z) && gridCreator.deplacementPoints[i].tag == "Floor")
                            {
                                gridCreator.AuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.boxes[j].transform.position = gridCreator.deplacementPoints[i].position;
                                gridCreator.UnAuthorizePoint(gridCreator.boxes[j].transform.position);
                                gridCreator.CheckBox();
                                gridCreator.AuthorizePoint(transform.position);
                                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                                gridCreator.UnAuthorizePoint(transform.position);
                                gridCreator.saveData.posPlayer.Add(transform.position);

                                for(int z = 0; z < gridCreator.boxes.Count; z++)
                                {
                                    gridCreator.boxes[z].GetComponent<BoxData>().positions.Add(gridCreator.boxes[z].transform.position);
                                }
                                return;
                            }
                        }
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                gridCreator.Reset();
            }
        }
    }


}
