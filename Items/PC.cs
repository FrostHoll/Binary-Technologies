using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyTestMod.Items
{
	public class PC : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PC");
			Tooltip.SetDefault("On this table you can create things filled with information.");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 14;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = 150;
			Item.createTile = ModContent.TileType<Tiles.TilePC>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 15);
			recipe.AddIngredient(ItemID.Glass, 5);
			recipe.AddIngredient(ItemID.Wire, 20);
			recipe.AddIngredient(ModContent.ItemType<Items.ElectMaterial>(), 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}