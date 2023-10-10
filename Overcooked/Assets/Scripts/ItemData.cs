using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public string name;
    public ItemType itemType;
    public GameObject prefab;
    public Sprite visual;
}