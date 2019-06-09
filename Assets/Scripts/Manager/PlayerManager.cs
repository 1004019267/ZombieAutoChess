using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class PlayerManager : Singleton<PlayerManager>
{
    List<Player> players = new List<Player>();
    public void AddPlayer(Player p)
    {
        players.Add(p);
    }

    public void RemovePlayer(Player p)
    {
        players.Remove(p);
    }

    //获得右边玩家
    public Player getLeftPlayer()
    {

        return BattleManager.getMe().leftPlayer;
    }
    //获得左边玩家
    public Player getRightPlayer()
    {

        return BattleManager.getMe().rightPlayer;
    }

    public AiPlayer getAiPlayer()
    {

        return (AiPlayer)BattleManager.getMe().rightPlayer;
    }

    public Player GetPlayer(int id)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].id == id)
            {
                return players[i];
            }
        }
        return null;
    }







}

