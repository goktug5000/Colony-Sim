using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class NavMeshUpdate : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
        if (surface == null)
        {
            surface = this.gameObject.GetComponent<NavMeshSurface>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            UpdateNavMesh();
            Debug.Log("NavMesh updated");

        }

    }
    public void UpdateNavMesh()
    {
        surface.BuildNavMesh();
    }
}
