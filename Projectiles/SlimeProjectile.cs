using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace BinaryTechnologies.Projectiles
{
    public class SlimeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override string Texture => "BinaryTechnologies/Projectiles/TechnoSlimeSpike";

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(20, 20);
            //Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.arrow = true;
            Projectile.aiStyle = ProjAIStyleID.Arrow;

        }

        public override void Kill(int timeLeft)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 4, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, Color.Green, 1f);
        }
    }
}
