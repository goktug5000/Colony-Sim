using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class RootStockPileCode : MonoBehaviour
{

    [SerializeField] BlockHold[] allBlocks = new BlockHold[0];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            forcedRefindAllStockPileOwns();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            allBlocks = new BlockHold[0];
        }
    }
    public void forcedRefindAllStockPileOwns()
    {
        stockPileCode[] stockPileCodes = FindObjectsOfType<stockPileCode>();
        allBlocks = new BlockHold[0];
        foreach (stockPileCode stockPileCode in stockPileCodes)
        {
            if (stockPileCode != null)
            {
                foreach (GameObject myObj in stockPileCode.myObjs)
                {
                    if (myObj != null)
                    {
                        foreach(BlockHold allBlock in allBlocks)
                        {
                            if (allBlocks != null)
                            {
                                try
                                {

                                    if (myObj.GetComponent<BlockHolder>().whoIsHold.whoAmI.name == allBlock.whoAmI.name)
                                    {
                                        allBlock.amount += myObj.GetComponent<BlockHolder>().whoIsHold.amount;
                                        return;
                                    }
                                    else
                                    {

                                    }
                                }
                                catch
                                {

                                }
                            }
                            
                        }

                        reSizeAndAddNew(myObj);
                        


                    }
                }
            }
            
        }


        foreach(BlockHold allBlock in allBlocks)
        {
            if (allBlock != null)
            {
                Debug.Log(allBlock.whoAmI.name + ": " + allBlock.amount);

            }
        }
    }
    void reSizeAndAddNew(GameObject objToAdd)
    {
        try
        {
            Array.Resize(ref allBlocks, allBlocks.Length + 1);
            allBlocks[allBlocks.Length - 1] = new BlockHold(objToAdd.GetComponent<BlockHold>().whoAmI, objToAdd.GetComponent<BlockHold>().amount);
        }
        catch
        {

        }
        
    }
}
