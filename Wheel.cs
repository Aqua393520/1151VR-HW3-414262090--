using UnityEngine;
// 如果你使用的是新版 Input System，保留這一行；若是舊版可以刪除
using UnityEngine.InputSystem; 

public class Wheel : MonoBehaviour
{
    private float currentSpeed = 0f;
    private float maxSpeed = 1000f; // 最高旋轉速度
    private float acceleration = 500f; // 加速度
    private float deceleration = 300f; // 減速阻力
    private bool isSpinning = false;
    private bool isStopping = false;

    private AudioSource audioSource; // 宣告音效元件

    void Start()
    {
        // 自動取得掛載在同一個物件上的 AudioSource 元件
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 檢查新版 Input System 的空白鍵是否被按下
        bool spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;

        if (spacePressed && !isSpinning)
        {
            isSpinning = true;
            isStopping = false;

            // 當輪盤開始旋轉時播放音效
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }

        // 加速階段
        if (isSpinning && !isStopping)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed >= maxSpeed)
            {
                currentSpeed = maxSpeed;
                isStopping = true; 
            }
        }
        // 減速階段
        else if (isStopping)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed <= 0f)
            {
                currentSpeed = 0f;
                isSpinning = false;
                isStopping = false;
            }
        }

        // 執行旋轉 (繞著 Z 軸轉動)
        transform.Rotate(0, 0, -currentSpeed * Time.deltaTime);
    }
}