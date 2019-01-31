using UnityEngine;
using System.Collections;

public class RayMaker : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(400, 200, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}