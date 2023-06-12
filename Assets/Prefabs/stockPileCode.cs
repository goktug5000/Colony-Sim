using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class stockPileCode : MonoBehaviour
{
    [SerializeField] public BlockHold[] myBlocks;
    [SerializeField] public int sizeX,sizeZ;

    [SerializeField] public GameObject myObjsParent;
    [SerializeField] public GameObject[] myObjs;
    void Start()
    {
        myBlocks = new BlockHold[(sizeX * 3) * (sizeZ * 3) * 3];
        myObjs = new GameObject[(sizeX * 3) * (sizeZ * 3) * 3];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBlock(BlockHolder BlockHolderr)
    {
        foreach(GameObject myObj in myObjs)
        {
            if (myObj != null)
            {
                try
                {
                    BlockHolder myObjBlock = myObj.GetComponent<BlockHolder>();
                    if (BlockHolderr.whoIsHold.whoAmI.name == myObjBlock.whoIsHold.whoAmI.name)
                    {
                        int leftAmount = myObjBlock.whoIsHold.whoAmI.maxAmount - myObjBlock.whoIsHold.amount;
                        if (leftAmount != 0)
                        {
                            if (leftAmount >= BlockHolderr.whoIsHold.amount)
                            {
                                myObjBlock.whoIsHold.amount += BlockHolderr.whoIsHold.amount;
                                Destroy(BlockHolderr.gameObject);
                                return;
                            }
                            else
                            {
                                myObjBlock.whoIsHold.amount = myObjBlock.whoIsHold.whoAmI.maxAmount;
                                BlockHolderr.whoIsHold.amount -= leftAmount;
                                addBlock(BlockHolderr);
                                return;
                            }
                        }
                        

                    }
                }
                catch
                {

                }
                
            }
            
        }
        if (BlockHolderr.whoIsHold.amount > 0)
        {
            addToGameObj(BlockHolderr.gameObject);
        }
        
        return;
        /*
        foreach (BlockHold myBlock in myBlocks)
        {
            if (myBlock != null)
            {
                
                if (myBlock.whoAmI == BlockHolderr.whoIsHold.whoAmI)
                {
                    myBlock.amount += BlockHolderr.whoIsHold.amount;
                    return;
                }
            }

        }

        for (int i = 0; i < myBlocks.Length; i++)
        {
            if (myBlocks[i].whoAmI == null)
            {

                myBlocks[i] = new BlockHold(BlockHolderr.whoIsHold.whoAmI, BlockHolderr.whoIsHold.amount);
                return;
            }
        }
        */
    }
    public void addToGameObj(GameObject GameObjectt)
    {
        for (int i = 0; i < myObjs.Length; i++)
        {
            if (myObjs[i] == null)
            {
                myObjs[i] = GameObjectt;

                //Debug.Log((((i % (sizeX * 3)) - 1))+"  " + ((((i % ((sizeX * 3) * (sizeZ * 3))) / (sizeZ * 3)) - 1)));
                GameObjectt.transform.SetParent(myObjsParent.transform);
                GameObjectt.transform.localPosition = new Vector3(0.31f * ((i%(sizeX*3))-1), 1f, 0.31f* ((((i % ((sizeX * 3) * (sizeZ * 3)) )/ (sizeZ*3))-1)));
                return;
            }
        }
    }
}
