using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    private IEnumerator moveCoroutine;

    private int x;
    private int y;

    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    private Grid.PieceType type;

    public Grid.PieceType Type
    {
        get { return type; }
    }

    private Grid grid;

    public Grid GridRef
    {
        get { return grid; }
    }

    public void Init(int _x,int _y,Grid _grid,Grid.PieceType _type)
    {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }
    private void OnMouseDown()
    {
        grid.PressTile(this);
    }
    private void OnMouseEnter()
    {
        grid.EnterTile(this);
    }
    private void OnMouseUp()
    {
        grid.ReleaseTile();
    }
    public void Move(int newX,int newY,float time)
    {
        if(moveCoroutine !=null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = MoveCoroutine(newX, newY, time);
        StartCoroutine(moveCoroutine);
    }

    private IEnumerator MoveCoroutine(int newX,int newY,float time)
    {
        X = newX;
        Y = newY;

        Vector3 startPos=transform.position;
        Vector3 endPos = this.GridRef.GetWorldPosition(newX, newY);

        for (float t = 0; t <= 1 * time; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t / time);
            yield return 0;
        }
        transform.position = endPos;
    }
}

