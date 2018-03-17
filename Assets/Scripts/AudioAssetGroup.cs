using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Assets")]
public class AudioAssets : ScriptableObject
{
    public List<AudioAsset> audioAssets;

    public AudioClip GetAudioClip(AudioClipTypes audioClipType)
    {
        foreach (AudioAsset asset in audioAssets)
        {
            if (asset.audioClipType == audioClipType)
            {
                return asset.audioClip;
            }
        }

        return null;
    }

}
