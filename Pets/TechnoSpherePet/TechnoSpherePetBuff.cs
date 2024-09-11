using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BinaryTechnologies.Pets.TechnoSpherePet
{
	public class TechnoSpherePetBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;

			int projType = ModContent.ProjectileType<TechnoSphereProjectile>();

			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex), 
					player.Center, 
					Vector2.Zero, 
					projType, 
					0, 
					0f, 
					player.whoAmI);
			}
		}
	}
}