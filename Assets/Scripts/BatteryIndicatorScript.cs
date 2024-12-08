using UnityEngine;
using UnityEngine.UI;

public class BatteryIndicatorScript : MonoBehaviour
{
    private Image image;
    private FleshLightScript flashLighScript;
    void Start()
    {
        image = GetComponent<Image>();
        flashLighScript = GameObject
            .Find("FlashLight")
            .GetComponent<FleshLightScript>();
    }

    void Update()
    {
        image.fillAmount = flashLighScript.chargeLevel;
        image.color = new Color(
            (60 + (1 - image.fillAmount) * 130) / 255f,
            (30 + (image.fillAmount) * 130) / 255f,
            (30 + (image.fillAmount) * 30) / 255f
            );
    }
}
