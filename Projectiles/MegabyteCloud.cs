using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace BinaryTechnologies.Projectiles
{
    class MegabyteCloud40 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override string Texture => "BinaryTechnologies/Projectiles/MegabyteCloud40";

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(40, 40);
            Projectile.aiStyle = ProjAIStyleID.ToxicCloud;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = true;
            Projectile.light = 0.8f;
            AIType = ProjectileID.ToxicCloud;
        }

    }

    class MegabyteCloud32 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override string Texture => "BinaryTechnologies/Projectiles/MegabyteCloud32";

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(32, 32);
            Projectile.aiStyle = ProjAIStyleID.ToxicCloud;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = true;
            Projectile.light = 0.8f;
            AIType = ProjectileID.ToxicCloud;
        }

    }

    class MegabyteCloud30 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override string Texture => "BinaryTechnologies/Projectiles/MegabyteCloud30";

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(30, 30);
            Projectile.aiStyle = ProjAIStyleID.ToxicCloud;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            Projectile.arrow = true;
            Projectile.light = 0.8f;
            AIType = ProjectileID.ToxicCloud;
        }

    }
}
