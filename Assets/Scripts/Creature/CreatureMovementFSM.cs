using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatureMovementFSM : MonoBehaviour
{
    StateFSM state;
    [Header("Creature View")]
    public float angleDetection;
    public float rayDistance;
    public int rayAmount;
    public LayerMask objectMask;
    [Header("Seek Range")]
    public float seekRange;
    [Header("Stats")]
    public float creatureSpeed;

    private Collider[] colSeek;
    public List<Ray> rays = new List<Ray>();
    void Awake()
    {
        colSeek = Physics.OverlapSphere(transform.position,seekRange);
        SetState(new CreaturePatrolState(this));
    }
    void FixedUpdate()
    { 
        state?.Update();
    }
    public void SetState(StateFSM state) {
        state?.Exit();
        this.state = state;
        state?.Enter();
    }
}
