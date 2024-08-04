using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    //[SerializeField] protected Transform tf;

    public Vector3 destination;
    public Vector3 winPos;

    private IState currentState;
    private Floor currentFloor;

    public NavMeshAgent agent;

    public Enemy(ColorType color)
    {
        this.color = color;
    }
        

    // Change character color, set speed, set idle state when start
    public override void OnInit()
    {
        Debug.Log("aaaaaa");
        ChangeColor(color);
        agent.enabled = true;
        agent.speed = 100f;
        ChangeState(new IdleState());
    }

    // if state is gameplay - character can move
    // else state is not gameplay - character stop
    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }

            isGround = CheckGround();

            //CheckIsUp
            if (Tf.forward.z > 0f)
            {
                isUp = true; // go on
            }
            else if (Tf.forward.z < 0f)
            {
                isUp = false; // go off
            }
        }
        else if (currentState != null && currentState.GetType() != typeof(PauseState))
        {
            ChangeState(new PauseState());
        }
    }

    // Check when character came target des
    public bool IsDestination()
    {
        float distance = Mathf.Abs(Tf.position.x - destination.x) + Mathf.Abs(Tf.position.z - destination.z);
        return distance < 0.1f;
    }

    // Set destination for bot AI
    public void SetDestination(Vector3 pos)
    {
        agent.enabled = true;
        destination = pos;
        agent.destination = pos;
        agent.SetDestination(pos);
    }

    public override void OnStopMove()
    {
        agent.enabled = false;
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public Vector3 SeekBrickSameColor()
    {
        return currentFloor.SeekBrick(color);
    }

    private void OnTriggerEnter(Collider other)
    {
        ColorBrick colorBrick = other.GetComponent<ColorBrick>();

        if (other.CompareTag(TagName.STAIR_TAG))
        {
            if (colorBrick.color != color)
            {
                if (brickStack.Count > 0) // Still have brick
                {
                    colorBrick.ChangeColor(color);
                    RemoveBrick();
                }
                else // Out of brick
                {
                    ChangeState(new PatrolState());
                }
            }
        }
        else if (other.CompareTag(TagName.BRICK_TAG) && isGround)
        {
            // Take brick if same color
            if (colorBrick.color == color)
            {
                colorBrick.ChangeColor(ColorType.None);
                AddBrick();
                Debug.Log("Take");
            }
        }

        if (other.CompareTag("Ground"))
        {
            currentFloor = other.gameObject.GetComponent<Floor>();
        }

        if (other.CompareTag(TagName.WIN_AREA))
        {
            Win();
            GameManager.Instance.ChangeState(GameState.Finish);
            LevelManager.Instance.OnFinish();
            UIManager.Instance.OpenUI<CanvasFail>();
        }
    }
}