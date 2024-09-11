using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BinaryTechnologies.Items.Tools
{
	public class MegabyteMultiToolPickaxe : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 7;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5f;
			Item.value = 180000;
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item1;
			Item.pick = 200;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.tileBoost = 2;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<KilobyteMultiToolPickaxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MegabyteUpgradeModule>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<MegabyteMultiToolHamaxe>());
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}