using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace MyTestMod.Projectiles
{
    public class LastHopeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(32, 32);
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.arrow = false;
            Projectile.light = 0.8f;
            Projectile.timeLeft = 600;
            DrawOffsetX = -26;
            //AIType = ProjectileID.SwordBeam;
            //Projectile.aiStyle = ProjAIStyleID.Beam;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            //Vector2 dustPosition = Projectile.Center + new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)); ;
            //Dust dust = Dust.NewDustPerfect(dustPosition, 229, null, 100, Color.Cyan, 2f);
            if(Main.rand.NextBool(3)) Dust.NewDust(new Vector2(Projectile.Hitbox.X, Projectile.Hitbox.Y), Projectile.Hitbox.Width, Projectile.Hitbox.Height, 101, 0f, 0f, 100, Color.LightCyan, 1.2f);
            
            //dust.velocity *= 0.3f;
            //dust.noGravity = true;

            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                return;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;

                Projectile.velocity = Vector2.Normalize(vectorToCursor) * 8f;

                Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
                Projectile.netUpdate = true;

            }

        }

        public override void Kill(int timeLeft)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 3, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 1f);
        }
    }
}
