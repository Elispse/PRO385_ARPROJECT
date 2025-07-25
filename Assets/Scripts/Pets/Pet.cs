using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Pet : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;

    [SerializeField] float maxHunger;
    [SerializeField] float hungerDepletionRate;
    [HideInInspector] public float curHunger;

    [SerializeField] float maxThirst;
    [SerializeField] float thirstDepletionRate;
    [HideInInspector] public float curThirst;

    [SerializeField] float maxExhaustion;
    [SerializeField] float exhaustionDepletionRate;
    [HideInInspector] public float curExhaustion;

    [HideInInspector] public bool sleeping = false;

    [SerializeField] public Animator animator;

    float wanderTimer = 0;

    public float GetMaxHunger() { return maxHunger; }
    public float GetMaxThirst() { return maxThirst; }
    public float GetMaxExhaustion() { return maxExhaustion; }

    private void Start()
    {
        curHunger = maxHunger;
        curThirst = maxThirst;
        curExhaustion = maxExhaustion;

        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    private void Update()
    {
        if (!sleeping)
        {
            curHunger -= Time.deltaTime * hungerDepletionRate;
            curThirst -= Time.deltaTime * thirstDepletionRate;
            curExhaustion -= Time.deltaTime * exhaustionDepletionRate;
            animator.SetFloat("Walking", agent.velocity.magnitude);
            CheckStats();

            if(agent.isStopped)
            {
                wanderTimer -= Time.deltaTime;
                if (wanderTimer <= 0)
                {
                    wanderTimer = Random.Range(5, 10);
                    agent.SetDestination(new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)) + transform.position);
                    agent.isStopped = false;
                }
            }
            else if(agent.remainingDistance <= 0.5f)
            {
                agent.isStopped = true;
            }
        }
        else
        {
            curHunger -= Time.deltaTime * .1f * hungerDepletionRate;
            curThirst -= Time.deltaTime * .1f * thirstDepletionRate;
            curExhaustion += Time.deltaTime * .5f;
        }
    }

    private void CheckStats()
    {
        if(curHunger <= 0) StartCoroutine(Dead());
        if(curThirst <= 0) StartCoroutine(Dead());
        if(curExhaustion <= 0) StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
