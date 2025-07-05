
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int id; // 玩家ID
    public List<CellPos> occupiedCells; // 占领的格子
    public List<IOperation> operations; // 玩家本回合操作列表

    public int RetreatCell(CellPos cell)
    {
        if (occupiedCells == null)
        {
            Debug.Log("occupiedCells is null");
            return -1; // 返回-1表示操作失败
        }

        if (occupiedCells.Contains(cell) == false)
        {
            Debug.Log(cell+"cell not occupied by player");
            return -1;// 返回-1表示操作失败
        }
        occupiedCells.Remove(cell); // 从占领的格子列表中移除该格子
        return 0;// 返回0表示操作成功
    }

    public int OccupyCell(CellPos cell)
    {
        if (occupiedCells == null)
        {
            Debug.Log("occupiedCells is null");
            return -1; // 返回-1表示操作失败
        }

        if (occupiedCells.Contains(cell))
        {
            Debug.Log(cell+"cell already occupied by player");
            return -1; // 返回-1表示操作失败
        }

        occupiedCells.Add(cell); // 将该格子添加到占领的格子列表中
        return 0; // 返回0表示操作成功
    }
}

