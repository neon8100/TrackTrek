using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Audio Asset Collection")]
public class AudioAssetCollection : ScriptableObject
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
