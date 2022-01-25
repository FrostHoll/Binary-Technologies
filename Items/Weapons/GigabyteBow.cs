using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Weapons
{
	public class GigabyteBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots 3 arrows at once." +
				"\nNot moving for 6 seconds putting you in rage mode, shooting one arrow per shot rapidly" +
				"\nHas 33% chance not to consume ammo");
		}

		public override void SetDefaults()
		{
			Item.damage = defaultDamage;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2.5f;
			Item.value = 1400000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 11f;
			Item.scale = 0.8f;
			Item.noMelee = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<MegabyteBow>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Items.GigabyteUpgradeModule>(), 1);
			recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
			recipe.Register();
		}

		private readonly int defaultDamage = 75;
		private readonly int defaultSpeed = 16;
		private int buff_timer = 0;
		private bool isBuff = false;
		public override void HoldItem(Player player)
        {
			//player.GetModPlayer<BinaryTechnologiesPlayer>().gigabyteBowBuff = true;
			if (!isBuff && player.velocity == Vector2.Zero) buff_timer++;

			if (player.velocity != Vector2.Zero)
			{
				buff_timer = 0;
				isBuff = false;

			}

			if (buff_timer > 360)
			{
				isBuff = true;
				buff_timer = 0;
			}         

			if (isBuff)
			{
				Item.damage = 150;
				Item.useTime = 8;
				if (Main.rand.NextBool(2))
				{
					int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 182, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 0.8f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					//Main.PlayerDrawDust.Add(dust);
				}
			}
            else
            {
				Item.damage = defaultDamage;
				Item.useTime = defaultSpeed;
			}
		}

        public override void UpdateInventory(Player player)
        {
			if (player.inventory[player.selectedItem].type != Type)
			{
				buff_timer = 0;
				isBuff = false;
			}
		}

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (isBuff)
            {
				return true;
            }
			Projectile.NewProjectile(source, position.X, position.Y + 15, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y - 15, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			return false;
		}

		public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .33f;
		}
	}
}