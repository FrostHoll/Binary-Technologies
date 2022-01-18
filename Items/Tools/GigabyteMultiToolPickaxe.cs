using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace MyTestMod.Items.Tools
{
	public class GigabyteMultiToolPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gigabyte MultiTool [Pickaxe]");
			Tooltip.SetDefault("Right click to switch to hamaxe");
		}

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
			Item.value = 15000;
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
			recipe.AddIngredient(ModContent.ItemType<Items.Tools.MegabyteMultiToolPickaxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Items.GigabyteShard>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

		public override bool CanRightClick()
		{
			return true;
		}

		

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<Items.Tools.GigabyteMultiToolHamaxe>());
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => (pre != -3 && pre != -1);
	}
}