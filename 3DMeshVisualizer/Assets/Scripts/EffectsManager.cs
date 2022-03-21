using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Handles the scene's Light and Post Processing effects that the user can select.
/// </summary>
public class EffectsManager : MonoBehaviour
{
    /// <summary>
    /// All of the Post Processing effects available to the user.
    /// </summary>
    public List<PostProcessingEffect> PostProcessingEffects;

    /// <summary>
    /// All of the light effects available to the user.
    /// </summary>
    public List<LightEffect> LightEffects;

    /// <summary>
    /// The volume that will be handling post processing for this scene.
    /// </summary>
    [SerializeField]
    private Volume _volume;

    /// <summary>
    /// Holds relevant data for a Post Processing effect.
    /// </summary>
    [Serializable]
    public class PostProcessingEffect
    {
        /// <summary>
        /// The name that should display when being shown to the user.
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// A reference to the post processing profile for this effect.
        /// </summary>
        public VolumeProfile VolumeProfile;
    }

    /// <summary>
    /// Holds relevant data for a light effect.
    /// </summary>
    [Serializable]
    public class LightEffect
    {
        /// <summary>
        /// The name that should display when being shown to the user.
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// A reference to the light for this effect.
        /// </summary>
        public Light Light;
    }

    /// <summary>
    /// Activates a specific Post Processing effect.
    /// </summary>
    /// <param name="displayName">The name displayed to the user when selecting the effect.</param>
    public void ActivatePostProcessingEffect(string displayName)
    {
        _volume.profile = PostProcessingEffects.Find(x => x.DisplayName == displayName).VolumeProfile;
    }

    /// <summary>
    /// Activates a specific light effect.
    /// </summary>
    /// <param name="displayName">The name displayed to the user when selecting the effect.</param>
    public void SetLightEffectState(bool state, string displayName)
    {
        LightEffects.Find(x => x.DisplayName == displayName).Light.gameObject.SetActive(state);
    }

    /// <summary>
    /// Gets the status on a specific light effect.
    /// </summary>
    /// <param name="displayName">The name of the effects displayed to the user.</param>
    /// <returns>The active status of the effect.</returns>
    public bool GetLightEffectState(string displayName)
    {
        return LightEffects.Find(x => x.DisplayName == displayName).Light.gameObject.activeInHierarchy;
    }
}
