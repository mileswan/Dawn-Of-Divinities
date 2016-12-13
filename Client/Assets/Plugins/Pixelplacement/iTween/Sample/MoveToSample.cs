using UnityEngine;
using System.Collections;

public class MoveToSample : MonoBehaviour
{	
	void Start(){
        //键值对儿的形式保存iTween所用到的参数
        Hashtable args = new Hashtable();

        //这里是设置类型，iTween的类型又很多种，在源码中的枚举EaseType中
        //例如移动的特效，先震动在移动、先后退在移动、先加速在变速、等等
        args.Add("easeType", iTween.EaseType.easeInOutExpo);

        //移动的速度，
        args.Add("speed", 1f);
        //移动的整体时间。如果与speed共存那么优先speed
        args.Add("time", 1f);
        //这个是处理颜色的。可以看源码的那个枚举。
        //args.Add("NamedValueColor", "_SpecColor");
        //延迟执行时间
        args.Add("delay", 0.1f);
        //三个循环类型 none loop pingPong (一般 循环 来回)	
        args.Add("loopType", "none");
        //args.Add("loopType", "loop");	
        //args.Add("loopType", "pingPong");

        // x y z 标示移动的位置。
        args.Add("x", 2.8);
        args.Add("y", 3);
        args.Add("z", 0);

        //iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        iTween.MoveTo(gameObject, args);
    }
}

