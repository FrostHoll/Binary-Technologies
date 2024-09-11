using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BinaryTechnologies.Items.Tools
{
	public class BitMultiToolHamaxe : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 9;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 23;
			Item.useAnimation = 27;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5.5f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.axe = 11;
			Item.hammer = 55;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.scale = 0.8f;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<BitMultiToolPickaxe>());
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}