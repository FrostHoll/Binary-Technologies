using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace BinaryTechnologies.Items.Weapons
{
	public class LastHope : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This sword is imbued with the mighty power of dying hope.");
		}

		public override void SetDefaults()
		{
			Item.damage = 800;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 52;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.LastHopeProjectile>();
			Item.shootSpeed = 10f;
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 101, 0f, 0f, 100, Color.Cyan, 1.2f);
			}

			//Dust dust = Dust.NewDustPerfect(hitbox.Top() , 101, null, 100, Color.Cyan, 1.2f);
        }

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DD2SquireBetsySword, 1);
			recipe.AddIngredient(ItemID.Meowmere, 1);
			recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
        }
    }
}