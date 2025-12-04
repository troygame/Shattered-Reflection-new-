using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position=player.position+new Vector3(0,0,-10);
    }
}
