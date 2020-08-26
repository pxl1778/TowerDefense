using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    [SerializeField]
    private float Speed = 5;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            var direction = (target.transform.position - transform.position).normalized;
            this.transform.position += direction * Speed * Time.deltaTime;
        }
    }

    public void SetTarget(Enemy target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && enemy == target)
        {
            Debug.Log("Projectile hit");
            target.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
