using UnityEngine;
using UnityEngine.UI;

public class KeyPointImageScript : MonoBehaviour
{
    private Image image;
    private KeyPontScript keyPointScript;
    void Start()
    {
        image = GetComponent<Image>();
        keyPointScript = GetComponentInParent<KeyPontScript>();
    }

    void Update()
    {
        image.fillAmount = keyPointScript.part;
        image.color = new Color(1 - image.fillAmount, image.fillAmount, 0.3f);
    }
}
