using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class swatAI : MonoBehaviour
{
    private Transform S_tr;
    private Transform P_tr;
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider CapCol;

    private WaitForSeconds Ws;
    private swatMoveAgent moveAgent;

    private readonly string P_tag = "Player";
    private readonly string hashIsMove = "IsMove";
    private readonly string hashMoveSpeed = "MoveSpeed";

    private float ATK_dist = 5.0f;
    private float TRA_dist = 10.0f;
    public bool isDie = false;

    public enum S_State
    {
        PATOL = 0, TRACE, ATTACK, DIE
    }
    public S_State state = S_State.PATOL;

    void Awake()
    {
        var PlayerObj = GameObject.FindWithTag(P_tag);
        if (PlayerObj != null)
            P_tr = PlayerObj.transform;
        S_tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CapCol = GetComponent<CapsuleCollider>();

        Ws = new WaitForSeconds(0.3f);

        moveAgent = GetComponent<swatMoveAgent>();
    }
    private void OnEnable()
    {
        StartCoroutine(StateCheck());
        StartCoroutine(PlayAction());
    }

    IEnumerator StateCheck()
    {
        while (!isDie)
        {
            if(state == S_State.DIE) yield break;
            float dist = (P_tr.position - S_tr.position).magnitude;
            if (dist <= ATK_dist)
                state = S_State.ATTACK;
            else if (dist <= TRA_dist)
                state = S_State.TRACE;
            else
                state = S_State.PATOL;

            yield return Ws;
        }
    }
    IEnumerator PlayAction()
    {
        while(!isDie)
        {
            yield return Ws;

            switch(state)
            {
                case S_State.PATOL:
                    animator.SetBool(hashIsMove, true);
                    moveAgent._Patoling = true;
                    break;

                case S_State.ATTACK:
                    animator.SetBool(hashIsMove, false);
                    moveAgent.Stop();
                    break;

                case S_State.TRACE:
                    animator.SetBool(hashIsMove, true);
                    moveAgent._traceTarget = P_tr.position;
                    break;

                case S_State.DIE:
                    isDie = true;
                    moveAgent.Stop();
                    break;
            }
        }

    }
    void Update()
    {
        animator.SetFloat(hashMoveSpeed, moveAgent.Speed);
    }
}
