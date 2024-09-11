using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace BinaryTechnologies.Items.Accessories
{
	public class Adrenaline : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 80000;
			Item.rare = ItemRarityID.Pink;
			Item.maxStack = 1;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            if ((float)player.statLife / player.statLifeMax2 > 0.5f)
            {
				player.GetDamage(DamageClass.Generic) += 0.1f;
			}
            else
            {
				player.GetDamage(DamageClass.Generic) += 0.2f;
				player.moveSpeed *= 1.2f;
				//player.maxRunSpeed += 2f;
			}
			
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Items.ElectMaterial>(), 6);
			recipe.AddIngredient(ModContent.ItemType<Items.KilobyteShard>(), 8);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}
	}
}
