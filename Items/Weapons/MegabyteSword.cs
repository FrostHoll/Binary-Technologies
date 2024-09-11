using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items.Weapons
{
    class MegabyteSword : ModItem
    {
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
            Item.value = 180000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.MegabyteSwordProjectile>();
            Item.shootSpeed = 11f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KilobyteSword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MegabyteUpgradeModule>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numClouds = Main.rand.Next(3, 4);
            for (int i = 0; i < numClouds; i++)
            {
                int spawnCloud = Main.rand.NextFromList<int>(
                    ModContent.ProjectileType<Projectiles.MegabyteCloud30>(),
                    ModContent.ProjectileType<Projectiles.MegabyteCloud32>(),
                    ModContent.ProjectileType<Projectiles.MegabyteCloud40>());
                Projectile.NewProjectile(player.GetSource_FromThis(), 
                    target.position, 
                    new Vector2(1f, 1f).RotatedByRandom(360), 
                    spawnCloud, 
                    hit.Damage / 4, 
                    hit.Knockback, 
                    player.whoAmI);
            }
        }
    }
}
