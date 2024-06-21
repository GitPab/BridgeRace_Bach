using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : BaseMono
{
    [SerializeField] ColorData colorData;
    [SerializeField] Renderer meshRenderer;
    [SerializeField] private Animator anim;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform brickStackPostion;
    [SerializeField] protected Transform brickStackHolder;
    [SerializeField] protected Brick brickCharacter;
    public ColorType color;
    public List<Brick> brickStack = new List<Brick>();

    protected bool isGround;
    protected bool isUp;
    
    private string currentAnimName = "idle";
    private float raycastMaxDistance = 3f;

    public virtual void OnInit()
    {

    }

    public virtual void OnStopMove()
    {
        //For override
    }

    // Check if character on ground
    public bool CheckGround()
    {
        return Physics.Raycast(Tf.position, Tf.TransformDirection(Vector3.down), raycastMaxDistance, groundLayer);
    }

    public void AddBrick()
    {
        brickCharacter.GetComponent<ColorBrick>().ChangeColor(color);
        Brick brickStackPrefab = Instantiate(brickCharacter, brickStackPostion.position + Vector3.up * brickStack.Count * 0.2f, brickStackPostion.rotation);
        brickStackPrefab.transform.parent = this.brickStackHolder;
        brickStack.Add(brickStackPrefab);
    }

    public void RemoveBrick()
    {
        Destroy(brickStack[brickStack.Count - 1].gameObject);
        brickStack.Remove(brickStack[brickStack.Count - 1]);
    }

    public void ClearBrick()
    {
        for (int i = 0; i < brickStack.Count; i++)
        {
            Destroy(brickStack[i].gameObject);
        }
        brickStack.Clear();
    }

    public virtual void Win()
    {
        ClearBrick();
        OnStopMove();
        ChangeAnim(AnimName.VICTORY);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        Debug.Log(color + "======");
        meshRenderer.material = colorData.GetMat(colorType);
        if(this is Enemy)
        {
            Debug.Log(color + "----------");
        }
    }
}
