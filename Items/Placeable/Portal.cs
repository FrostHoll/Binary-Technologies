using BinaryTechnologies.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Placeable
{
	public class Portal : ModItem
	{
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
			Item.createTile = ModContent.TileType<TilePortal>();
		}

        public override bool CanUseItem(Player player)
        {
			foreach (TileEntity te in TileEntity.ByID.Values)
			{
				if (te.type == ModContent.TileEntityType<TEPortal>())
				{
					return false;
				}
			}
			return true;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Marble, 30);
			recipe.AddIngredient(ModContent.ItemType<BitShard>(), 4);
			recipe.AddIngredient(ModContent.ItemType<ElectMaterial>(), 4);
			recipe.AddIngredient(ModContent.ItemType<EnergyCore>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}