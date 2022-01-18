using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MyTestMod.Items.Weapons
{
    class MegabyteSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("On impact, it releases a cloud that corrodes everything in its path");
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4.5f;
            Item.scale = 1f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.MegabyteSwordProjectile>();
            Item.shootSpeed = 11f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Items.Weapons.KilobyteSword>(), 1);
            recipe.AddIngredient(ItemType<MegabyteShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int numClouds = Main.rand.Next(3, 4);
            for (int i = 0; i < numClouds; i++)
            {
                int spawnCloud = Main.rand.NextFromList<int>(ProjectileType<Projectiles.MegabyteCloud30>(), ProjectileType<Projectiles.MegabyteCloud32>(), ProjectileType<Projectiles.MegabyteCloud40>());
                Projectile.NewProjectile(player.GetProjectileSource_Item(Item), target.position, new Vector2(1f, 1f).RotatedByRandom(360), spawnCloud, damage / 4, knockBack, player.whoAmI);
            }
        }
    }
}
