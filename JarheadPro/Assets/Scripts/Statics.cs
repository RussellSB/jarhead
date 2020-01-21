using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static void ResetStatics()
    {
        EffectController.partnerCount = 0;
        EffectController.childCount = 0;
        EffectController.workCount = 0;
        EffectController.existentialCount = 0;

        EffectController.partnerSummoned = false;
        EffectController.childSummoned = false;
        EffectController.existentialSummoned = false;
        EffectController.overtimeSummoned = false;

        EffectController.impactThreshold = 2;

        EffectController.monthlyMoneyEffect.Clear();
        
        Sanity.extra_decay = 0f;
        Sanity.sanity = Sanity.SANITY_MAX;

        Money.money = 21000f;
        Money.moneyOverTime = 0;

        PlayerController.velocity = Vector3.zero;

        IntervalController.causeWorkplacePrompt = false;
        IntervalController.causePartnerPrompt = false;
        IntervalController.causeChildPrompt = false;
        IntervalController.causeOtherPrompt = true;

        IntervalController.intervalCount = 1;
        IntervalController.income = 0;
        IntervalController.expense = 0;

        NetworkController.decided_partner = false;
        NetworkController.decided_child = false;

        PartyController.partyJarbuds = new List<GameObject>();
    }
}