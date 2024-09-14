using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
   public static string TextSetting(this string target, Color wantColor, params string[] tags)
    {
        string result = target;

        int red = Mathf.RoundToInt(wantColor.r * 255);
        int green = Mathf.RoundToInt(wantColor.g * 255);
        int blue = Mathf.RoundToInt(wantColor.b * 255);

        result = $"<#{red.ToString("X2")}{green.ToString("X2")}{blue.ToString("X2")}>" + result + "</color>";
        foreach (var current in tags)
        {
            result = $"<{current}>" + result + $"</{current}>";
        };

        return result;
    }

    public static string PartnerTypeToText(this PartnerType target)
    {
        switch (target)
        {
            case PartnerType.Defense: return "탱커".TextSetting(Color.blue);
            case PartnerType.Attack: return"딜러".TextSetting(Color.red);
            case PartnerType.Support: return "서포터".TextSetting(Color.green);
            default: return "오류";
        }
    }

    public static string AttackTypeToText(this AttackType target)
    {
        switch (target)
        {
            case AttackType.Short: return "근접공격".TextSetting(Color.gray);
            case AttackType.Long: return "원거리공격".TextSetting(Color.yellow);
            default: return "오류";
        }
    }
}
