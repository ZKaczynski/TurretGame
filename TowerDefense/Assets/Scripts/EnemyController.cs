using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHP;

    private int HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

  
    private void OnCollisionEnter(Collision collision) {
        Debug.Log("HIT!");
        if (collision.gameObject.CompareTag("Bullet")){
            Destroy(collision.gameObject);
            HP -= 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) {
            Destroy(gameObject);
        }
    }
}
