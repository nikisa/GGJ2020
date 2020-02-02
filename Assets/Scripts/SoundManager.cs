using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SoundManager
{

    public enum Sound
    {
        pressedButton,
        galoppata1,
        galoppata2,
        galoppata3,
        galoppata4,
        nitrito1,
        nitrito2,
        nitrito3,
        nitrito4,
        sbuffo1,
        sbuffo2,
        sbuffo3,
        sbuffo4,
        steelBrokenArmor1,
        steelBrokenArmor2,
        steelBrokenArmor3,
        steelBrokenArmor4,
        spearSound,
        funnyLaunch,
        wearingArmor,
        slaveWalking1,
        slaveWalking2,
        slaveWalking3,
        slaveWalking4,
        follaLoop,
        knightFalling,
        armorDankWalking,
        trombe,
        scorra,
        menu,
        gameplay
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }

        }
        return null;
    }
}