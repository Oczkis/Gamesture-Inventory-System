using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [SerializeField] string _volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _volumeSlider;
    [SerializeField] float _multiplier = 30f;

    [SerializeField] AudioPlayer equipAudioPlayer;
    [SerializeField] AudioPlayer eatAudioPlayer;
    [SerializeField] AudioPlayer clickAudioPlayer;
    [SerializeField] AudioPlayer messageAudioPlayer;

    void Awake()
    {
        if (_instance == null)
            _instance = this;

       _volumeSlider.onValueChanged.AddListener(HandleVolumeChanged);
       _volumeSlider.value = 0.5f;
    }

    void OnEnable()
    {
        Inventory.OnItemEquipped += AudioManagerHandleItemEquipped;
        Inventory.OnItemDeEquipped += AudioManagerHandleItemEquipped;
    }

    void OnDisable()
    {
        Inventory.OnItemEquipped -= AudioManagerHandleItemEquipped;
        Inventory.OnItemDeEquipped -= AudioManagerHandleItemEquipped;
    }

    private void HandleVolumeChanged(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value)* _multiplier);
    }

    private void AudioManagerHandleItemEquipped(EquipmentSlot eq)
    {
        PlayRandomAudioSound("Equip");
    }

    public void PlayRandomAudioSound(string audio)
    {
        switch (audio)
        {
            case "Equip": equipAudioPlayer.PlayRandomAudioSound(); break;
            case "Eat": eatAudioPlayer.PlayRandomAudioSound(); break;
            case "Message": messageAudioPlayer.PlayRandomAudioSound(); break;
            case "Click": clickAudioPlayer.PlayRandomAudioSound(); break;
        }
    }
}
