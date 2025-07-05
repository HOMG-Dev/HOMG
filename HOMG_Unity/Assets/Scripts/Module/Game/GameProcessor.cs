
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameProcessor
{
    public int _randomSeed = 114514;

    private Dictionary<int, Player> _players = new Dictionary<int, Player>();// 保证player_id 从 0 -- n-1

    public async void ProcessRound()
    {
        // 同步游戏数据
        await Task.Run(() => SyncGameData());

        // 设置随机种子
        Random.InitState(_randomSeed);

        // 处理玩家操作
        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];
            if (player == null) Debug.Log("不是哥们你player_ID不按规定设置的吗?");

            // 执行玩家操作
            foreach (IOperation operation in player.operations)
            {
                operation.Process(_players);
            }

        }
    }

    private void SyncGameData()
    {

    }

}
