using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelOptionsController : MonoBehaviour
{
    MeshFilter modelMesh;
    MeshCollider modelCollider;
    MeshRenderer modelRenderer;

    [Serializable]
    public class DynamicButton
    {
        public string buttonName;
    }

    [Serializable]
    public class MeshOption : DynamicButton
    {
        public Mesh mesh;
    }

    [Serializable]
    public class MaterialOption : DynamicButton
    {
        public Material material;
    }

    [Serializable]
    public class TextureOption : DynamicButton
    {
        public Texture texture;
    }

    public List<MeshOption> meshOptions;
    public List<MaterialOption> materialOptions;
    public List<TextureOption> textureOptions;

    private void Start()
    {
        modelMesh = GetComponent<MeshFilter>();
        modelCollider = GetComponent<MeshCollider>();
        modelRenderer = GetComponent<MeshRenderer>();
    }

    public void SelectNewMesh(string buttonName)
    {
        Mesh newMesh = meshOptions.Find(x => x.buttonName == buttonName).mesh;
        modelMesh.mesh = newMesh;
        modelCollider.sharedMesh = newMesh;
    }

    public void SelectNewMaterial(string buttonName)
    {
        modelRenderer.sharedMaterial = materialOptions.Find(x => x.buttonName == buttonName).material;
    }

    public void SelectNewTexture(string buttonName)
    {
        modelRenderer.sharedMaterial.mainTexture = textureOptions.Find(x => x.buttonName == buttonName).texture;
    }

    

}
