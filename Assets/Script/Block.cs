using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    start,
    wall,
    enemy,
    fire,
    hammer,
    end
}

public class Block : MonoBehaviour
{
    public BlockType blocktype;
    public Vector2 gridIndex;

}
