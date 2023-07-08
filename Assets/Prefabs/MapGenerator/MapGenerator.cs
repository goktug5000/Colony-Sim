using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] private GameObject[] grids;

    [SerializeField] private int xSize, zSize;
    [SerializeField] private float[,] noiseTry;
    [SerializeField] private float changeParameter;
    [SerializeField] private bool isIsland;

    [SerializeField] [Range(-200, 200)] private int randomLowest, randomHighest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(generateMapp(xSize, zSize));
            Debug.Log("Grids Generated");

        }
    }
    IEnumerator generateMapp(int x, int z)
    {
        float[,] noise = new float[x, z];
        float valueeChanger = 0;


        if (isIsland)
        {
            noise[0, 0] = -1;
        }
        else
        {
            float valuee = UnityEngine.Random.Range(randomLowest, randomHighest);
            noise[0, 0] = valuee;
            Debug.Log("Baþlangýç: " + valuee);
        }



        GenerateGrid(noise[0, 0], 0, 0);
        
        for (int zz = 1; zz < z; zz++)
        {

            if (isIsland)
            {
                noise[0, zz] = -1;
            }
            else
            {
                valueeChanger = UnityEngine.Random.Range(-1f, 1f);
                noise[0, zz] = noiseValueEdit(noise[0, zz - 1] + valueeChanger * changeParameter*2);
            }

            

            GenerateGrid(noise[0, zz], 0, zz);
        }
        
        for (int xx = 1; xx < x; xx++)
        {

            if (isIsland)
            {
                noise[xx, 0] = -1;
            }
            else
            {
                valueeChanger = UnityEngine.Random.Range(-1f, 1f);
                noise[xx, 0] = noiseValueEdit(noise[xx - 1, 0] + valueeChanger * changeParameter*2);
            }


            GenerateGrid(noise[xx, 0], xx, 0);
        }

        
        for (int xx = 1; xx < x; xx++)
        {
            for (int zz = 1; zz < z; zz++)
            {


                valueeChanger = UnityEngine.Random.Range(-1f, 1f);
                noise[xx, zz] = noiseValueEdit(((noise[xx - 1, zz] + noise[xx, zz - 1]) / 2) + valueeChanger * changeParameter);
                if (isIsland)
                {
                    if (xx == (x-1) || zz == (z-1))
                    {
                        noise[xx, zz] = -1;
                    }
                    
                }

                GenerateGrid(noise[xx, zz], xx, zz);
            }

        }



        noiseTry = noise;
        
        yield return null;
    }
    private float noiseValueEdit(float var)
    {
        if (var <= randomLowest)
        {
            var = randomLowest;
        }
        if (var >= randomHighest)
        {
            var = randomHighest;
        }
        return var;
    }
    void GenerateGrid(float noiseFloatt, int xx, int zz)
    {

        var gridd = Instantiate(GetGrid(noiseFloatt));

        gridd.transform.parent = this.gameObject.transform;
        int myHeigh = (Convert.ToInt32(noiseFloatt)* 3);
        gridd.gameObject.name = ((xx).ToString() + "-" + (zz).ToString() + "*" + myHeigh / 3 + "_" + gridd.gameObject.name);
        if (myHeigh < 0) myHeigh = 0;
        gridd.transform.position = new Vector3(xx * 5, myHeigh, zz * 5);

        //katman kayasý koyuyorum
        var gridBedRock = Instantiate(getUnderGrid(-31, false));
        gridBedRock.transform.parent = this.gameObject.transform;
        gridBedRock.gameObject.name = ((xx).ToString() + "-" + (zz).ToString() + "*" + (randomLowest - 1) + "_" + "BedRock");
        gridBedRock.transform.position = new Vector3(xx * 5, (randomLowest-1)*3, zz * 5);
        //üstüne 1 sýta normal malzeme koyuyorum
        var gridRock = Instantiate(getUnderGrid(1, false));
        gridRock.transform.parent = this.gameObject.transform;
        gridRock.gameObject.name = ((xx).ToString() + "-" + (zz).ToString() + "*" + (randomLowest - 1) + "_" + gridRock.gameObject.name);
        gridRock.transform.position = new Vector3(xx * 5, randomLowest * 3, zz * 5);

        //katman kayasýndan buraya kadar block koyuyorum
        for (int q = randomLowest+1; q < (myHeigh/3); q++)
        {
            bool buZeminSuMu=false;
            if (noiseFloatt < 0)
            {
                buZeminSuMu = true;
            }
            var griddd = Instantiate(getUnderGrid(0, buZeminSuMu));

            griddd.transform.parent = this.gameObject.transform;
            griddd.gameObject.name = ((xx).ToString() + "-" + (zz).ToString() + "*" + q + "_" + gridd.gameObject.name);
            griddd.transform.position = new Vector3(xx * 5, q*3, zz * 5);
        }
        
        //Debug.Log(gridd.transform.position + gridd.name);



    }
    private GameObject GetGrid(float noiseFloat)
    {

        if (noiseFloat < 0)
        {
            return grids[0];
        }
        else if (noiseFloat >= 0 && noiseFloat < 4)
        {
            return grids[1];
        }
        else if (noiseFloat >= 4 && noiseFloat < 130)
        {
            return grids[2];
        }
        else if (noiseFloat >= 130 && noiseFloat <= 160)
        {
            return grids[3];
        }
        return null;
    }
    private GameObject getUnderGrid(float myHeigh,bool suMu)
    {
        if (suMu)
        {
            return grids[0];
        }
        else if(myHeigh==-31)  
        {
            return grids[4];
        }
        else
        {
            return grids[2];

        }
        return null;
    }
}
