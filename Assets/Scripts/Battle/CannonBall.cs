using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private float damage = 1.0f;
    [SerializeField] private float lifeTime = 5.0f;
    [SerializeField] private GameObject explosionPrefab;

    private String originName;

    public void Init(string ballOriginName){
        originName = ballOriginName;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // If the cannonball flies for lifeTime seconds, destroy it
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f){
            // Instantiate(explosionPrefab,
            //     gameObject.transform.position,
            //     gameObject.transform.rotation);
            Destroy(gameObject);
        }   
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.name != originName){
            BattleBoat enemy = hitInfo.GetComponent<BattleBoat>();
            if (enemy)
                enemy.TakeDamage(damage);
            // Instantiate(explosionPrefab,
            //     gameObject.transform.position,
            //     gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
