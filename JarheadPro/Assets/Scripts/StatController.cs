using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatEffect {
    public float moneyPerMonth;
    public float sanityDecay;
    public float moneyInstant;
    public float sanityInstant;

    public StatEffect(float moneyPerMonth = 0, float sanityDecay = 0, float moneyInstant = 0, float sanityInstant = 0){
        this.moneyPerMonth = moneyPerMonth;
        this.sanityDecay = sanityDecay;
        this.moneyInstant = moneyInstant;
        this.sanityInstant = sanityInstant;
    }
}

public class StatController : MonoBehaviour
{

    public static Dictionary<string, StatEffect> Effects = new Dictionary<string, StatEffect>() {
        // Job Effects
        { "JobProgrammer",  new StatEffect(moneyPerMonth:    100, sanityDecay:      -0.20f) },
        { "JobWaiter",      new StatEffect(moneyPerMonth:     50, sanityDecay:      -0.50f) },
        { "JobLawyer",      new StatEffect(moneyPerMonth:    200, sanityDecay:      -2.00f) },
        { "JobCashier",     new StatEffect(moneyPerMonth:     50, sanityDecay:      -0.20f) },
        { "JobDelivery1",   new StatEffect(moneyPerMonth:     50, sanityDecay:      -0.50f) },
        { "JobDelivery2",   new StatEffect(moneyPerMonth:    200, sanityDecay:      -2.00f) },
        { "JobConsultant",  new StatEffect(moneyPerMonth:     75, sanityDecay:      -1.00f) },
        // Housing Effects                                                        
        { "HousingRental",  new StatEffect(moneyPerMonth:   -100, sanityDecay:      -1.00f) },
        { "HousingReal",    new StatEffect(moneyInstant:   -2000, sanityInstant:   -20.00f) },
    };

    public static Dictionary<string, float> monthlyMoneyEffect = new Dictionary<string, float>() { };

    public static void addEffect(string refname)
    {
        StatEffect effect = Effects[refname];
        Money.money += effect.moneyInstant;
        Sanity.Update(effect.sanityInstant);
        Sanity.UpdateDecay(effect.sanityDecay);

        // Add the monthly money effect to the dictionary, if the effect has a money per month.
        if(effect.moneyPerMonth == 0f)
        {
            monthlyMoneyEffect.Add(refname, effect.moneyPerMonth);
        }
    }

    public static void removeEffect(string refname)
    {
        StatEffect effect = Effects[refname];
        // Removes the monthly money effect from the dictionary, if the effect has a money per month.
        if (effect.moneyPerMonth == 0f)
        {
            monthlyMoneyEffect.Remove(refname);
        }
    }

    public static void updateMoneyMonthly()
    {
        List<float> moneyFX = monthlyMoneyEffect.Values.ToList();
        foreach (var money in moneyFX)
        {
            Money.money -= money;
        }
    }
}
