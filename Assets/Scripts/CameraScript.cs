using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private InputAction lookAction;
    private GameObject cameraPosition3;
    private GameObject Character;
    private Vector3 c;
    private Vector3 cameraAngels;
    private bool isFpv;
    private float sensitivityH = 0.2F;
    private float sensitivityV = -0.25F;

    private float minDistance = 2.0f;
    private float maxDistance = 10.0f;
    private float smoothTransitionSpeed = 5.0f;

    private float minVerticalAngle = 35.0f;
    private float maxVerticalAngle = 75.0f;

    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        cameraPosition3 = GameObject.Find("CameraPosition");
        cameraAngels = this.transform.eulerAngles;
        Character = GameObject.Find("Character");
        c = this.transform.position - Character.transform.position;
        isFpv = true;
    }

    void Update()
    {
        if (Time.timeScale == 0.0f) return;
        if (isFpv)
        {
            float wheel = Input.mouseScrollDelta.y;
            c *= 1 - wheel / 10.0f;




            GameState.isFpv = c.magnitude < 0.25f;
            c = Vector3.ClampMagnitude(c, maxDistance);
            if (c.magnitude < minDistance)
            {
                c = c.normalized * minDistance;
            }

            Vector2 lookValue = lookAction.ReadValue<Vector2>();
            cameraAngels.x += lookValue.y * sensitivityV;
            cameraAngels.y += lookValue.x * sensitivityH;

            cameraAngels.x = Mathf.Clamp(cameraAngels.x, minVerticalAngle, maxVerticalAngle);

            this.transform.eulerAngles = cameraAngels;

            if (c.magnitude < minDistance + 1.0f)
            {
                c = Vector3.Lerp(c, c.normalized * minDistance, Time.deltaTime * smoothTransitionSpeed);
            }

            this.transform.position = Character.transform.position +
                                       Quaternion.Euler(0, cameraAngels.y, 0) * c;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isFpv)
            {
                this.transform.position = cameraPosition3.transform.position;
                this.transform.rotation = cameraPosition3.transform.rotation;
            }
            isFpv = !isFpv;
        }
    }
}
