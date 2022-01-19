using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BinaryTechnologies.Projectiles
{
    public class MegabyteArrowProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16, 16);
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = true;
            Projectile.light = 0.8f;
            DrawOffsetX = -15;
            DrawOriginOffsetY = -8;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 71, null, 100, Color.White, 0.8f);
                dust.velocity *= 0.3f;
                dust.noGravity = true;
            }
            base.AI();
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 71, 0f, 0f, 100, default(Color), 1.2f);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            float spawnRadius = 100f;
            Vector2 spawnPosition = target.position + new Vector2(spawnRadius, 0f).RotatedByRandom(360);

            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), spawnPosition, spawnPosition.DirectionTo(target.position) * 4f, ModContent.ProjectileType<Projectiles.MegabyteProjectile>(), damage, knockback, Main.LocalPlayer.whoAmI);
        }
    }
}
