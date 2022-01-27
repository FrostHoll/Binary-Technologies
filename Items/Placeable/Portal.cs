using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Placeable
{
	public class Portal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Portal");
			Tooltip.SetDefault("This mysterious portal may connect you with another world..." +
				"\nPut a Byte Shard to activate it." +
				"\nBreak a portal to deactivate it.");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = 500;
			Item.createTile = ModContent.TileType<Tiles.TilePortal>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 15);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}