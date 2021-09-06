using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using System;

namespace WorseRespawn
{
	public class WorseRespawn : Mod
	{
		internal static WorseRespawn Instance;

		public override void Load()
		{
			Instance = this;
		}

		public override void Unload() {
			Instance = null;
		}
	}

	public class WorseRespawnPlayer : ModPlayer
	{
		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			bool bossAlive = false;
			if (Main.netMode != 0 && !pvp)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (Main.npc[i].active && (Main.npc[i].boss || Main.npc[i].type == 13 || Main.npc[i].type == 14 || Main.npc[i].type == 15) && Math.Abs(player.Center.X - Main.npc[i].Center.X) + Math.Abs(player.Center.Y - Main.npc[i].Center.Y) < 4000f)
					{
						bossAlive = true;
						break;
					}
				}
			}
			int playerCount = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					playerCount++;
				}
			}

			if (bossAlive)
			{
				if (playerCount > 3)
				{
					player.respawnTimer = (((playerCount-3)*10)+30)*60;
					return;
				}
				player.respawnTimer = 30 * 60;
				return;
			}
			if (Main.expertMode)
			{
				if (playerCount > 3)
				{
					player.respawnTimer = ((((playerCount-3)*10)+30)*60)/2;
					return;
				}
				player.respawnTimer = 15 * 60;
				return;
			}
			if (playerCount > 3)
			{
				player.respawnTimer = (int)((((((playerCount-3)*10)+30)*60)/2)*0.6);
				return;
			}
			player.respawnTimer = 10 * 60;
		}
	}
}