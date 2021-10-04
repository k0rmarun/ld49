using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomizedSound : MonoBehaviour
{
    public static string DIE = "Audio/Die";
    public static string NEXT_LEVEL = "Audio/NextLevel";
    public static string MOVEMENT = "Audio/Movement";
    public static string PLACE = "Audio/Place";
    public static string DROP = "Audio/Drop";
    public static string FUSE = "Audio/Fuse";
    public static string EXPLODE = "Audio/Explode";
    public WeightedAudioClip[] sounds;

    private static Dictionary<string, int> lastPlayedSlots = new Dictionary<string, int>();

    public static void TriggerUpdate()
    {
        foreach (var key in lastPlayedSlots.Keys.ToList())
        {
            lastPlayedSlots[key] = Mathf.Max(0, lastPlayedSlots[key] - 1);
        }
    }

    public void Play(bool sourceDeleted)
    {
        int totalWeight = 0;
        foreach (WeightedAudioClip randomizedSound in sounds)
        {
            totalWeight += randomizedSound.weight;
        }

        int chosenSound = Random.Range(0, totalWeight);
        totalWeight = 0;
        AudioSource source = GetComponent<AudioSource>();
        if (sourceDeleted)
        {
            source.transform.SetParent(null);
        }

        foreach (WeightedAudioClip randomizedSound in sounds)
        {
            if (totalWeight <= chosenSound && chosenSound < totalWeight + randomizedSound.weight)
            {
                source.Play();
                string name = randomizedSound.clip.name;
                if (!lastPlayedSlots.ContainsKey(name))
                {
                    lastPlayedSlots.Add(name, 0);
                }

                if (lastPlayedSlots[name] < 9)
                {
                    source.PlayOneShot(randomizedSound.clip);
                    lastPlayedSlots[name]++;
                }

                break;
            }

            totalWeight += randomizedSound.weight;
        }
    }

    public static void Play(Transform baseObject, string soundSet, bool sourceDeleted = false)
    {
        Transform playFrom = baseObject.transform.Find(soundSet);
        if (playFrom)
        {
            playFrom.GetComponent<RandomizedSound>().Play(sourceDeleted);
        }
    }
}