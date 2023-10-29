using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    
    public List<Item> itemsInsidePlate;
    public List<GameObject> visuals;

    public void UpdateVisual(ItemData actualItem = null)
    {
        if(actualItem != null)
        {
             switch(actualItem.name)
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
}
