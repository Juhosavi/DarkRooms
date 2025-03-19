using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchDuration; //aika kauanko vihu etsii
    public float searchTurnSpeed; //kuinka nopee vihu kääntyy
    public float sightRange; //näkösäteen pituus
    //public Transform[] waypoints; //taulukko waypointeille joita pitkin vihu kulkee
    public Transform eye; //silmä
    public MeshRenderer indicator; //pallo vihollisen päällä
    public Vector3 lastKnownPlayerPosition;//piste jossa pelaaja on kun katoaa nurkan taa

    public PlayerHealth playerHealth;
    public Collider enemysCollider;
    public float attackTimer = 0f;

    public Transform chaseTarget;

    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public TrackingState trackingState;
    [HideInInspector]
    public AttackState attackState;


    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public SphereCollider col;
    // public MouseLook_S mouselook_s;
    [SerializeField]
    private float colliderRadius;//enemyn sphere colliderin säteen pituus


    public float rangeOfPatrolling;
    public Transform centerOfPatrolArea, miniPatrolCenter;
    public List<Transform> PatrolAreaCenters;

    public float radius, flashlightRadius;
    private float flashOffradius;
    [Range(0,360)]
    public float angle;
    public float previousAngle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public Animator anim;

    public bool canSeePlayer;
    public AudioSource laugh;
    public AudioSource hit;


    private void Awake()
    {
        //luodaan uudet objektit luokista
        patrolState = new PatrolState(this);//oma objektinsa
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);//muuttujat jotka saatavilla muista luokista pitää olla public
        trackingState = new TrackingState(this);
        attackState = new AttackState(this);

    }

    void Start()
    {
        enemysCollider = this.gameObject.GetComponent<BoxCollider>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = gameObject.GetComponentInChildren<Animator>();
        currentState = patrolState; //kun peli alkaa kerrotaan viholliselle että tila on patrol state.

        // mouselook_s = GameObject.FindWithTag("MainCamera").GetComponent<MouseLook_S>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        PatrolAreaCenters.Add(centerOfPatrolArea);
        StartCoroutine(FOVRoutine());

        previousAngle = angle;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);//ei tsekkaa ihan joka frame

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)//jos collider array ei ole tyhjä (eli k.o. layerilla on osuttu johonkin) niin..
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                ////lähetetään ray targetiin kun target on spherellä määritetyssä kulmassa, jos ray ei osu mihinkään mikä on obstruction -layerilla niin nähdään pelaaja
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    // Debug.Log("Pelaaja näkyy");
                    lastKnownPlayerPosition = target.position;
                }
                else
                {
                    canSeePlayer = false;//ei nähdä pelaajaa jos ray osuu esteeseen
                }
            }
            else
                canSeePlayer = false;//ei nähdä pelaajaa jos ei ole kulman sisällä
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    void Update()
    {
        currentState.UpdateState();

        // if (mouselook_s.flashlightOn)
        // {

        //     angle = 360;
        // }
        // else if (!mouselook_s.flashlightOn)
        // {
        //     angle = previousAngle;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnCollisionEnter(Collision other) 
    {
        currentState.OnCollisionEnter(other);
    }

    


}
