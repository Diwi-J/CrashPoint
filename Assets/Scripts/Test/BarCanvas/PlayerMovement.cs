using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // WASD input for 2D movement (X and Y)
        if (Input.GetKey(KeyCode.W))
            moveY = 1f;
        if (Input.GetKey(KeyCode.S))
            moveY = -1f;
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // Move the player on X and Y axes
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
