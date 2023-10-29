using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public List<GameObject> gameObjectsInRange;

    public List<GameObject> cuttingBoards;

    public List<GameObject> hotPlates;
    public List<GameObject> plateGiver;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Item"))
        {
            gameObjectsInRange.Add(other.gameObject);
        }
        else if(other.CompareTag("CuttingBoard"))
        {
            cuttingBoards.Add(other.gameObject);
        }
        else if(other.CompareTag("HotPlate"))
        {
            hotPlates.Add(other.gameObject);
        }
        else if(other.CompareTag("PlateGiver"))
        {
            plateGiver.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Item"))
        {
            gameObjectsInRange.Remove(other.gameObject);
        }
        else if(other.CompareTag("CuttingBoard"))
        {
            cuttingBoards.Remove(other.gameObject);
        }
        else if(other.CompareTag("HotPlate"))
        {
            hotPlates.Remove(other.gameObject);
        }
        else if(other.CompareTag("PlateGiver"))
        {
            plateGiver.Remove(other.gameObject);
        }
    }
}
