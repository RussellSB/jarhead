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

        Money.money = 1000;
        Money.moneyOverTime = 0;

        PlayerController.velocity = Vector3.zero;
    }
}