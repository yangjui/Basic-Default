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

    public Flag[] GetNextFlags() // �÷��� �ν����� �� ȣ��
    {
        return nextFlags;
    }

    public bool IsNextEmpty() // �÷��� �ν������� �ؽ�Ʈ �÷��װ� ���������
    {
        return nextFlags == null || nextFlags.Length == 0;
    }
}