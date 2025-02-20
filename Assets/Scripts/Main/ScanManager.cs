using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isScan;
    private bool isNPC;      // ��ĵ ����� NPC���� ����
    private bool canConfirm; // ��ĵ Ȱ�� ���Ĵ� Ȯ�� �Է��� �����ϱ� ���� �÷���


    private void Update()
    {
        if (isScan)
        {
            // �����̽��ٰ� ������ ���� ����(������)�� Ȯ���� �Ŀ��� ���Է� ���
            if (!canConfirm && !Input.GetButton("Jump"))
            {
                canConfirm = true;
            }
            // Ȯ�� ���� ���¿��� �����̽��ٸ� ������
            else if (canConfirm && Input.GetButtonDown("Jump"))
            {
                if (isNPC)
                {
                    // NPC�� ��� �� ��° �������� �� ��ȯ
                    SceneManager.LoadScene("MiniGameScene");
                }
                else
                {
                    // NPC�� �ƴϸ� ��ĵ ���� ���� �� ��ȭâ �ݱ�
                    isScan = false;
                    talkPanel.SetActive(false);
                }
            }
        }
    }
    public void Scan(GameObject scanObj)
    {
        if (isScan) return;  // �̹� ��ĵ ���̸� ����

        isScan = true;
        canConfirm = false;  // ��ĵ ���� �ÿ��� ��� Ȯ������ ����
        scanObject = scanObj;

        int objLayer = scanObj.layer;
        if (objLayer == 6) // NPC ���̾� ��ȣ (��: 6)
        {
            talkText.text = "���! ���� �ٷ� �̴ϰ������� �̵� �����ٰ�";
            isNPC = true;
        }
        else
        {
            talkText.text = "�̰��� " + scanObj.name + "�Դϴ�.";
            isNPC = false;
        }
        talkPanel.SetActive(true);
    }
}
