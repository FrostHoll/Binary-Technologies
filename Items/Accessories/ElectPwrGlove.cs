using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BinaryTechnologies.Items.Accessories
{
	public class ElectPwrGlove : ModItem
	{
        public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 50000;
			Item.rare = ItemRarityID.Green;
			Item.maxStack = 1;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Melee) += 0.08f;
			player.moveSpeed -= 0.1f;
			player.maxRunSpeed -= 0.1f;
			player.GetModPlayer<BinaryTechnologiesPlayer>().electpwrglove = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ElectMaterial>(), 6);
			recipe.AddIngredient(ModContent.ItemType<BitShard>(), 8);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}
    }
}
