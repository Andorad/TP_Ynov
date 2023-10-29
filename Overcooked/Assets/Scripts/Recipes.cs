using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "Recipes")]
public class Recipes : ScriptableObject {
    public Texture visual;
    public ItemData[] items;
}
