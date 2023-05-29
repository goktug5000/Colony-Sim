using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FarmPile : MonoBehaviour
{
    [SerializeField] public BlockHold mySeed;
    [SerializeField] public bool cultivated;

    [SerializeField] public float growtMeter;
    private void Update()
    {
        //growtMeter -= Time.deltaTime;
    }
    public bool ekBeni(BlockHolder ekBunuuu)
    {
        mySeed = ekBunuuu.whoIsHold;
        //growtMeter = mySeed.whoAmI.growtTime;
        cultivated = true;
        return false;
    }
}
