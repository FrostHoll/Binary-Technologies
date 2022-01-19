using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace BinaryTechnologies.Items.Accessories
{
	public class ElectPwrGlove : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Electrical Power Glove");
			Tooltip.SetDefault("Weird glove that helps you perfoming melee attacks."
				+ "\n- It's so heavy! -"
				+ "\n8% increased melee damage"
				+ "\n10% decreased movement speed"
				+ "\nMelee attacks can elecrticify your enemies");
		}

		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.sellPrice(silver:60);
			Item.rare = ItemRarityID.Green;
			Item.maxStack = 1;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
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
			recipe.AddIngredient(ModContent.ItemType<Items.ElectMaterial>(), 6);
			recipe.AddIngredient(ModContent.ItemType<Items.BitShard>(), 8);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}
    }
}
