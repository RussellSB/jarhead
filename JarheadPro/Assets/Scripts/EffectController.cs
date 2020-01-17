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

enum EffectType { Stat, Summon, Multi }

public class EffectController : MonoBehaviour
{

    public static Dictionary<string, StatEffect> StatEffects = new Dictionary<string, StatEffect>() {
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
        // Decision Effects
        { "CHOICE1ID_PLACEHOLDER",    new StatEffect(moneyInstant:   200, sanityInstant:   +20.00f) },
        { "CHOICE2ID_PLACEHOLDER",    new StatEffect(moneyInstant:   200, sanityInstant:   +2.00f) }
    };

    public static Dictionary<string, float> monthlyMoneyEffect = new Dictionary<string, float>() { };

    // A function used by multiple other scripts to add another active effect.
    // This effect is generic over multiple possibilities, be it a Stat effect, or a summon, etc.
    // The refname in this case must exist within the Dictionary. If it doesn't an error will be logged.
    public static void addEffect(string refname)
    {
        switch (parseEffectType(refname))
        {
            case EffectType.Multi:
                var effects = refname.Split('&');
                foreach(string effect in effects)
                {
                    addEffect(effect);
                }
                break;
            case EffectType.Stat:
                AddStatEffect(refname); break;
            case EffectType.Summon:
                SummonJarbud(refname); break;
        }
    }

    private static EffectType parseEffectType(string refname)
    {
        if (refname.IndexOf('&') != -1)
        {
            return EffectType.Multi;
        } 
        else if (refname.StartsWith("Summon-"))
        {
            return EffectType.Summon;
        } 
        else
        {
            return EffectType.Stat;
        }
    }

    private static void SummonJarbud(string refname)
    {
        string jarname = refname.Substring(8, refname.Length - 8);
        // PartyController.SummonJarbud(jarname) // INCOMPLETE
    }

    private static void AddStatEffect(string refname)
    {
        if (!StatEffects.ContainsKey(refname))
        {
            Debug.LogError("Effect with the name of \"" + refname + "\" does not exist.");
            return;
        }

        StatEffect effect = StatEffects[refname];
        Money.money += effect.moneyInstant;
        Sanity.Update(effect.sanityInstant);
        Sanity.UpdateDecay(effect.sanityDecay);

        //Debug.Log("MONEYPERMONTH0-" + effect.moneyPerMonth);

        // Add the monthly money effect to the dictionary, if the effect has a money per month.
        if (effect.moneyPerMonth != 0f)
        {
            //Debug.Log("MONEYPERMONTH1-" + effect.moneyPerMonth);
            monthlyMoneyEffect.Add(refname, effect.moneyPerMonth);
        }
    }

    // Removes an active effect using its ID.
    public static void removeEffect(string refname)
    {
        if (!StatEffects.ContainsKey(refname))
        {
            Debug.LogError("Effect with the name of \"" + refname + "\" does not exist.");
            return;
        } else if(!monthlyMoneyEffect.ContainsKey(refname))
        {
            Debug.LogError("Effect with the name of \"" + refname + "\" is not active. Couldn't remove.");
        }

        StatEffect effect = StatEffects[refname];
        // Removes the monthly money effect from the dictionary, if the effect has a money per month.
        if (effect.moneyPerMonth == 0f)
        {
            monthlyMoneyEffect.Remove(refname);
        }
    }

    public static void updateMoneyMonthly()
    {
        Debug.Log(Money.money);
        List<float> moneyFX = monthlyMoneyEffect.Values.ToList();
        foreach (var money in moneyFX)
        {
            Money.money += money;
        }
    }
}
