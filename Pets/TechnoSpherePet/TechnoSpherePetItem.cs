using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace BinaryTechnologies.Pets.TechnoSpherePet
{
    public class TechnoSpherePetItem : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Techno Sphere Shard");
			Tooltip.SetDefault("Summons a mini Techno Sphere to follow aimlessly behind you");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);

			Item.shoot = ModContent.ProjectileType<TechnoSphereProjectile>();
			Item.buffType = ModContent.BuffType<TechnoSpherePetBuff>();

			Item.value = Item.buyPrice(gold: 1);
			Item.master = true;
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600);
			}
		}
	}
}
