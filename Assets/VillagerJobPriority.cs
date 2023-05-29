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
                float distance = Vector3.Distance(this.transform.position, blockHolders[i].transform.position);
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
                float distance = Vector3.Distance(this.transform.position, TreeCodes[i].transform.position);
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

            float distance = Vector3.Distance(this.transform.position, stockPileCodes[i].gameObject.transform.position);
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

            float distance = Vector3.Distance(this.transform.position, farmPileCodes[i].gameObject.transform.position);
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
    public GameObject lookForSeeds(mySeeds.SeedTypesEnum[] SeedTypesEnumToPlant)
    {
        BlockHolder[] blockHolders = FindObjectsOfType<BlockHolder>();
        GameObject enYakinObj = null;

        float distanceMin = Mathf.Infinity;
        for(int seedDongusu = 0; seedDongusu < SeedTypesEnumToPlant.Length; seedDongusu++)
        {
            for (int i = 0; i < blockHolders.Length; i++)
            {
                if (blockHolders[i].onHaul == false && blockHolders[i].someoneOnIt == false && blockHolders[i].whoIsHold.whoAmSeed.mySeedTypesEnum == SeedTypesEnumToPlant[seedDongusu])
                {
                    float distance = Vector3.Distance(this.transform.position, blockHolders[i].transform.position);
                    //Debug.Log("Distance between " + this.name + " and " + blockHolders[i].name + " is " + distance + " units.");
                    if (distance < distanceMin)
                    {
                        distanceMin = distance;
                        enYakinObj = blockHolders[i].gameObject;
                    }
                }

            }
        }
        
        if (enYakinObj != null)
        {
            StartCoroutine(haulJob(enYakinObj));
        }
        return enYakinObj;
    }


    IEnumerator haulJob(GameObject haulingObj)
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
            yield break;
        }
        //Debug.Log("haul started");
        myVillagerMove.moveToPoint(haulingObj.transform.position);//obje ye git

        while (!myVillagerMove.anyPathRemaining())//gitmesini bekle
        {
            
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
            yield break;
        }
        //Debug.Log("in position");



        yield return new WaitForSeconds(1.0f);//1sn bekle

        haulingObj.GetComponent<BlockHolder>().onHaul = true;//objeyi taþýnýyo haline getirdi
        myVillagerMove.haulThis(haulingObj,true);//objeyi taþýmaya baþladý


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
            yield break;
        }
        Debug.Log("in pile");
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
