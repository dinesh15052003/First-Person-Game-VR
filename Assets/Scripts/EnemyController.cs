using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TechXR.Core.Sense;

public class EnemyController : GazePointer
{ 
    public GameObject particleEffect;
    public float speed;
    public Animator enemyModel;
    Vector3 endPos;
    BulletSpawner bulletSpawner;
    // Start is called before the first frame update
    void Start()
    {
        endPos = 1.5f * (transform.position - Vector3.zero).normalized;
        bulletSpawner = GameObject.FindObjectOfType<BulletSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPos, speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            Attack();
        else if(other.CompareTag("Enemy"))
            Death();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        bulletSpawner.ShootBullet();
        //Death();
    }

    public void Death()
    {
        particleEffect.SetActive(true);
        particleEffect.transform.SetParent(null);
        Destroy(gameObject);
        PlayerManager.currentScore += 100;
    }

    public void Attack()
    {
        enemyModel.SetTrigger("attack");
        PlayerManager.playerHealth -= 0.2f;
    }
}
