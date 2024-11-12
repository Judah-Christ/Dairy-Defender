using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class SodaSlowController : MonoBehaviour
{
    public UpgradeLevel SodaLevel;
    public int UpgradeCost;
    public Sprite toolbarImage;

    public float slowSpeed = 2;
}
