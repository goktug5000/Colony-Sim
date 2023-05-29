using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed", menuName = "MyObjs/Seeds")]
[System.Serializable]
public class mySeeds : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public string description;


    [SerializeField] public Sprite artWork;
    [SerializeField] public GameObject myGameObj;

    [SerializeField] public SeedTypesEnum mySeedTypesEnum;

    [SerializeField] public float growtTime;

    public enum SeedTypesEnum
    {

        Berry
    }
}