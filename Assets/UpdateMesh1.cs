using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class UpdateMesh1 : MonoBehaviour
{

    [SerializeField] NavMeshSurface meshSurface;
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


            if (meshSurface != null)
            {
                meshSurface.BuildNavMesh();
            }
            Destroy(gameObject);


        }
    }
}
