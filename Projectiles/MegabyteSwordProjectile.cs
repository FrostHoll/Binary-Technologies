using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace BinaryTechnologies.Projectiles
{
    public class MegabyteSwordProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16, 16);
            //Projectile.scale = 1f;
            //Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = false;
            DrawOffsetX = -15;
            DrawOriginOffsetY = -8;
            Projectile.light = 0.8f;

        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 dustPosition = Projectile.Center;
            //if (Main.rand.NextBool(3)) Dust.NewDust(new Vector2(Projectile.Hitbox.X, Projectile.Hitbox.Y), Projectile.Hitbox.Width, Projectile.Hitbox.Height, 105, 0f, 0f, 100, Color.White, 1.5f);
            Dust dust = Dust.NewDustPerfect(dustPosition, 71, null, 255, Color.White, 1f);
            dust.velocity *= 0.3f;
            dust.noGravity = true;

            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
                return;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;

                Projectile.velocity = Vector2.Normalize(vectorToCursor) * 11f;

                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;

            }

        }

        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            int numClouds = Main.rand.Next(3, 4);
            for (int i = 0; i < numClouds; i++)
            {
                int spawnCloud = Main.rand.NextFromList<int>(ProjectileType<Projectiles.MegabyteCloud30>(), ProjectileType<Projectiles.MegabyteCloud32>(), ProjectileType<Projectiles.MegabyteCloud40>());
                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.position, new Vector2(1f, 1f).RotatedByRandom(360), spawnCloud, damage / 4, knockBack, Main.LocalPlayer.whoAmI);
            }
        }

    }
}
