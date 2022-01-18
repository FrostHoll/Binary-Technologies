using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace MyTestMod.Projectiles
{
    public class BitArrowProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(10, 28);
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void Kill(int timeLeft)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 3, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 1f);
        }

    }
}
