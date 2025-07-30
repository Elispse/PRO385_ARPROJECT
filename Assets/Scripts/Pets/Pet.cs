using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    }

    private void Update()
    {
        if (!sleeping)
        {
            curHunger -= Time.deltaTime * hungerDepletionRate;
            curThirst -= Time.deltaTime * thirstDepletionRate;
            curExhaustion -= Time.deltaTime * exhaustionDepletionRate;
            CheckStats();
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
