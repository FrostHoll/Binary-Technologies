using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace BinaryTechnologies.Items.Weapons
{
    public class MegabyteBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Changes Wooden Arrows to Megabyte Arrows");
		}

		public override void SetDefaults()
		{
			Item.damage = 55;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2.5f;
			Item.value = 180000;
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 9f;
			Item.scale = 0.8f;
			Item.noMelee = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<KilobyteBow>(), 1);
			recipe.AddIngredient(ItemType<Items.MegabyteShard>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int shootType = type;
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				shootType = ModContent.ProjectileType<Projectiles.MegabyteArrowProjectile>();
			}

			Projectile.NewProjectile(source, position.X, position.Y + 15, velocity.X, velocity.Y, shootType, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, shootType, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y - 15, velocity.X, velocity.Y, shootType, damage, knockback, player.whoAmI);
			return false;
		}
	}
}