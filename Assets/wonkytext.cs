using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class wonkytext : MonoBehaviour
{

    public TMP_Text textComponent;

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var characterInfo = textInfo.characterInfo[i];

            if (!characterInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[characterInfo.materialReferenceIndex].vertices;
            var meshInfo = textInfo.meshInfo[characterInfo.materialReferenceIndex];
            for (int j = 0; j < 4; ++j)
            {
                var index = characterInfo.vertexIndex + j;
                var orig = meshInfo.vertices[index];
                //verts[characterInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 20f, 0);
                meshInfo.vertices[index] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 20f, 0);
                meshInfo.colors32[index] = Color.red;
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i) 
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
