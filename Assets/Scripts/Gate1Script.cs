using UnityEngine;

public class Gate1Script : MonoBehaviour
{
    [SerializeField]
    private string keyName = "1";

    private string closedMessageTpl = "Двері зачинені!\r\nЗнайдіть +1 ключ'";
    private AudioSource closedSound;
    private AudioSource openingSound;

    [SerializeField]
    private float baseOpeningTime = 3.0f; 
    [SerializeField]
    private float slowOpeningMultiplier = 2.0f; 

    private float openingTime;
    private float timeout = 0f;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        closedSound = audioSources[0];
        openingSound = audioSources[1];
        GameState.AddChangeListener(
            OnSoundsVolumeChanged,
            nameof(GameState.effectsVolume),
            nameof(GameState.isSoundsMuted));
    }

    void Update()
    {
        if (timeout > 0f)
        {
            timeout -= Time.deltaTime;
            transform.Translate(0, 0, Time.deltaTime / openingTime);
            if (timeout <= 0.0f)
            {
                GameState.room += 1;
                Debug.Log(GameState.room);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            if (GameState.collectedItems.TryGetValue("Key" + keyName, out object partObj) && partObj is float part)
            {
                if (timeout == 0f)
                {
                    openingTime = part > 0 ? baseOpeningTime : baseOpeningTime * slowOpeningMultiplier;

                    GameState.TriggerEvent("Gate",
                        new GameEvents.GateEvent { message = "Двері відчинено!" });
                    timeout = openingTime;
                    openingSound.Play();
                }
            }
            else
            {
                GameState.TriggerEvent("Gate",
                    new GameEvents.GateEvent { message = closedMessageTpl.Replace("%s", keyName) });
                closedSound.Play();
            }
        }
    }

    private void OnSoundsVolumeChanged(string name)
    {
        closedSound.volume =
            openingSound.volume =
                GameState.isSoundsMuted ? 0f : GameState.effectsVolume;
    }

    private void OnDestroy()
    {
        GameState.RemoveChangeListener(
            OnSoundsVolumeChanged,
            nameof(GameState.effectsVolume),
            nameof(GameState.isSoundsMuted));
    }
}
