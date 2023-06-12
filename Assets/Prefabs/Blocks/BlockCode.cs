using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "MyObjs/Blocks")]
[System.Serializable]
public class BlockCode : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public string description;


    [SerializeField] public Sprite artWork;
    [SerializeField] public GameObject myGameObj;
    [SerializeField] private MaterialTypesEnum myMaterialTypesEnum;

    [SerializeField] public float kg;
    [SerializeField] public int maxAmount;
    private enum MaterialTypesEnum
    {
        
        Food,
        RawMaterial,
        ManuFactured,

    }
}