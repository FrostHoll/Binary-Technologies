using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace MyTestMod.Items.Tools
{
	public class MultiToolAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MultiTool");
			Tooltip.SetDefault("Right click to switch to pickaxe");
		}

		public override void SetDefaults()
		{

			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 14;
			Item.useAnimation = 27;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7f;
			Item.value = 15000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.axe = 30;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            int _item = player.QuickSpawnItem(ModContent.ItemType<Items.Tools.MultiToolPickaxe>());
        }

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}