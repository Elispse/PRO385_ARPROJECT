using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    private NavMeshAgent agent;

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
            curExhaustion += Time.deltaTime * .5f;
        }
    }

    private void CheckStats()
    {
        if(curHunger <= 0) Destroy(gameObject);
        if(curThirst <= 0) Destroy(gameObject);
        if(curExhaustion <= 0) Destroy(gameObject);
    }
}
