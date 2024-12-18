using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
   public Transform target;
    public float maxSpeed;
    private float currentSpeed;
    private Rigidbody rb;
    public float attackRate;
    private float lastAttackTime;
    public float chaseRadius;
    public float attackRadius;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = maxSpeed= 5;
        //get component
        rb = GetComponent<Rigidbody>();
        //Target player 
        target = GameObject.FindObjectOfType<PlayeeController>().transform;
        //target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
            rb.MovePosition(temp);
            
            if (Vector3.Distance(target.position, transform.position) <= attackRadius)
            { 
                if (Time.time - lastAttackTime > attackRate)
                {
                    lastAttackTime = Time.time;
                }
            }
        }
       

    }
}

