using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockHold
{
    [Header("Düz kaynak")]
    [SerializeField] public BlockCode whoAmI;

    [SerializeField] public int amount;


    public BlockHold(BlockCode BlockCodee, int amountt)
    {
        whoAmI = BlockCodee;
        amount = amountt;

    }

}
