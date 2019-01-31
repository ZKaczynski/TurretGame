using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour { 

    [Header("Stats")]
    public float range;
    public float lockrange;
    public float rotationSpeed;
    public float fireRate;
    public float maxAngle;
    public float minAngle;

    [Header("Ammo")]
    public GameObject ammo;
    public float recoil;

    [Header("Setters")]
    public string tagToShoot;
    public Transform[] firePoint;
    public Transform[] barrels;
    public Transform towerHead;

    private Transform target;
    private float currentDistanceToTarget=0.0f;
    private float fireTimer=0.0f;


    void Update() { 
        InvokeRepeating("FindEnemys", 0.0f, 1f);
        if (target == null) return;
        TargetEnemy();
        if (currentDistanceToTarget < range) {
            if (fireTimer > 1.0f / fireRate) {
                Shoot();
                fireTimer = 0.0f;
            } else {
                fireTimer += Time.deltaTime;
            }
        }
    }

    void Shoot() {
        foreach (Transform point in firePoint) {
            Instantiate(ammo, point.position, point.rotation);
        }      
        foreach (Transform barrel in barrels) {
            Vector3 stablePosition = barrel.localPosition;
            barrel.localPosition = new Vector3(stablePosition.x, stablePosition.y, stablePosition.z - recoil);
            StartCoroutine(MoveBarrel(barrel,stablePosition));
        }
    }

    void TargetEnemy(){
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation =  Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed*Time.deltaTime).eulerAngles;
        if (rotation.x > 360.0f-maxAngle||rotation.x<minAngle) {
            towerHead.rotation = Quaternion.Euler(rotation.x, rotation.y, 0.0f);
        } else if (rotation.x<=360.0f&&rotation.x>180.0f) {
            towerHead.rotation = Quaternion.Euler(360.0f - maxAngle, rotation.y, 0.0f);
        } else {
            towerHead.rotation = Quaternion.Euler(minAngle, rotation.y, 0.0f);
        }

    }

    void FindEnemys() {
        GameObject[] enemys;
        enemys = GameObject.FindGameObjectsWithTag(tagToShoot);
        GameObject bestAsFar = null;
        float bestDistance = Mathf.Infinity;
        foreach ( GameObject enemy in enemys){
            Vector3 enemyPosition = enemy.transform.position;
            float angle = Quaternion.Angle(transform.rotation, enemy.transform.rotation);
            Debug.Log(angle);
            float distance = Vector3.Distance(enemyPosition, transform.position);
            if (distance < bestDistance) {
                bestAsFar = enemy;
                bestDistance = distance;
            }
            if (bestDistance < lockrange) {
                currentDistanceToTarget = bestDistance;
                target = bestAsFar.transform;
            } else {
                target = null;
                currentDistanceToTarget = Mathf.Infinity;
            }
        }
    }

    IEnumerator MoveBarrel(Transform trans, Vector3 stablePosition) {
        while (trans.localPosition.z <= stablePosition.z) {
            trans.localPosition = Vector3.Lerp(trans.localPosition, stablePosition,  2.0f*fireRate* Time.deltaTime);
            yield return null;
        }
    }

}
