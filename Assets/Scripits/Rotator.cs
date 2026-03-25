using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float rotateSpeed = 30;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

}