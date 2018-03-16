using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AudioAsset
{
    public AudioClipTypes audioClipType;
    //SFX for this object
    public AudioClip audioClip;
}

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
