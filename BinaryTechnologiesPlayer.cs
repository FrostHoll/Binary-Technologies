using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace BinaryTechnologies
{
    public class BinaryTechnologiesPlayer : ModPlayer
    {
        public bool electpwrglove = false;

        public bool electShock = false;

        public bool gigabyteBowBuff = false;

        public override void ResetEffects()
        {
            base.ResetEffects();
            electpwrglove = false;
            electShock = false;
            gigabyteBowBuff = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (electpwrglove && item.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(2))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.ElectShock>(), 120);
                }
            }

            

            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (electpwrglove && proj.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(2))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.ElectShock>(), 120);
                }
            }
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }

        public override void UpdateDead()
        {
            electShock = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (electShock)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;

                Player.lifeRegen -= 30;
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (electShock)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Electric, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    //Main.PlayerDrawDust.Add(dust);
                }
                r *= 0.1f;
                g *= 0.2f;
                b *= 0.7f;
                fullBright = true;
            }
        }
    }
}
