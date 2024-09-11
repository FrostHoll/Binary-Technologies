using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BinaryTechnologies.Items.Tools
{
	public class BitMultiToolPickaxe : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 17;
			Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.pick = 55;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.scale = 0.8f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ElectMaterial>(), 3);
			recipe.AddIngredient(ModContent.ItemType<BitShard>(), 8);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<BitMultiToolHamaxe>());
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}