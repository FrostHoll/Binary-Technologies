using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BinaryTechnologies.Projectiles
{
    public class MegabyteProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16, 16);
            Projectile.scale = 1f;
            //Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = false;
            DrawOffsetX = -15;
            DrawOriginOffsetY = -8;
            Projectile.light = 0.8f;
            Projectile.timeLeft = 600;

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

        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 71, 0f, 0f, 100, default(Color), 2f);
            }
        }

    }
}
