using System.Collections.Generic;
using GameCore;
using GameCore.Player;
using Mirror;
using Utils;

namespace Cheats
{
    public class BattleCheat : NetworkBehaviour
    {
        public void Kill(int i)
        {
            CLog.LogWHeader("Cheat", $"Kill player: {i}", "r", "w");
            var players = Battle.current.Players;
            if (players.Count <= i)
                return;
            players[i].OnDeath();
        }
        
        public void FirstWin()
        {
            CLog.LogWHeader("Cheat", "First win call", "r", "w");
            var players = Battle.current.Players;
            Win(players, 0);
        }

        public void SecondWin()
        {
            CLog.LogWHeader("Cheat", "Second win call", "r", "w");
            var players = Battle.current.Players;
            Win(players, 1);
        }

        private void Win(IList<PlayerClient> players, int index)
        {
            if (players.Count <= index)
                return;
            players[index].Win();
            for (var i = 0; i < players.Count; i++)
            {
                if(index == i)
                    continue;
                players[index].Loose();
            }
        }
    }
}