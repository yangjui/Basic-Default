using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    [SerializeField]
    private Flag[] flagPath = null;

    private NavMeshAgent agent = null;
    private int curIdx = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (flagPath == null ||
            flagPath.Length == 0) return;

        agent.SetDestination(flagPath[curIdx].GetPosition());
    }

    private void Update()
    {
        if (curIdx == flagPath.Length - 1) return; // 마지막 플래그에 도달하면 return

        if (IsMoving())
        {
            if (DistanceToDestination() < 0.5f)
            {
                ++curIdx;
                if (curIdx == flagPath.Length)     // 이 부분 존재 : 무한 루프 도는 것처럼 반복
                    curIdx = 0;               // 플래그를 비활성화해도 눈에만 안 보일 뿐 목적지는 세팅되어 있음 --> 무한 루프
                agent.SetDestination(
                    flagPath[curIdx].GetPosition());
            }
        }
    }

    private bool IsMoving()
    {
        return agent.velocity != Vector3.zero;
    }

    private float DistanceToDestination()
    {
        Vector3 playerPos = transform.position;
        playerPos.y = 0f;
        Vector3 destPos = agent.destination;
        destPos.y = 0f;
        return Vector3.Distance(playerPos, destPos);
    }
}
