using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTurret : MonoBehaviour
{
    private SphereCollider detectRadius;
    private HomingParticles homingParticles;
    [SerializeField]
    private GameObject ProjectilePrefab;
    private Enemy currentTarget;
    private bool isAttacking;
    private bool isActivated;
    [SerializeField]
    private float attackPower = 1;
    [SerializeField]
    private float attackCooldown = 1;
    private float cooldownTimer = 0;
    [SerializeField]
    private int placementCost = 10;
    public int PlacementCost { get { return placementCost; } }

    // Start is called before the first frame update
    void Start()
    {
        detectRadius = GetComponent<SphereCollider>();
        detectRadius.enabled = false;
        homingParticles = GetComponentInChildren<HomingParticles>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= attackCooldown)
            {
                Attack();
                cooldownTimer = 0;
            }
        }
    }

    protected virtual void Attack()
    {
        //currentTarget.TakeDamage(attackPower);
        //homingParticles.SetTarget(currentTarget);
        var bullet = GameObject.Instantiate(ProjectilePrefab);
        var projectile = bullet.GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.SetTarget(currentTarget, attackPower);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("collided enemy");
            currentTarget = other.gameObject.GetComponent<Enemy>();
            if(currentTarget != null)
            {
                isAttacking = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentTarget != null && other.gameObject == currentTarget.gameObject)
        {
            currentTarget = null;
            isAttacking = false;
        }
    }

    public void Activate()
    {
        isActivated = true;
        detectRadius.enabled = true;
    }
}
