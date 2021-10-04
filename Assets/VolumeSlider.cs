using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;

    private void Update()
    {
        float master;
        mixer.GetFloat("Master", out master);
        GetComponent<Slider>().value = master;
    }

    public void SetMaster()
    {
        mixer.SetFloat("Master", GetComponent<Slider>().value);
    }
}