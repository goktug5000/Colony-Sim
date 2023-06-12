using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TreeCode : MonoBehaviour
{
    [SerializeField] public BlockHold[] myBlocks;
    public float HP;
    public float HPmax;
    public bool someoneOnIt;

    [SerializeField] private TextMeshPro myHPShower;
    private bool thisShowHPisWorking;
    private void Start()
    {
        myHPShower.gameObject.SetActive(false);
        if (HPmax == 0)
        {
            HPmax = HP;
        }

    }
    public void takeDMG(float dmg)
    {


        HP -= dmg;
        myHPShower.text = (HP + "/" + HPmax);
        if (HP <= 0)
        {
            StartCoroutine(aboutDestroy());
            Destroy(this.gameObject);
        }
        if (thisShowHPisWorking == false)
        {
            StartCoroutine(showHP());
        }

    }

    IEnumerator showHP()
    {

        thisShowHPisWorking = true;
        myHPShower.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        myHPShower.gameObject.SetActive(false);
        thisShowHPisWorking = false;
    }
    IEnumerator aboutDestroy()
    {
        int total = 0;
        foreach(BlockHold myblock in myBlocks)
        {
            for(int i = 0; i < myblock.amount; i++)
            {
                
                Instantiate(myblock.whoAmI.myGameObj, new Vector3(this.transform.position.x, this.transform.position.y + total*(0.31f), this.transform.position.z), Quaternion.identity);
                total++;
            }
        }
        yield return new WaitForSeconds(0.1f);
    }
}
