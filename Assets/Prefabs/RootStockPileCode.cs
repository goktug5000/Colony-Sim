using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[System.Serializable]
public class RootStockPileCode : MonoBehaviour
{

    [SerializeField] private BlockHold[] allBlocks = new BlockHold[0];
    [SerializeField] private BlockHold allBloc;
    [SerializeField] private TextMeshProUGUI depomuz;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.Q))
        {
            forcedRefindAllStockPileOwns();
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
                        bool foreachBool = false;
                        foreach(BlockHold allBlock in allBlocks)
                        {
                            if (allBlocks != null)
                            {
                                try
                                {

                                    if (myObj.GetComponent<BlockHolder>().whoIsHold.whoAmI.name == allBlock.whoAmI.name)
                                    {
                                        allBlock.amount += myObj.GetComponent<BlockHolder>().whoIsHold.amount;
                                        foreachBool = true;
                                        break;
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
                        if (!foreachBool)
                        {
                            reSizeAndAddNew(myObj);
                        }
                        
                        
                    }


                }
            }
            
        }
        
        string dildozer = "";
        foreach(BlockHold allBlock in allBlocks)
        {
            if (allBlock != null)
            {

                dildozer+=(allBlock.whoAmI.name + ": " + allBlock.amount);
                dildozer += "\n";
            }
        }
        //Debug.Log(dildozer);
        depomuz.text = dildozer;
        
    }
    void reSizeAndAddNew(GameObject objToAdd)
    {
        Array.Resize(ref allBlocks, allBlocks.Length + 1);
        allBlocks[allBlocks.Length - 1] = new BlockHold(objToAdd.GetComponent<BlockHolder>().whoIsHold.whoAmI, objToAdd.GetComponent<BlockHolder>().whoIsHold.amount);

    }

}
