using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class UpdateMesh : MonoBehaviour
{
    [SerializeField] NavMeshSurface meshSurface;
    [SerializeField] GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (cube.activeSelf == true)
            {
                
                cube.SetActive(false);


                if (meshSurface != null)
                {
                    meshSurface.BuildNavMesh();
                }
                
            }
            else
            {
                cube.SetActive(true);
                if (meshSurface != null)
                {
                    meshSurface.BuildNavMesh();
                }
            }
        }
        
    }
}
