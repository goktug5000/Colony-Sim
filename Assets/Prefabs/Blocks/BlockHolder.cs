using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockHolder : MonoBehaviour
{
    [SerializeField] public BlockHold whoIsHold;
    public bool inPile;
    public bool onHaul;

    public bool someoneOnIt;

    
}
