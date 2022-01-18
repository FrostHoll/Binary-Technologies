using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace MyTestMod.Items.Tools
{
	public class MegabyteMultiToolHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Megabyte MultiTool [Hamaxe]");
			Tooltip.SetDefault("Right click to switch to pickaxe");
		}

		public override void SetDefaults()
		{

			Item.damage = 70;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 7;
			Item.useAnimation = 29;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7f;
			Item.value = 15000;
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item1;
			Item.axe = 30;
			Item.hammer = 90;
			Item.tileBoost++;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			int _item = player.QuickSpawnItem(ModContent.ItemType<Items.Tools.MegabyteMultiToolPickaxe>());
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}