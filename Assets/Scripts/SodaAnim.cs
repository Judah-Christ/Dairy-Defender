using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaAnim : MonoBehaviour
{
    private Animator sodaAnim;
    public UpgradeTag UpgradeLevel;
    void Start()
    {
        sodaAnim = GetComponentInParent<Animator>();
        SetSodaAnim();
    }

    public void SetSodaAnim()
    {
        switch (UpgradeLevel)
        {
            case UpgradeTag.Soda:
                sodaAnim.SetInteger("Level", 1);
                return;
            case UpgradeTag.Lemonade:
                sodaAnim.SetInteger("Level", 2);
                return;
            case UpgradeTag.Tea:
                sodaAnim.SetInteger("Level", 3);
                return;
            default:
                break;
        }
    }

    public enum UpgradeTag
    {
        None,
        Soda,
        Lemonade,
        Tea
    }
}
