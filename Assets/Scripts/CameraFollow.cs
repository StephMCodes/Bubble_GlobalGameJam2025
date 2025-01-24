using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public Vector3 offset;    // Offset from the player

    private void LateUpdate()
    {
        // Follow player position with offset, but don't affect rotation
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);

        // Ensure the camera's rotation stays fixed
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}