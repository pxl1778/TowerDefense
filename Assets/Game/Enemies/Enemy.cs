using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1.0f;
    [SerializeField]
    protected float health = 3.0f;
    [SerializeField]
    protected float attack = 1.0f;
    protected PathwayNode[] pathwayNodes;
    protected int nodeIndex = 0;
    private float pathAlpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (pathwayNodes != null && pathwayNodes.Length > 0 && nodeIndex < pathwayNodes.Length)
        {
            var distance = Vector3.Distance(pathwayNodes[nodeIndex - 1].transform.position, pathwayNodes[nodeIndex].transform.position);
            pathAlpha += (speed/distance) * Time.deltaTime;
            if (pathAlpha >= 1)
            {
                nodeIndex++;
                pathAlpha = pathAlpha - 1;
                pathAlpha -= speed * Time.deltaTime;
                FollowPath();
                return;
            }
            transform.position = Vector3.Lerp(pathwayNodes[nodeIndex - 1].transform.position, pathwayNodes[nodeIndex].transform.position, pathAlpha);

            //Vector3 direction = (pathwayNodes[nodeIndex].transform.position - this.transform.position).normalized;
            //Vector3 newPos = this.transform.position + direction * speed * Time.deltaTime;
            //Vector3 difference = (pathwayNodes[nodeIndex].transform.position - newPos).normalized;
            //if (difference != direction)
            //{
            //    this.transform.position = pathwayNodes[nodeIndex].transform.position;
            //    nodeIndex++;
            //}
            //else
            //{
            //    this.transform.position = newPos;
            //}

        }
    }

    public void SetData(PathwayNode[] nodes)
    {
        if(nodes != null && nodes.Length > 0)
        {
            pathwayNodes = nodes;
            this.transform.position = nodes[0].transform.position;
            this.transform.rotation = nodes[0].transform.rotation;
            nodeIndex = 1;
            pathAlpha = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
