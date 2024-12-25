using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
   public Transform target;
    public float maxSpeed = 5;
    private float currentSpeed;
    public float attackRate;
    private float lastAttackTime;
    public float chaseRadius;
    public float attackRadius;
    public float viewAngle =180;

    public Transform[] waypoint;
    public Transform stayPoint;
    private int currentWaypointIndex;
    private float waitTime = 3f;
    private float waitCounter = 0f;
    private bool waiting = false;
    private bool canPatrol = true;

    private Animator anim;
    

    
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = maxSpeed ;
        //Target player 
        target = GameObject.FindObjectOfType<PlayeeController>().transform;
        //target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed > 0)
        {

            anim.SetBool("Walk", true);
        }
        else 
        {
            anim.SetBool("Walk", false);
        }
        if (canPatrol)
        {
            Patrol();
        }
         
        Vector3 dirToPlayer = (target.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
        {
            
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
               
                transform.LookAt(target.position);
                canPatrol = false;
                if (Vector3.Distance(target.position, transform.position) >= attackRadius)
                {
                    if (Time.time - lastAttackTime > attackRate)
                    {
                        lastAttackTime = Time.time;
                    }
                    canPatrol = true;
                   
                }
            }
        }
        else
        {
            canPatrol = true;
        }
    }

    public void Patrol()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
                return;
            waiting = false;
        }
        Transform wp = waypoint[currentWaypointIndex];
        
       
            if (Vector3.Distance(transform.position, wp.position) < 0.01F)
            {
                transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoint.Length;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, currentSpeed * Time.deltaTime);
                transform.LookAt(wp.position);
            }
        
        
    }
}

