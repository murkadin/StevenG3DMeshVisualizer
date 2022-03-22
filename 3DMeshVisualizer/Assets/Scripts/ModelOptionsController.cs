using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Mesh, Material and Texture options for the Model.
/// </summary>
[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class ModelOptionsController : MonoBehaviour
{
    /// <summary>
    /// Inspector set mesh options available to the User.
    /// </summary>
    public List<MeshOption> MeshOptions;

    /// <summary>
    /// Inspector set material options available to the User.
    /// </summary>
    public List<MaterialOption> MaterialOptions;

    /// <summary>
    /// Inspector set texture options available to the User.
    /// </summary>
    public List<TextureOption> TextureOptions;

    Material _currentMaterial;
    MeshFilter _modelMesh;
    MeshCollider _modelCollider;
    MeshRenderer _modelRenderer;

    /// <summary>
    /// Holds data for a button that has a name to display to a user.
    /// </summary>
    [Serializable]
    public class DynamicButton
    {
        /// <summary>
        /// The name that should display for the button.
        /// </summary>
        public string ButtonName;
    }

    /// <summary>
    /// Holds the data needed to display a Mesh Option to the user.
    /// </summary>
    [Serializable]
    public class MeshOption : DynamicButton
    {
        /// <summary>
        /// The mesh for this option.
        /// </summary>
        public Mesh Mesh;
    }

    /// <summary>
    /// Holds the data needed to display a Material Option to the user.
    /// </summary>
    [Serializable]
    public class MaterialOption : DynamicButton
    {
        /// <summary>
        /// The material for this option.
        /// </summary>
        public Material Material;
    }

    /// <summary>
    /// Holds the data needed to display a Texture Option to the user.
    /// </summary>
    [Serializable]
    public class TextureOption : DynamicButton
    {
        /// <summary>
        /// The Base Map texture for this option.
        /// </summary>
        public Texture Texture;

        /// <summary>
        /// The Normal Map texture for this option.
        /// </summary>
        public Texture NormalTexture;
    }

    /// <summary>
    /// Applies the new mesh to the model based on which button was selected.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed when selecting the Mesh.</param>
    public void SelectNewMesh(string buttonName)
    {
        Mesh newMesh = MeshOptions.Find(x => x.ButtonName == buttonName).Mesh;
        _modelMesh.mesh = newMesh;
        _modelCollider.sharedMesh = newMesh;
    }

    /// <summary>
    /// Applies the new material to the model based on which button was selected. This will also carry forward the Texture that was previously selected.
    /// </summary>
    /// <param name="buttonName">The name of the button pressed when selecting the Material.</param>
    public void SelectNewMaterial(string buttonName)
    {
        TextureOption currTextureOption = TextureOptions.Find(x => _modelRenderer.sharedMaterial.mainTexture == x.Texture);
        _modelRenderer.sharedMaterial = MaterialOptions.Find(x => x.ButtonName == buttonName).Material;

        //Apply the texture we have been using to the new material so that the only thing which changes is the material being used.
        SetTexture(currTextureOption);

        //Now that we have changed materials, we need to destroy the previous instanced material.
        Destroy(_currentMaterial);
        _currentMaterial = _modelRenderer.sharedMaterial;
    }

    /// <summary>
    /// Applies the new texture based on which button was selected.
    /// </summary>
    /// <param name="buttonName">he name of the button pressed when selecting the Texture.</param>
    public void SelectNewTexture(string buttonName)
    {
        SetTexture(TextureOptions.Find(x => x.ButtonName == buttonName));
    }

    private void SetTexture(TextureOption newTexture)
    {
        //Accessing the .material of a renderer will cause the material to instance if it has not already. 
        _modelRenderer.material.mainTexture = newTexture.Texture;

        if(newTexture.NormalTexture != null)
            _modelRenderer.material.EnableKeyword("_NORMALMAP");
        else
            _modelRenderer.material.DisableKeyword("_NORMALMAP");

        _modelRenderer.material.SetTexture("_BumpMap", newTexture.NormalTexture);
    }

    private void Start()
    {
        _modelMesh = GetComponent<MeshFilter>();
        _modelCollider = GetComponent<MeshCollider>();
        _modelRenderer = GetComponent<MeshRenderer>();

        //Accessing the .material will cause Unity to instance the material. 
        //We want this because the user will be changing material settings (textures) and we do not want to change the base material, just the version on this object.
        //We need to store a reference to the instanced material so it can be cleaned up.
        _currentMaterial = _modelRenderer.material;
    }

    private void OnDestroy()
    {
        Destroy(_currentMaterial);
    }
}
