using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Developers
{
	public class InfinityDirt : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 1;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = false;
			Item.rare = ItemRarityID.Blue;
			Item.value = 0;
			Item.createTile = TileID.Dirt;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 999);
			recipe.AddIngredient(ModContent.ItemType<TerrabyteShard>(), 1);
			recipe.Register();
		}
	}
}