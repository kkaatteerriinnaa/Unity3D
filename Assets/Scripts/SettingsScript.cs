using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private GameObject content;
    #region soundEffectsSlider
    private Slider soundEffectsSlider;
    private void initSoundEffectsSlider()
    {
        soundEffectsSlider = transform
            .Find("Content/Sound/EffectsSlider")
            .GetComponent<Slider>();

        GameState.effectsVolume = soundEffectsSlider.value;
    }
    #endregion

    #region SoundAmbientSlider
    private Slider SoundAmbientSlider;
    private void initSoundAmbientSlider()
    {
        SoundAmbientSlider = transform
            .Find("Content/Sound/AmbientSlider")
            .GetComponent<Slider>();

        GameState.ambientVolume = SoundAmbientSlider.value;
    }
    #endregion

    #region SoundsMuteToggle
    private Toggle SoundsMuteToggle;
    private void initSoundsMuteToggle()
    {
        SoundsMuteToggle = transform
            .Find("Content/Sound/MuteToggle")
            .GetComponent<Toggle>();

        GameState.isSoundsMuted = SoundsMuteToggle.isOn;
    }
    public void OnSoundsMuteToggle(System.Boolean value)
    {
        GameState.isSoundsMuted = value;
    }
    #endregion

    void Start()
    {
        initSoundEffectsSlider();
        initSoundAmbientSlider();
        initSoundsMuteToggle();
        content = transform
            .Find("Content")
            .gameObject;
        

        if (content.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale= content.activeInHierarchy ? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);
        }
    }
    public void OnSoundsEffectsChanged(System.Single value)
    {
        GameState.effectsVolume = value;
    }
    public void OnSoundsAmbientChanged(System.Single value)
    {
        GameState.ambientVolume = value;
    }
}
