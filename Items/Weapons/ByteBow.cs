using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MyTestMod.Items.Weapons
{
	public class ByteBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is x8 times more powerful bow than Bit Bow! (nope)");
		}

		public override void SetDefaults()
		{

			Item.damage = 38;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 4;
			Item.useAnimation = 8;
			Item.reuseDelay = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(gold: 1, silver: 60);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 7.5f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<BitBow>(), 1);
			recipe.AddIngredient(ItemType<ByteShard>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

		public override bool CanConsumeAmmo(Player player)
		{
			return !(player.itemAnimation < Item.useAnimation - 2);
		}
	}
}