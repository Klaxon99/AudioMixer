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

    private float ConvertToVolume(float value) => Mathf.Log10(value) * 20f;

    public void ChangeBackgroundVolume(float value)
    {
        ChangeVolume(BackgroungVolumeParameter, value);
    }

    public void ChangeGlobalValue(float value)
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

    public void ChangeEffectsValue(float value)
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

    private void ChangeVolume(string volumeParameter, float value)
    {
        _mixer.SetFloat(volumeParameter, ConvertToVolume(value));
    }
}