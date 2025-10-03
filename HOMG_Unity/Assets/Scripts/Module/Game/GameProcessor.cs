
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class GameProcessor
{
    public int _randomSeed = 114514;

    private Dictionary<int, Player> _players = new Dictionary<int, Player>();// 保证player_id 从 0 -- n-1

    private MapModel _map;

    private List<Fight> _fights = new List<Fight>(); // 战斗列表

    public async void ProcessRound()
    {
        // 同步游戏数据
        await Task.Run(() => SyncGameData());

        // 设置随机种子
        Random.InitState(_randomSeed);

        // 统计所有移动&战斗
        CountAllMoveAndFights();

        // 执行所有战斗
        ApplyAllFights();


        // // 处理玩家操作
        // for (int i = 0; i < _players.Count; i++)
        // {
        //     Player player = _players[i];
        //     if (player == null) Debug.Log("不是哥们你player_ID不按规定设置的吗?");
        //
        //     // 执行玩家操作
        //     foreach (IOperation operation in player.operations)
        //     {
        //         // 此处的operation需要按照先移动 后攻击的顺序放入数组
        //         operation.Process(_players);
        //     }
        //
        // }

    }

    private void SyncGameData()
    {

    }


    private void ApplyAllFights()
    {

    }

    private void CountAllMoveAndFights()
    {
        Dictionary<CellPos, List<int>> moveTarget = new Dictionary<CellPos, List<int>>();
        Dictionary<CellPos, Dictionary<int, List<Unit>>> cellData = new Dictionary<CellPos, Dictionary<int, List<Unit>>>();
        Dictionary<CellPos, int> cellController = new Dictionary<CellPos, int>();

        int fightId = 0;

        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];
            if (player == null) Debug.Log("不是哥们你player_ID不按规定设置的吗?");

            foreach (CellPos cell in player.occupiedCells)
            {
                cellController[cell] = i;
            }
        }

        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];
            if (player == null) Debug.Log("不是哥们你player_ID不按规定设置的吗?");

            // 执行玩家操作
            foreach (IOperation operation in player.operations)
            {
                if (operation.GetType() == typeof(MoveOperation))
                {
                    MoveOperation op = (MoveOperation)operation;
                    foreach (FightingUnit fUnit in op.division.GetFightingUnits())
                    {
                        if (cellData[op.srcPos] == null)
                        {
                            cellData[op.srcPos] = new Dictionary<int, List<Unit>>();
                        }
                        if (cellData[op.srcPos].ContainsKey(i) == false)
                        {
                            cellData[op.srcPos][i] = new List<Unit>();
                        }
                        cellData[op.srcPos][i].Add(fUnit.unit);

                        if (moveTarget[op.dstPos].Contains(i) == false)
                        {
                            moveTarget[op.dstPos].Add(i);
                        }

                    }
                }
                else if (operation.GetType() == typeof(FightOperation))
                {
                    FightOperation op = (FightOperation)operation;

                    Division ATKDivision = op.division;
                    Division DEFDivision = new Division(_map.cellData[op.dstPos].GetUnits());

                    _fights.Add(new Fight(
                        fightId++,
                        cellController[op.srcPos],
                        false,
                        cellController[op.dstPos],
                        true,
                        ATKDivision,
                        DEFDivision));

                }
            }

            // 如果一个格子上有多个玩家的单位，则进行战斗
            foreach (KeyValuePair<CellPos, List<int>> kvp in moveTarget)
            {
                if (kvp.Value.Count > 1)
                {
                    CellPos cellPos = kvp.Key;
                    int playerId1 = kvp.Value[0];
                    int playerId2 = kvp.Value[1];

                    // 进行战斗
                    Division player1Division = new Division(cellData[cellPos][playerId1]);
                    Division player2Division = new Division(cellData[cellPos][playerId2]);

                    _fights.Add(new Fight(
                        fightId++,
                        playerId1,
                        false,
                        playerId2,
                        false,
                        player1Division,
                        player2Division
                        ));
                }
            }

        }
    }

    public void SetMapModel(MapModel mapModel)
    {
        this._map = mapModel;
    }

}
