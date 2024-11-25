using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference Jump { get; private set; }
    //[field: SerializeField] public EventReference Fall { get; private set; }
    [field: SerializeField] public EventReference fallThud { get; private set; }


    [field: Header("Enemy SFX")]
    [field: SerializeField] public EventReference ratSqueaks { get; private set; }
    [field: SerializeField] public EventReference flyBuzzing { get; private set; }
    [field: SerializeField] public EventReference ratDeathScreams { get; private set; }
    [field: SerializeField] public EventReference enemyHit { get; private set; }
    [field: SerializeField] public EventReference flyDie { get; private set; }



    [field: Header("Tower SFX")]
    [field: SerializeField] public EventReference sodaTower { get; private set; }
    [field: SerializeField] public EventReference towerPlace { get; private set; }
    [field: SerializeField] public EventReference towerShoot { get; private set; }
    [field: SerializeField] public EventReference dismantle { get; private set; }
    [field: SerializeField] public EventReference upgrade { get; private set; }



    [field: Header("Music")]
    [field: SerializeField] public EventReference Music { get; private set; }
    public static FMODEvents instance { get; private set; }


    [field: Header("General")]
    [field: SerializeField] public EventReference pauseMenuOpen { get; private set; }
    [field: SerializeField] public EventReference errorFeedback { get; private set; }
    [field: SerializeField] public EventReference roundWin { get; private set; }
    [field: SerializeField] public EventReference buttonDrop { get; private set; }
    [field: SerializeField] public EventReference buttonPickUp { get; private set; }
    [field: SerializeField] public EventReference Fail { get; private set; }
    [field: SerializeField] public EventReference Win { get; private set; }
    [field: SerializeField] public EventReference waveStart { get; private set; }



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
