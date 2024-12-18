using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class BowInteraction : MonoBehaviour
{
    public Transform player;               // �÷��̾��� Transform (Ȱ�� ���� ���)
    public Transform attachPoint;          // Ȱ�� ���� ��ġ
    public XRNode controllerNode = XRNode.RightHand; // ����� ��Ʈ�ѷ� (������ ��Ʈ�ѷ�)
    private InputDevice controller;        // ��Ʈ�ѷ� ��ü
    private bool isAttached = false;       // Ȱ�� ����ڿ��� �پ����� ����

    void Start()
    {
        // ��Ʈ�ѷ� �ʱ�ȭ
        InitializeController();
    }

    void Update()
    {
        if (!controller.isValid)
        {
            // ��Ʈ�ѷ��� ��ȿ���� �ʴٸ� �ٽ� �ʱ�ȭ
            InitializeController();
        }

        // Trigger �� �б�
        if (controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            if (!isAttached)
            {
                AttachBow();
            }
        }
    }

    void InitializeController()
    {
        // ������ XRNode�� �ش��ϴ� ��Ʈ�ѷ��� ã��
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (!controller.isValid)
        {
            Debug.LogWarning("Right hand controller not found.");
        }
        else
        {
            Debug.Log("Controller initialized: " + controller.name);
        }
    }

    void AttachBow()
    {
        // Ȱ ��ġ�� ����ڿ��� ����
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        isAttached = true;

        // Console �޽��� ���
        Debug.Log("Ȱ�� �����߽��ϴ�.");
    }
}
