using UnityEngine;

public class Gate1Script : MonoBehaviour
{
    private string closedMessage = "Двері зачинено!\r\nДля відкривання двері необхідно знайти ключ № 1. Продовжуйте пошук";
    private AudioSource closedSound;

    void Start()
    {
        closedSound = GetComponent<AudioSource>();
        GameState.AddChangeListener(
           OnSoundsVolumeChanged,
           nameof(GameState.effectsVolume));
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Character")
        {
            // bool r = Random.value < 0.5f;
            MessagesScript.ShowMessage(closedMessage);
            closedSound.Play();
        }
    }

    private void OnSoundsVolumeChanged(string name)
    {
        closedSound.volume = GameState.effectsVolume;
    }

    private void OnDestroy()
    {
        GameState.RemoveChangeListener(
            OnSoundsVolumeChanged,
            nameof(GameState.effectsVolume));
    }
}
