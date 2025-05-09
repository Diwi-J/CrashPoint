using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       // movement.x = Input.GetAxisRaw("Horizontal");
       // movement.y = Input.GetAxisRaw("Vertical");
    }
}
