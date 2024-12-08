using UnityEngine;
public enum BatteryType
{
    Normal,   
    Small     
}

public class BatteryScript : MonoBehaviour
{
    public BatteryType batteryType;

    private void OnTriggerEnter(Collider other)
    {
       
        switch (batteryType)
        {
            case BatteryType.Normal:
                GameState.Collect("Battery"); 
                break;

            case BatteryType.Small:
                GameState.Collect("SmallBattery");
                break;
        }
        Destroy(gameObject);
    }
}
