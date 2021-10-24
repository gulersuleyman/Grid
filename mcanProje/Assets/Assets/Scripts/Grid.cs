using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum PieceType
    {
        NORMAL,
        JOKER,
        COUNT,
    };

    
    public int xDim;
    public int yDim;
    public float swapTime;
    
    public GameObject[] tiles;

    public GameTile[,] gameTiles;

    //private PieceType type;
    private GameTile pressedTile;
    private GameTile enteredTile;
    // Start is called before the first frame update
    void Start()
    {
        int tileNum=0;
        gameTiles = new GameTile[xDim, yDim];

        for (int x = 0; x <xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject newTile = (GameObject)Instantiate(tiles[tileNum], GetWorldPosition(x,y), Quaternion.identity);
                newTile.transform.parent = transform;

                gameTiles[x, y] = newTile.GetComponent<GameTile>();
                if (x == 0 && y == 0)
                {
                    gameTiles[x, y].Init(x, y, this, PieceType.JOKER);
                }
                else
                {
                    gameTiles[x, y].Init(x, y, this, PieceType.NORMAL);
                }

                tileNum++;
            }
        }

    }

    public Vector2 GetWorldPosition(int x,int y)
    {
        return new Vector2(transform.position.x - xDim / 2 + x,
            transform.position.y - yDim / 2 + y);
    }
    public bool IsAdjacent(GameTile tile1,GameTile tile2)
    {
        return ((tile1.X == tile2.X && (int)Mathf.Abs(tile1.Y - tile2.Y) == 1)
            || (tile1.Y == tile2.Y && (int)Mathf.Abs(tile1.X - tile2.X) == 1));
    }
    public void SwapTile(GameTile tile1,GameTile tile2)
    {
        if (tile2.Type == PieceType.JOKER)
        {
            gameTiles[tile1.X, tile1.Y] = tile2;
            gameTiles[tile2.X, tile2.Y] = tile1;

            int tile1X = tile1.X;
            int tile1Y = tile1.Y;

            tile1.Move(tile2.X, tile2.Y, swapTime);
            tile2.Move(tile1X, tile1Y, swapTime);
        }
    }
    public void PressTile(GameTile tile)
    {
        pressedTile = tile;
    }
    public void EnterTile(GameTile tile)
    {
        enteredTile = tile;
    }
    public void ReleaseTile()
    {
        if(IsAdjacent(pressedTile,enteredTile))
        {
            SwapTile(pressedTile, enteredTile);
        }
    }
}
