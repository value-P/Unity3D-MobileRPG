using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
   public static string TextSetting(this string target, Color wantColor, params string[] tags)
    {
        //<#ffffff> 내용 </color>
        //일단 아무렇게나 쓸 수 있게 결과물을 미리 준비해둡시다!
        string result = target;

        //Color라고 하는 녀석은 RGBA 순서로 되어있고! 0 ~ 1사이의 값으로 되어있어요!
        int red = Mathf.RoundToInt(wantColor.r * 255);
        int green = Mathf.RoundToInt(wantColor.g * 255);
        int blue = Mathf.RoundToInt(wantColor.b * 255);

        //쌍따옴표처럼 "기능"이 있는 글자를 쓰실 때에는 앞에 \ 를 넣어주세요!
        // \ 는 특수한 기능 문자인데, \' \" \\ 요거는 뒤에 있는 글자를 기능 없이 쓰겠다!
        //                       이거.. 글자로!
        //                             근데 16진수로!
        result = $"<#{red.ToString("X2")}{green.ToString("X2")}{blue.ToString("X2")}>" + result + "</color>";
        //                     16진수 2자리

        //<b> Bold           굵은 글씨 
        //<i> Italic         기울임
        //<u> UnderLine      밑줄
        //<s> Strike Through 취소선
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
