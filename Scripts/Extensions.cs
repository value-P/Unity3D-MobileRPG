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
            case PartnerType.Defense: return "��Ŀ".TextSetting(Color.blue);
            case PartnerType.Attack: return"����".TextSetting(Color.red);
            case PartnerType.Support: return "������".TextSetting(Color.green);
            default: return "����";
        }
    }

    public static string AttackTypeToText(this AttackType target)
    {
        switch (target)
        {
            case AttackType.Short: return "��������".TextSetting(Color.gray);
            case AttackType.Long: return "���Ÿ�����".TextSetting(Color.yellow);
            default: return "����";
        }
    }
}
