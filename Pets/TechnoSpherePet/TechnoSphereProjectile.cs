using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Pets.TechnoSpherePet
{
    public class TechnoSphereProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Sphere");

            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            Projectile.Size = new Vector2(25, 25);
            AIType = ProjectileID.ZephyrFish;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            player.zephyrfish = false;

            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),
                Projectile.position, Vector2.Zero, ModContent.ProjectileType<TechnoSphereProjectile1>(),
                0, 0f, Main.myPlayer, Projectile.whoAmI);
            }


            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.dead && player.HasBuff(ModContent.BuffType<TechnoSpherePetBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            //int animationSpeed = 20;

            //Projectile.frameCounter++;
            //if (Projectile.frameCounter > animationSpeed)
            //{
            //    Projectile.frameCounter = 0;
            //    Projectile.frame++;

            //    if (Projectile.frame >= Main.projFrames[Projectile.type])
            //    {
            //        Projectile.frame = 0;
            //    }
            //}
        }
    }

    public class TechnoSphereProjectile1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Sphere");

            //Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(45, 45);
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.netImportant = true;
            AIType = 0;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            player.zephyrfish = false;

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.dead && player.HasBuff(ModContent.BuffType<TechnoSpherePetBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            Projectile.Center = Main.projectile[(int)Projectile.ai[0]].Center;
            Projectile.rotation -= MathHelper.ToRadians(2f);
        }
    }
}
