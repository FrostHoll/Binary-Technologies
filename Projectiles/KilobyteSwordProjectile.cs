using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace MyTestMod.Projectiles
{
    public class KilobyteSwordProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16, 16);
            //Projectile.scale = 1.4f;
            //Projectile.aiStyle = 27;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = false;
            DrawOffsetX = -10;
            DrawOriginOffsetY = -16;
            Projectile.light = 0.8f;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 dustPosition = Projectile.Center;
            //if (Main.rand.NextBool(3)) Dust.NewDust(new Vector2(Projectile.Hitbox.X, Projectile.Hitbox.Y), Projectile.Hitbox.Width, Projectile.Hitbox.Height, 197, 0f, 0f, 100, Color.White, 1.2f);
            Dust dust = Dust.NewDustPerfect(dustPosition, 197, null, 100, Color.White, 0.8f);
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

        public override void Kill(int timeLeft)
        {
            //Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 107, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, Color.Cyan, 1f);
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
        }

    }
}
