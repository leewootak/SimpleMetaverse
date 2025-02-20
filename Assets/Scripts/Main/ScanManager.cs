using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isScan;
    private bool isNPC;      // 스캔 대상이 NPC인지 여부
    private bool canConfirm; // 스캔 활성 직후는 확인 입력을 무시하기 위한 플래그


    private void Update()
    {
        if (isScan)
        {
            // 스페이스바가 눌리지 않은 상태(릴리즈)를 확인한 후에야 재입력 허용
            if (!canConfirm && !Input.GetButton("Jump"))
            {
                canConfirm = true;
            }
            // 확인 가능 상태에서 스페이스바를 누르면
            else if (canConfirm && Input.GetButtonDown("Jump"))
            {
                if (isNPC)
                {
                    // NPC인 경우 두 번째 누름에서 씬 전환
                    SceneManager.LoadScene("MiniGameScene");
                }
                else
                {
                    // NPC가 아니면 스캔 상태 해제 및 대화창 닫기
                    isScan = false;
                    talkPanel.SetActive(false);
                }
            }
        }
    }
    public void Scan(GameObject scanObj)
    {
        if (isScan) return;  // 이미 스캔 중이면 무시

        isScan = true;
        canConfirm = false;  // 스캔 시작 시에는 즉시 확인하지 않음
        scanObject = scanObj;

        int objLayer = scanObj.layer;
        if (objLayer == 6) // NPC 레이어 번호 (예: 6)
        {
            talkText.text = "어서와! 지금 바로 미니게임으로 이동 시켜줄게";
            isNPC = true;
        }
        else
        {
            talkText.text = "이것은 " + scanObj.name + "입니다.";
            isNPC = false;
        }
        talkPanel.SetActive(true);
    }
}
