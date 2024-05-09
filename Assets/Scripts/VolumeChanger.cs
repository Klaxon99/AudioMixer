using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private const string MasterVolumeParameter = "MasterVolume";
    private const string BackgroungVolumeParameter = "BackgroundVolume";
    private const string EffectsVolumeParameter = "EffectsVolume";

    private bool _isMuted = false;
    private float _minVolume = -80f;
    private float _oldMasterVolume;

    public void ChangeBackgroundVolume(float value)
    {
        ChangeVolume(BackgroungVolumeParameter, value);
    }

    public void ChangeGlobalVolume(float value)
    {
        if (_isMuted)
        {
            _oldMasterVolume = ConvertToVolume(value);
        }
        else
        {
            ChangeVolume(MasterVolumeParameter, value);
        }
    }

    public void ChangeEffectsVolume(float value)
    {
        ChangeVolume(EffectsVolumeParameter, value);
    }

    public void SwitchMute()
    {
        if (_isMuted)
        {
            _mixer.SetFloat(MasterVolumeParameter, _oldMasterVolume);
        }
        else
        {
            _mixer.GetFloat(MasterVolumeParameter, out _oldMasterVolume);
            _mixer.SetFloat(MasterVolumeParameter, _minVolume);
        }

        _isMuted = !_isMuted;
    }

    private float ConvertToVolume(float value) => Mathf.Log10(value) * 20f;

    private void ChangeVolume(string volumeParameter, float value)
    {
        _mixer.SetFloat(volumeParameter, ConvertToVolume(value));
    }
}