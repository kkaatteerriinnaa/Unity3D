using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 f;
    private InputAction moveAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        
        f = Camera.main.transform.forward;
        f.y = 0.0f; 
        if(f == Vector3.zero)  
        {  
            f = Camera.main.transform.up;
            f.y = 0.0f;
        }
        f.Normalize();


        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.AddForce(Time.deltaTime * 300 * 

            (
                moveValue.x * Camera.main.transform.right +
                moveValue.y * f
            )
        );
    }
}
