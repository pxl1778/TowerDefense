using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingParticles : MonoBehaviour
{
    [SerializeField]
    private float particleSpeed = 5;
    private ParticleSystem attackParticles;
    private Enemy attackTarget;
    private List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        attackParticles = GetComponent<ParticleSystem>();
        attackParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTarget != null)
        {
            if (attackParticles.particleCount > 0)
            {
                var particles = new ParticleSystem.Particle[attackParticles.particleCount];
                var count = attackParticles.GetParticles(particles);
                Vector3 enemyPosition = attackTarget.transform.position;
                for (int i = 0; i < count; i++)
                {
                    var particle = particles[i];
                    Vector3 particlePosition = attackParticles.transform.TransformPoint(particle.position);
                    //Vector3 targetPosition = (enemyPosition - particlePosition) * (particle.remainingLifetime / particle.startLifetime);
                    //particle.position = attackParticles.transform.InverseTransformPoint(enemyPosition - targetPosition);

                    Vector3 dir = attackParticles.transform.InverseTransformDirection((enemyPosition - particlePosition)).normalized;
                    particle.velocity = particle.velocity + (dir * particleSpeed * Time.deltaTime);
                    particles[i] = particle;
                }
                attackParticles.SetParticles(particles);
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("colliding " + other);
        if(other == attackTarget.gameObject)
        {
            var count = attackParticles.GetCollisionEvents(other, collisionEvents);
            foreach(ParticleCollisionEvent e in collisionEvents)
            {
                //e.colliderComponent
            }
        }
    }

    public void SetTarget(Enemy target)
    {
        attackTarget = target;
        attackParticles.Play();
    }
}
