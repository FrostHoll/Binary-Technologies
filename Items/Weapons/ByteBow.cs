using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Weapons
{
	public class ByteBow : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 34;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 4;
			Item.useAnimation = 8;
			Item.reuseDelay = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2;
			Item.value = 18000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 7.5f;
			Item.noMelee = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<BitBow>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ByteUpgradeModule>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 2);
        }
	}
}