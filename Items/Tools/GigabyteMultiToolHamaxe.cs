using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BinaryTechnologies.Items.Tools
{
	public class GigabyteMultiToolHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gigabyte MultiTool [Hamaxe]");
			Tooltip.SetDefault("Right click to switch to pickaxe");
		}

		public override void SetDefaults()
		{

			Item.damage = 74;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 7;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7f;
			Item.value = 1400000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.axe = 30;
			Item.hammer = 90;
			Item.tileBoost = 2;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

        

        public override void RightClick(Player player)
		{
			int _item = player.QuickSpawnItem(ModContent.ItemType<Items.Tools.GigabyteMultiToolPickaxe>());
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}