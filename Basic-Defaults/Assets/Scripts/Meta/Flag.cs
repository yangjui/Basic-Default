using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Flag : MonoBehaviour
{
    [SerializeField]
    private Flag[] nextFlags = null;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetScale(float _scale)
    {
        transform.localScale = Vector3.one * _scale;
    }

    private void Update()
    {
        if (nextFlags == null || nextFlags.Length == 0) return;
    }

    public Flag[] GetNextFlags() // 플래그 인스펙터 값 호출
    {
        return nextFlags;
    }

    public bool IsNextEmpty() // 플래그 인스펙터의 넥스트 플래그가 비어있을때
    {
        return nextFlags == null || nextFlags.Length == 0;
    }
}