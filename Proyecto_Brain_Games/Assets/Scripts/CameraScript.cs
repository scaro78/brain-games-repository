using UnityEngine;

public class CameraScript : MonoBehaviour
{
    PlayerScript playerScr;
    [SerializeField] float offsetX;
    private void Awake()
    {
        playerScr = Object.FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        Vector2 playerPos = playerScr.GetPosition();
        transform.position = new Vector3(playerPos.x + offsetX, transform.position.y, transform.position.z);
    }
}
