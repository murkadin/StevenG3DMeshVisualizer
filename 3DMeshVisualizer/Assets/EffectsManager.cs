using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectsManager : MonoBehaviour
{
    public Volume volume;

    public List<PostProcessingEffect> postProcessingEffects;

    [Serializable]
    public class PostProcessingEffect
    {
        public string displayName;
        public string effectName;
    }

    public void SetPostProcessingEffectState(bool state, string effectName)
    {
        volume.profile.components.Find(x => x.name.Contains(effectName)).active = state;
    }
}
