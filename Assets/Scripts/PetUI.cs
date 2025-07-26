using UnityEngine;
using UnityEngine.UIElements;

public class PetUI : MonoBehaviour
{
    VisualElement root;
    ProgressBar hungerBar;
    ProgressBar thirstBar;
    ProgressBar exhaustionBar;

    Button foodBtn;
    Button waterBtn;
    Button sleepBtn;

    public Pet curPet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        hungerBar = root.Q<ProgressBar>("HungerBar");
        thirstBar = root.Q<ProgressBar>("ThirstBar");
        exhaustionBar = root.Q<ProgressBar>("ExhaustionBar");

        hungerBar.highValue = curPet.GetMaxHunger();
        thirstBar.highValue = curPet.GetMaxThirst();
        exhaustionBar.highValue = curPet.GetMaxExhaustion();

        foodBtn = root.Q<Button>("FoodBtn");
        waterBtn = root.Q<Button>("WaterBtn");
        sleepBtn = root.Q<Button>("SleepBtn");

        foodBtn.clicked += FoodBtnClicked;
        waterBtn.clicked += WaterBtnClicked;
        sleepBtn.clicked += SleepBtnClicked;
    }

    private void Update()
    {
        hungerBar.value = curPet.curHunger;
        thirstBar.value = curPet.curThirst;
        exhaustionBar.value = curPet.curExhaustion;
    }

    void FoodBtnClicked()
    {
        if (!curPet.sleeping)
        {
            curPet.curHunger += Mathf.Ceil(curPet.GetMaxHunger() * .25f);
            if (curPet.curHunger > curPet.GetMaxHunger()) curPet.curHunger = curPet.GetMaxHunger();
        }
        curPet.agent.isStopped = true;
        curPet.animator.SetTrigger("Eat");
    }

    void WaterBtnClicked()
    {
        if (!curPet.sleeping)
        {
            curPet.curThirst += Mathf.Ceil(curPet.GetMaxThirst() * .25f);
            if (curPet.curThirst > curPet.GetMaxThirst()) curPet.curThirst = curPet.GetMaxThirst();
        }
        curPet.agent.isStopped = true;
        curPet.animator.SetTrigger("Eat");
    }

    void SleepBtnClicked()
    {
        curPet.sleeping = (!curPet.sleeping) ? true : false;
        curPet.agent.isStopped = true;
        if (curPet.sleeping)
        {
            curPet.animator.SetBool("Sleep", true);
        }
        else
        {
            curPet.animator.SetBool("Sleep", false);
        }
    }
}