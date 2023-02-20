using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCameraManager : Singleton<NpcCameraManager>
{
    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        cam.gameObject.SetActive(false);
    }

    private void Update()
    {
        TalkWithNpc();
    }

    public void TalkWithNpc() // npc랑 상호작용 할 때 카메라 고정
    {
        if (Input.GetKeyDown(KeyCode.Tab)) cam.gameObject.SetActive(true);
        if (Input.GetKeyUp(KeyCode.Tab)) cam.gameObject.SetActive(false);
    }
}