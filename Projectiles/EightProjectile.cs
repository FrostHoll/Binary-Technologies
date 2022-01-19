using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace BinaryTechnologies.Projectiles
{
    public class EightProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(20, 20);
            //Projectile.scale = 1.4f;
            //Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = false;
            DrawOffsetX = -5;
            DrawOriginOffsetY = -10;
            Projectile.light = 0.8f;           
        } 

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 dustPosition = Projectile.Center;
            Dust dust = Dust.NewDustPerfect(dustPosition, 229, null, 100, Color.Lime, 0.8f);
            dust.velocity *= 0.3f;
            dust.noGravity = true;

            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                return;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                
                Projectile.velocity = Vector2.Normalize(vectorToCursor) * 4f;
                
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
