using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BinaryTechnologies.Items.Tools
{
	public class GigabyteMultiToolPickaxe : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 42;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 6;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4f;
			Item.value = 1400000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.pick = 210;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.tileBoost = 2;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<MegabyteMultiToolPickaxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GigabyteUpgradeModule>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

		public override bool CanRightClick()
		{
			return true;
		}

		

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<GigabyteMultiToolHamaxe>());
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}