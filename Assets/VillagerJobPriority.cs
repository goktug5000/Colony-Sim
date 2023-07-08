using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VillagerJobPriority : MonoBehaviour
{

    [SerializeField] private VillagerMove myVillagerMove;
    [SerializeField] private GameObject chosenSym;
    [SerializeField] private bool amIChosen;
    [SerializeField] private bool onDuty;
    [SerializeField] private bool godLevelOrder;

    public LayerMask layerMask;

    [Header("Mesleki Puanlar")]
    [SerializeField] private allJobs[] myJobs;
    private enum allJobs
    {
        Cooker,
        Hunter,
        Grower,
        plantCuter,
        lumberJack,
        Hauler
    }

    [SerializeField] private float lumberJackCD;
    [SerializeField] private float lumberJackHit;



    void Start()
    {
        if (myVillagerMove == null)
        {
            myVillagerMove = this.gameObject.GetComponent<VillagerMove>();
        }
    }

    void Update()
    {
        chosenSym.SetActive(amIChosen);
        chooseMe();
        orderMeIfImChoosen();
        regularJob();

    }
    public void orderMeIfImChoosen()
    {
        if (!amIChosen)
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            godLevelOrder = true;
            StartCoroutine(walkJob());
            //godLevelOrder = false;

        }

    }
    public void regularJob()//tanrý baþka görev vermiyorsa bunlarý yapýyo
    {
        if (onDuty || godLevelOrder)
        {
            return;
        }
        if (lookForTree()!=null)
        {
            return;
        }
        if (lookForHaul() != null)
        {
            return;
        }
        if (lookForHaul() != null)
        {
            return;
        }

    }
    public void chooseMe()
    {
        if (Input.GetMouseButtonDown(0))//köylü seçmek için
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMask))
            {
                Debug.Log(raycastHit.transform.gameObject);
                try
                {
                    if (raycastHit.transform.gameObject != this.gameObject)
                    {
                        amIChosen = false;
                        godLevelOrder = false;
                    }
                    else
                    {
                        amIChosen = true;
                        
                    }

                }
                catch
                {
                    Debug.LogError("Chosen scripti yok");
                }

            }
            else
            {
                amIChosen = false;
                godLevelOrder = false;
            }

        }



    }
    public GameObject lookForHaul()
    {

        BlockHolder[] blockHolders = FindObjectsOfType<BlockHolder>();
        GameObject enYakinObj = null;

        float distanceMin = Mathf.Infinity;
        for (int i = 0; i < blockHolders.Length; i++)
        {
            if (blockHolders[i].inPile == false && blockHolders[i].onHaul == false && blockHolders[i].someoneOnIt == false)
            {
                float distance = myVillagerMove.CalculatePathDistance(blockHolders[i].transform.position);
                //Debug.Log("Distance between " + this.name + " and " + blockHolders[i].name + " is " + distance + " units.");
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    enYakinObj = blockHolders[i].gameObject;
                }
            }



        }
        if (enYakinObj != null)
        {
            StartCoroutine(haulJob(enYakinObj));
        }
        return enYakinObj;

    }
    public GameObject lookForTree()
    {
        
        TreeCode[] TreeCodes = FindObjectsOfType<TreeCode>();
        GameObject enYakinObj=null;

        float distanceMin = Mathf.Infinity;
        for (int i = 0; i < TreeCodes.Length; i++)
        {
            if (TreeCodes[i].someoneOnIt == false)
            {
                float distance = myVillagerMove.CalculatePathDistance(TreeCodes[i].transform.position);
                //Debug.Log("Distance between " + this.name + " and " + blockHolders[i].name + " is " + distance + " units.");
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    enYakinObj = TreeCodes[i].gameObject;
                }
            }


            
        }
        if (enYakinObj != null)
        {
            StartCoroutine(lumberJackJob(enYakinObj));
        }
        return enYakinObj;

    }
    public GameObject lookForStockPile()
    {
        stockPileCode[] stockPileCodes = FindObjectsOfType<stockPileCode>();
        GameObject enYakinObj = null;

        float distanceMin = Mathf.Infinity;
        for (int i = 0; i < stockPileCodes.Length; i++)
        {
            
            float distance = myVillagerMove.CalculatePathDistance(stockPileCodes[i].gameObject.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                enYakinObj = stockPileCodes[i].gameObject;
            }
            //Debug.Log("Distance between " + this.name + " and " + stockPileCodes[i].name + " is " + distance + " units.");

        }

        return enYakinObj;

    }
    public GameObject lookForFarmPile()
    {
        FarmPile[] farmPileCodes = FindObjectsOfType<FarmPile>();
        GameObject enYakinObj = null;

        float distanceMin = Mathf.Infinity;
        for (int i = 0; i < farmPileCodes.Length; i++)
        {
            float distance = myVillagerMove.CalculatePathDistance(farmPileCodes[i].gameObject.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                enYakinObj = farmPileCodes[i].gameObject;
            }
            //Debug.Log("Distance between " + this.name + " and " + stockPileCodes[i].name + " is " + distance + " units.");

        }
        if (enYakinObj != null)
        {

        }
        return enYakinObj;

    }

    IEnumerator haulJob(GameObject haulingObj)
    {
        StartCoroutine(haulJob(haulingObj, null));
        yield break;
    }

    IEnumerator haulJob(GameObject haulingObj,GameObject preHaulingObj)
    {
        haulingObj.GetComponent<BlockHolder>().someoneOnIt = true;

        onDuty = true;
        if (godLevelOrder)
        {
            Debug.Log("god stops");
            haulingObj.GetComponent<BlockHolder>().inPile = false;
            haulingObj.GetComponent<BlockHolder>().onHaul = false;
            haulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
            myVillagerMove.haulThis(haulingObj, false);//objeyi taþýmayý býraktý
            if (preHaulingObj != null)
            {
                preHaulingObj.GetComponent<BlockHolder>().inPile = false;
                preHaulingObj.GetComponent<BlockHolder>().onHaul = false;
                preHaulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
                myVillagerMove.haulThis(preHaulingObj, false);//objeyi taþýmayý býraktý
            }

            yield break;
        }
        //Debug.Log("haul started");
        myVillagerMove.moveToPoint(haulingObj.transform.position);//obje ye git

        while (!myVillagerMove.anyPathRemaining())//gitmesini bekle
        {
            if (godLevelOrder)
            {
                Debug.Log("god stops");
                haulingObj.GetComponent<BlockHolder>().inPile = false;
                haulingObj.GetComponent<BlockHolder>().onHaul = false;
                haulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
                myVillagerMove.haulThis(haulingObj, false);//objeyi taþýmayý býraktý
                if (preHaulingObj != null)
                {
                    preHaulingObj.GetComponent<BlockHolder>().inPile = false;
                    preHaulingObj.GetComponent<BlockHolder>().onHaul = false;
                    preHaulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
                    myVillagerMove.haulThis(preHaulingObj, false);//objeyi taþýmayý býraktý
                }

                yield break;
            }
            yield return null;
            //((Vector3.Distance(this.transform.position, haulingObj.transform.position))<0.7f)
        }
        if (godLevelOrder)
        {
            Debug.Log("god stops");
            haulingObj.GetComponent<BlockHolder>().inPile = false;
            haulingObj.GetComponent<BlockHolder>().onHaul = false;
            haulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
            myVillagerMove.haulThis(haulingObj, false);//objeyi taþýmayý býraktý
            preHaulingObj.GetComponent<BlockHolder>().inPile = false;
            preHaulingObj.GetComponent<BlockHolder>().onHaul = false;
            preHaulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
            myVillagerMove.haulThis(preHaulingObj, false);//objeyi taþýmayý býraktý
            yield break;
        }
        //Debug.Log("in position");
        yield return new WaitForSeconds(1.0f);//1sn bekle


        //2023_06_12
        if (preHaulingObj != null)
        {
            int leftAmount = preHaulingObj.GetComponent<BlockHolder>().whoIsHold.whoAmI.maxAmount-preHaulingObj.GetComponent<BlockHolder>().whoIsHold.amount;
            
            if(leftAmount >= haulingObj.GetComponent<BlockHolder>().whoIsHold.amount)
            {
                Destroy(preHaulingObj);
                haulingObj.GetComponent<BlockHolder>().whoIsHold.amount += preHaulingObj.GetComponent<BlockHolder>().whoIsHold.amount;
            }
            else
            {
                preHaulingObj.GetComponent<BlockHolder>().whoIsHold.amount = preHaulingObj.GetComponent<BlockHolder>().whoIsHold.whoAmI.maxAmount;
                haulingObj.GetComponent<BlockHolder>().whoIsHold.amount -= leftAmount;
                haulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
                haulingObj = preHaulingObj;
                
            }
            
        }
        //!2023_06_12

        haulingObj.GetComponent<BlockHolder>().onHaul = true;//objeyi taþýnýyo haline getirdi
        myVillagerMove.haulThis(haulingObj, true);//objeyi taþýmaya baþladý




        //2023_06_12

        //aldýðýmdan baþka var mý diye kontrol et
        int leftAmountt = haulingObj.GetComponent<BlockHolder>().whoIsHold.whoAmI.maxAmount - haulingObj.GetComponent<BlockHolder>().whoIsHold.amount;
        if (leftAmountt > 0)
        {
            
            float lookingRadius = 3;
            Collider[] colliders = Physics.OverlapSphere(haulingObj.transform.position, lookingRadius);
            foreach (Collider collider in colliders)
            {

                
                BlockHolder blockHolder = collider.gameObject.GetComponent<BlockHolder>();

                if (blockHolder != null)
                {
                    
                    if (blockHolder.whoIsHold.whoAmI.name == haulingObj.GetComponent<BlockHolder>().whoIsHold.whoAmI.name)
                    {
                        if (!blockHolder.onHaul && !blockHolder.someoneOnIt && !blockHolder.inPile)
                        {
                            StartCoroutine(haulJob(blockHolder.gameObject, haulingObj));
                            yield break;
                        }
                    }

                }

            }
        }
        //!2023_06_12





        GameObject myPile = lookForStockPile();
        myVillagerMove.moveToPoint(myPile.transform.position);//depoya git
        while (!myVillagerMove.anyPathRemaining())//gitmesini bekle
        {
            yield return null;
        }
        if (godLevelOrder)
        {
            Debug.Log("god stops");
            haulingObj.GetComponent<BlockHolder>().inPile = false;
            haulingObj.GetComponent<BlockHolder>().onHaul = false;
            haulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
            myVillagerMove.haulThis(haulingObj, false);//objeyi taþýmayý býraktý
            if (preHaulingObj != null)
            {
                preHaulingObj.GetComponent<BlockHolder>().inPile = false;
                preHaulingObj.GetComponent<BlockHolder>().onHaul = false;
                preHaulingObj.GetComponent<BlockHolder>().someoneOnIt = false;
                myVillagerMove.haulThis(preHaulingObj, false);//objeyi taþýmayý býraktý
            }

            yield break;
        }
        //Debug.Log("in pile");
        myVillagerMove.haulThis(haulingObj, false);//objeyi taþýmayý býraktý

        myPile.GetComponent<stockPileCode>().addBlock(haulingObj.GetComponent<BlockHolder>());

        haulingObj.GetComponent<BlockHolder>().inPile = true;
        haulingObj.GetComponent<BlockHolder>().onHaul = false;
        haulingObj.GetComponent<BlockHolder>().someoneOnIt = false;


        onDuty = false;
    }




    IEnumerator lumberJackJob(GameObject cuttingTreeObj)
    {

        cuttingTreeObj.GetComponent<TreeCode>().someoneOnIt = true;
        onDuty = true;
        if (godLevelOrder)
        {
            Debug.Log("god stops");
            cuttingTreeObj.GetComponent<TreeCode>().someoneOnIt = false;
            yield break;
        }
        //Debug.Log("haul started");
        myVillagerMove.moveToPoint(cuttingTreeObj.transform.position);//obje ye git

        while (!myVillagerMove.anyPathRemaining())//gitmesini bekle
        {

            yield return null;
            //((Vector3.Distance(this.transform.position, haulingObj.transform.position))<0.7f)
        }
        if (godLevelOrder)
        {
            Debug.Log("god stops");
            cuttingTreeObj.GetComponent<TreeCode>().someoneOnIt = false;
            yield break;
        }
        //Debug.Log("in position");



        yield return new WaitForSeconds(1.0f);//1sn bekle

        while (cuttingTreeObj.GetComponent<TreeCode>().HP>0)//Aðacý kesmesini bekle
        {
            if (godLevelOrder)
            {
                Debug.Log("god stops");
                cuttingTreeObj.GetComponent<TreeCode>().someoneOnIt = false;
                yield break;
            }
            yield return new WaitForSeconds(lumberJackCD);
            cuttingTreeObj.GetComponent<TreeCode>().takeDMG(lumberJackHit);
        }



       
        if (godLevelOrder)
        {
            Debug.Log("god stops");
            yield break;
        }
        Debug.Log("in pile");



        onDuty = false;

    }
    IEnumerator walkJob()
    {
        onDuty = true;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            myVillagerMove.newCommand();
            myVillagerMove.moveToPoint(hit.point);

        }

        while (!myVillagerMove.anyPathRemaining())//gitmesini bekle
        {
            yield return null;
        }
        onDuty = false;
    }

    
    
}
