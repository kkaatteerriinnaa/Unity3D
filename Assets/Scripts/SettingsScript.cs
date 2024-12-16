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
    public void OnSoundEffectsChanged(System.Single value)
    {
        GameState.effectsVolume = value;
    }
    #endregion

    #region soundAmbientSlider
    private Slider soundAmbientSlider;
    private void initSoundAmbientSlider()
    {
        soundAmbientSlider = transform
            .Find("Content/Sound/AmbientSlider")
            .GetComponent<Slider>();
        GameState.ambientVolume = soundAmbientSlider.value;
    }
    public void OnSoundAmbientChanged(System.Single value)
    {
        GameState.ambientVolume = value;
    }
    #endregion

    #region soundsMuteToggle
    private Toggle soundsMuteToggle;
    private void initSoundsMuteToggle()
    {
        soundsMuteToggle = transform
            .Find("Content/Sound/MuteToggle")
            .GetComponent<Toggle>();
        GameState.isSoundsMuted = soundsMuteToggle.isOn;
    }
    public void OnSoundsMuteToggle(System.Boolean value)
    {
        GameState.isSoundsMuted = value;
    }
    #endregion

    #region Controls - Sensitivity
    private Slider sensXSlider;
    private Slider sensYSlider;
    private Toggle linkToggle;
    private bool isLinked;
    private void initControlsSensitivity()
    {
        sensXSlider = transform
           .Find("Content/Controls/SensXSlider")
           .GetComponent<Slider>();
        sensYSlider = transform
           .Find("Content/Controls/SensYSlider")
           .GetComponent<Slider>();
        linkToggle = transform
            .Find("Content/Controls/LinkToggle")
            .GetComponent<Toggle>();
        OnLinkToggle(linkToggle.isOn);
        OnSensXSlider(sensXSlider.value);
        if ( ! isLinked) OnSensYSlider(sensYSlider.value);
    }
    public void OnSensXSlider(System.Single value)
    {
        float sens = Mathf.Lerp(0.01f, 0.1f, value);
        GameState.lookSensitivityX = sens;
        if (isLinked)
        {
            sensYSlider.value = value;
            GameState.lookSensitivityY = -sens;
        }
    }
    public void OnSensYSlider(System.Single value)
    {
        float sens = Mathf.Lerp(-0.01f, -0.1f, value);
        GameState.lookSensitivityY = sens;
        if (isLinked)
        {
            sensXSlider.value = value;
            GameState.lookSensitivityX = -sens;
        }
    }
    public void OnLinkToggle(System.Boolean value)
    {
        isLinked = value;
    }
    #endregion

    #region Controls - FPV limit
    private Slider fpvSlider;
    private void initControlsFpv()
    {
        fpvSlider = transform
           .Find("Content/Controls/FpvSlider")
           .GetComponent<Slider>();
        OnFpvSlider(fpvSlider.value);
    }
    public void OnFpvSlider(System.Single value)
    {
        GameState.fpvRange = Mathf.Lerp(0.3f, 1.1f, value);       
    }
    #endregion

    void Start()
    {
        initSoundEffectsSlider();
        initSoundAmbientSlider();
        initSoundsMuteToggle();
        initControlsSensitivity();
        initControlsFpv();

        content = transform.Find("Content").gameObject;
        if (content.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);
        }
    }

}
