using UnityEngine;

public class FleshLightScript : MonoBehaviour
{
    private float charge;
    private float worktime = 10.0f;
    private Light flashLight;
    public float chargeLevel => charge;
    void Start()
    {
        charge = 1.0f;
        flashLight = GetComponent<Light>();
        GameState.AddCollectListener(ItemCollected);
    }

    private void ItemCollected(string itemName)
    {
        if(itemName == "Battery")
        {
            charge = 1.0f;
        }
        if(itemName == "SmallBattery")
        {
            charge = 0.5f;
        }
    }

    void Update()
    {
        if (!GameState.isDay)
        {
            if(charge>0)
            {
                flashLight.intensity = charge;
                charge -= Time.deltaTime/worktime;
            }
        }
        if(GameState.isFpv)
        {
            this.transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Vector3 f = Camera.main.transform.forward;
            f.y = 0.0f;
            if (f == Vector3.zero) f = Camera.main.transform.up;
            this.transform.forward = f.normalized;
         
        }
    }
}
