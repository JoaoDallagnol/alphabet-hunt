using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        float masterVolume, soundVolume, musicVolume;

        // Tenta obter os valores do mixer. Note que esses valores estão em decibéis.
        // É preciso converter para o intervalo linear (ex: 0 a 1) se os sliders trabalharem nesse range.
        if (audioMixer.GetFloat("masterVolume", out masterVolume))
        {
            masterSlider.value = DecibelToLinear(masterVolume);
        }
        if (audioMixer.GetFloat("soundFXVolume", out soundVolume))
        {
            soundSlider.value = DecibelToLinear(soundVolume);
        }
        if (audioMixer.GetFloat("musicVolume", out musicVolume))
        {
            musicSlider.value = DecibelToLinear(musicVolume);
        }
    }

    // Conversão de decibéis para valor linear (aproximação)
    private float DecibelToLinear(float dB)
    {
        // Se você usou a fórmula: Mathf.Log10(value) * 20 para setar o volume,
        // a inversa pode ser: 10^(dB/20)
        return Mathf.Pow(10f, dB / 20f);
    }

    // Atualiza os volumes conforme o slider muda
    // public void OnMasterVolumeChanged(float value)
    // {
    //     soundMixerManager.SetMasterVolume(value);
    // }

    // public void OnSoundVolumeChanged(float value)
    // {
    //     soundMixerManager.SetSoundVolume(value);
    // }

    // public void OnMusicVolumeChanged(float value)
    // {
    //     soundMixerManager.SetMusicVolume(value);
    // }
}
