using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class GameProcessor
{
    public int _randomSeed = 114514;

    private Dictionary<int, Player> _players = new Dictionary<int, Player>();// 保证player_id 从 0 -- n-1

    private MapModel _map;

    private List<Fight> _fights = new List<Fight>(); // 战斗列表

    private List<CellData> changedCellData = new List<CellData>();

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

        // 更新玩家占领的格子

    }

    private void SyncGameData()
    {

    }


    private void CountAllMoveAndFights()
    {
        Dictionary<CellPos, Dictionary<int, MoveOperation>> moveTarget = new Dictionary<CellPos, Dictionary<int, MoveOperation>>();
        Dictionary<CellPos, int> cellController = new Dictionary<CellPos, int>();

        // 统计地图的控制权
        // todo 将控制权写到mapdata里而不是每个玩家
        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];
            if (player == null) Debug.Log("不是哥们你player_ID不按规定设置的吗?");

            foreach (CellPos cell in player.occupiedCells)
            {
                cellController[cell] = i;
            }
        }

        foreach (Player player in _players.Values)
        {
            // Player player = _players[i];
            if (player == null)
            {
                Debug.LogError("不是哥们player == null");
                continue;
            }

            // 统计所有的移动和战斗
            foreach (IOperation operation in player.operations)
            {
                if (operation.GetType() == typeof(MoveOperation))
                {
                    MoveOperation op = (MoveOperation)operation;
                    moveTarget[op.dstPos][player.id] = op;
                }
                else if (operation.GetType() == typeof(FightOperation))
                {
                    FightOperation op = (FightOperation)operation;

                    Division ATKDivision = op.division;
                    Division DEFDivision = new Division(player, _map.cellData[op.dstPos].GetUnits());

                    _fights.Add(new Fight(
                        cellController[op.srcPos],
                        false,
                        cellController[op.dstPos],
                        true,
                        ATKDivision,
                        DEFDivision));

                }
            }
        }

        foreach (var kvp in moveTarget)
        {
            // 如果一个格子的moveTarget有多个玩家，则进行战斗
            if (kvp.Value.Count > 1)
            {
                CellPos cellPos = kvp.Key;
                List<int> playIdList = new List<int>();
                List<Division> divisionList = new List<Division>();
                foreach (var id in kvp.Value)
                {
                    playIdList.Add(id.Key);
                    divisionList.Add(id.Value.division);
                }

                _fights.Add(new Fight(
                    playIdList[0],
                    false,
                    playIdList[1],
                    false,
                    divisionList[0],
                    divisionList[1]
                ));
            }
            // 应用移动
            else
            {
                MoveOperation moveOp = kvp.Value.First().Value;
                CellData srcCellData = new CellData(moveOp.srcPos);
                srcCellData._units = _map.cellData[moveOp.srcPos]._units;
                foreach (FightingUnit unit in moveOp.division.GetFightingUnits())
                {
                    srcCellData._units.Remove(unit.unit);
                }
                changedCellData.Add(srcCellData);

                CellData dstCellData = new CellData(moveOp.dstPos);
                dstCellData._units = _map.cellData[moveOp.dstPos]._units;
                foreach (FightingUnit unit in moveOp.division.GetFightingUnits())
                {
                    dstCellData._units.Add(unit.unit);
                }
                changedCellData.Add(dstCellData);
            }
        }

    }

    private void ApplyAllFights()
    {

    }

    public void SetMapModel(MapModel mapModel)
    {
        this._map = mapModel;
    }

}
