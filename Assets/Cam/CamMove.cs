using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CamMove : MonoBehaviour
{

    [SerializeField] private float CamMoveSpeed;
    [SerializeField] private float CamRotateSpeed;
    [SerializeField] private float CamZoomSpeed;
    [SerializeField] private GameObject MainCamm;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(0, 0, CamMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(0, 0, -1 * CamMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(CamMoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(-1 * CamMoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -1 * CamRotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, CamRotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            MainCamm.transform.localPosition += new Vector3(-CamZoomSpeed, CamZoomSpeed, 0);
            CamMoveSpeed += CamMoveSpeed/5;
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            MainCamm.transform.localPosition += new Vector3(CamZoomSpeed, -CamZoomSpeed, 0);
            CamMoveSpeed -= CamMoveSpeed/5;
        }
    }
}
