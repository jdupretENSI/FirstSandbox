using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    
    public float Speed;
    
    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontal * Speed, 0, vertical * Speed));
        
        // transform.Rotate(Vector3.up * (Speed * horizontal));
    }
}
