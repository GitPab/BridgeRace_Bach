using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : Character
{
    [SerializeField] public FixedJoystick joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float defaultSpeed;
    private Vector3 inputJoyStick;
    public int score = 0;
    private bool isGamePlayState;
    private Vector3 targetPos; // Reuse this instance
    private Camera mainCamera; // Cache the camera
    public override void OnInit()
    {
        ChangeColor(color);
    }

    private void Start()

    {

        mainCamera = Camera.main; // Cache the camera

        if (GameManager.Instance.IsState(GameState.GamePlay))

        {

            joystick.gameObject.SetActive(true);

        }

        // Initialize the brick stack

        brickStack.Clear();

    }

    private void Update()
    {
        isGamePlayState = GameManager.Instance.IsState(GameState.GamePlay);
        if (!isGamePlayState) return;

        isGround = CheckGround();

        inputJoyStick = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        HandleInput(inputJoyStick);

        // Go to the stair
        if (Tf.forward.z > 0)
        {
            isUp = true; // go on
        }
        else if (Tf.forward.z < 0)
        {
            isUp = false; //go off
            moveSpeed = defaultSpeed;
        }
    }

    private void HandleInput(Vector3 input)
    {
        if (input.magnitude > 0f)
        {
            ChangeAnim(AnimName.RUN);
            Move(input);
        }
        else
        {
            ChangeAnim(AnimName.IDLE);
        }
    }

    private void Move(Vector3 direct)
    {
        targetPos = mainCamera.transform.TransformDirection(direct);
        targetPos.y = 0f;

        if (Mathf.Abs(targetPos.x) > 0.1f || Mathf.Abs(targetPos.z) > 0.1f)
        {
            Quaternion targetRos = Quaternion.LookRotation(targetPos);
            Tf.rotation = Quaternion.Slerp(Tf.rotation, targetRos, 10f * Time.deltaTime);

            // Move the player
            Tf.position += targetPos * moveSpeed * Time.deltaTime;
        }
    }

    public override void OnStopMove()
    {
        base.OnStopMove();
    }

    public override void Win()
    {
        base.Win();
        joystick.gameObject.SetActive(false);
    }

    public void OnReset()
    {
        // Reset the player's position
        transform.position = Vector3.zero;

        // Reset the player's score
        score = 0;

        // Reset the player's speed
        moveSpeed = defaultSpeed;

        // Reset the player's animation
        ChangeAnim(AnimName.IDLE);

        // Reset the player's brick stack
        brickStack.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagName.STAIR_TAG))
        {
            HandleStairCollision(other);
            return;
        }

        if (other.CompareTag(TagName.BRICK_TAG))
        {
            HandleBrickCollision(other);
            return;
        }

        if (other.CompareTag(TagName.WIN_AREA))
        {
            Win();
            GameManager.Instance.ChangeState(GameState.Finish);
            LevelManager.Instance.OnFinish();
            UIManager.Instance.OpenUI<CanvasVictory>().SetBestScore(score);
            return;
        }
    }

    private void HandleStairCollision(Collider other)
    {
        ColorBrick colorBrick = other.GetComponent<ColorBrick>();
        if (colorBrick.color != color)
        {
            if (brickStack.Count > 0) // Still have brick
            {
                colorBrick.ChangeColor(color);
                RemoveBrick();
            }
            else // Out of brick
            {
                // Stop
                moveSpeed = 0;
            }
        }
    }

    private void HandleBrickCollision(Collider other)
    {
        ColorBrick colorBrick = other.GetComponent<ColorBrick>();
        if (isGround && colorBrick.color == color)
        {
            colorBrick.ChangeColor(ColorType.None);
            AddBrick();
            UIManager.Instance.GetUI<CanvasGamePlay>().UpdateScore(++score);
        }
    }
}