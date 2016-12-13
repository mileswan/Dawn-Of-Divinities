using UnityEngine;
using System.Collections;

public class MoveToSample : MonoBehaviour
{	
	void Start(){
        //��ֵ�Զ�����ʽ����iTween���õ��Ĳ���
        Hashtable args = new Hashtable();

        //�������������ͣ�iTween�������ֺܶ��֣���Դ���е�ö��EaseType��
        //�����ƶ�����Ч���������ƶ����Ⱥ������ƶ����ȼ����ڱ��١��ȵ�
        args.Add("easeType", iTween.EaseType.easeInOutExpo);

        //�ƶ����ٶȣ�
        args.Add("speed", 1f);
        //�ƶ�������ʱ�䡣�����speed������ô����speed
        args.Add("time", 1f);
        //����Ǵ�����ɫ�ġ����Կ�Դ����Ǹ�ö�١�
        //args.Add("NamedValueColor", "_SpecColor");
        //�ӳ�ִ��ʱ��
        args.Add("delay", 0.1f);
        //����ѭ������ none loop pingPong (һ�� ѭ�� ����)	
        args.Add("loopType", "none");
        //args.Add("loopType", "loop");	
        //args.Add("loopType", "pingPong");

        // x y z ��ʾ�ƶ���λ�á�
        args.Add("x", 2.8);
        args.Add("y", 3);
        args.Add("z", 0);

        //iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        iTween.MoveTo(gameObject, args);
    }
}

