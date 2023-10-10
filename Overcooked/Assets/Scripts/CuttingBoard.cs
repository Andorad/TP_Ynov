using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public List<GameObject> visuals;
    public ItemData _actualItem;

    public void UpdateVisual(ItemData actualItem = null)
    {
        for(int i = 0; i < visuals.Count; i++)
        {
            visuals[i].SetActive(false);
        }

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
