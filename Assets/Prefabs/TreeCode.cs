using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeCode : MonoBehaviour
{
    [SerializeField] public BlockHold[] myBlocks;
    public float HP;
    public bool someoneOnIt;


    public void takeDMG(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            StartCoroutine(aboutDestroy());
            Destroy(this.gameObject);
        }
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
