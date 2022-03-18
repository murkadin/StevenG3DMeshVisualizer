using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectsManager : MonoBehaviour
{
    public Volume volume;

    public List<PostProcessingEffect> postProcessingEffects;
    public List<LightEffect> lightEffects;

    [Serializable]
    public class PostProcessingEffect
    {
        public string displayName;
        public string effectName;
    }

    [Serializable]
    public class LightEffect
    {
        public string displayName;
        public Light light;
    }

    public void SetPostProcessingEffectState(bool state, string effectName)
    {
        volume.profile.components.Find(x => x.name.Contains(effectName)).active = state;
    }

    public bool GetPostProcessingEffectState(string effectName)
    {
        return volume.profile.components.Find(x => x.name.Contains(effectName)).active;
    }

    public void SetLightEffectState(bool state, string displayName)
    {
        lightEffects.Find(x => x.displayName == displayName).light.gameObject.SetActive(state);
    }

    public bool GetLightEffectState(string displayName)
    {
        return lightEffects.Find(x => x.displayName == displayName).light.gameObject.activeInHierarchy;
    }
}
