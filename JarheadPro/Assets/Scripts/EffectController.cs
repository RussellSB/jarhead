using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatEffect {
    public float moneyPerMonth;
    public float sanityDecay;
    public float moneyInstant;
    public float sanityInstant;

    public int partnerImpact;
    public int childImpact;
    public int existentialImpact;
    public int workImpact;

    public StatEffect(float moneyPerMonth = 0, float sanityDecay = 0, float moneyInstant = 0, float sanityInstant = 0, 
                        int partnerImpact = 0, int childImpact = 0, int workImpact = 0, int existentialImpact = 0)
    {
        this.moneyPerMonth = moneyPerMonth;
        this.sanityDecay = sanityDecay;
        this.moneyInstant = moneyInstant;
        this.sanityInstant = sanityInstant;

        this.partnerImpact = partnerImpact;
        this.childImpact = childImpact;
        this.workImpact = workImpact;
        this.existentialImpact = existentialImpact;
    }
}

enum EffectType { Stat, Summon, Multi }

public class EffectController : MonoBehaviour
{
    public static int partnerCount = 0;
    public static int childCount = 0;
    public static int workCount = 0;
    public static int existentialCount = 0;

    public static bool partnerSummoned = false;
    public static bool childSummoned = false;
    public static bool existentialSummoned = false;
    public static bool overtimeSummoned = false;

    public static int impactThreshold = 2;

    public static Dictionary<string, StatEffect> StatEffects = new Dictionary<string, StatEffect>() {
        // Job Effects
        { "JobProgrammer",  new StatEffect(moneyPerMonth:    200, sanityDecay:      -0.20f) },
        { "JobWaiter",      new StatEffect(moneyPerMonth:     50, sanityDecay:      -0.50f) },
        { "JobLawyer",      new StatEffect(moneyPerMonth:    200, sanityDecay:      -2.00f) },
        { "JobCashier",     new StatEffect(moneyPerMonth:     50, sanityDecay:      -0.20f) },
        { "JobDelivery1",   new StatEffect(moneyPerMonth:     50, sanityDecay:      -0.50f) },
        { "JobDelivery2",   new StatEffect(moneyPerMonth:    200, sanityDecay:      -2.00f) },
        { "JobConsultant",  new StatEffect(moneyPerMonth:     75, sanityDecay:      -1.00f) },

        // Housing Effects                                                        
        { "HousingRental",  new StatEffect(moneyPerMonth:   -100, sanityDecay:      -1.00f) },
        { "HousingReal",    new StatEffect(moneyInstant:    -500, sanityInstant:   -20.00f) },

        // Jarhead Effects
        { "JarheadChild",  new StatEffect(moneyPerMonth:    -50, sanityDecay:      1.00f) },
        { "JarheadPartner",  new StatEffect(moneyPerMonth:  -20, sanityDecay:      2.00f) },

        // Decision Effects
        { "CHOICE1ID_PLACEHOLDER",    new StatEffect(moneyInstant:   0, sanityInstant:   +30.00f) },
        { "CHOICE2ID_PLACEHOLDER",    new StatEffect(moneyInstant:   0, sanityInstant:   -30.00f) },
        { "ProLover",        new StatEffect(partnerImpact:   1, sanityInstant:   +20.00f) },
        { "AntiLover",       new StatEffect(partnerImpact:   -1, sanityInstant:   -20.00f) },
        { "ProParenting",           new StatEffect(childImpact:   1, sanityInstant:   +20.00f) },
        { "AntiParenting",          new StatEffect(childImpact:   -1, sanityInstant:   -20.00f) },
        { "ProExistential",         new StatEffect(existentialImpact:   1, sanityInstant:   -40.00f) },
        { "ProWork",                new StatEffect(workImpact:   1,     moneyInstant:   50, sanityInstant:   -20.00f) },
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
        string jarname = refname.Substring(7, refname.Length - 7);
        Debug.Log("JARNAME==" + jarname);
        IntervalController.SummonJarbud(jarname);
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

        // Add the monthly money effect to the dictionary, if the effect has a money per month.
        if (effect.moneyPerMonth != 0f)
        {
            monthlyMoneyEffect.Add(refname, effect.moneyPerMonth);
        }

        // Add the impact to child and partner respectively
        partnerCount += effect.partnerImpact;
        childCount += effect.childImpact;
        workCount += effect.workImpact;
        existentialCount += effect.existentialImpact;

        // Check if summoning based on counts is triggered
        if (!partnerSummoned)
        {
            if (partnerCount >= impactThreshold)
            {
                partnerSummoned = true;
                addEffect("Summon-EffectHealthy");
            }
            if (partnerCount <= -impactThreshold)
            {
                partnerSummoned = true;
                addEffect("Summon-EffectToxic");
            }
        }

        if (!childSummoned)
        {
            if (childCount >= impactThreshold)
            {
                childSummoned = true;
                addEffect("Summon-EffectLoved");
            }
            if (childCount <= -impactThreshold)
            {
                childSummoned = true;
                addEffect("Summon-EffectNeglected");
            }
        }

        if (!overtimeSummoned)
        {
            if (workCount >= impactThreshold)
            {
                overtimeSummoned = true;
                addEffect("Summon-EffectOvertime");
            }
        }

        if (!existentialSummoned)
        {
            if (existentialCount >= impactThreshold)
            {
                existentialSummoned = true;
                addEffect("Summon-EffectExistentialism");
            }
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
        //Debug.Log(Money.money);
        List<float> moneyFX = monthlyMoneyEffect.Values.ToList();
        foreach (var money in moneyFX)
        {
            Money.money = Money.money + money;
        }
    }
}
