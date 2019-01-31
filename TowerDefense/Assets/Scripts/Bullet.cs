using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0.0f, speed, 0.0f));
    }

    private void Update() {
        if (timer > lifetime) {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }



}
