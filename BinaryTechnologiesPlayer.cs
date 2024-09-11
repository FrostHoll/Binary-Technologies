using BinaryTechnologies.Tiles;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace BinaryTechnologies
{
    public class BinaryTechnologiesPlayer : ModPlayer
    {
        public bool electpwrglove = false;
        public bool sniperPack = false;

        public bool electShock = false;

        public bool gigabyteBowBuff = false;

        public bool standingNearPortalState = false;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (BinaryTechnologies.temp.JustPressed)
            {

                foreach (TileEntity te in TileEntity.ByID.Values)
                {
                    if (te.type == ModContent.TileEntityType<TEPortal>())
                    {
                        TEPortal portal = (TEPortal)te;
                        Main.NewText(portal.PortalState);
                    }
                }
            }
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            electpwrglove = false;
            sniperPack = false;
            electShock = false;
            gigabyteBowBuff = false;
            if(Main.rand.NextBool(8))standingNearPortalState = false;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (electpwrglove && item.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(2))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.ElectShock>(), 120);
                }
            }
            base.OnHitNPCWithItem(item, target, hit, damageDone);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (electpwrglove && proj.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(2))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.ElectShock>(), 120);
                }
            }
            if (sniperPack && proj.DamageType == DamageClass.Ranged)
            {
                if (Main.rand.Next(1, 11) == 1)
                {
                    target.AddBuff(BuffID.Confused, 180);
                }
            }
            base.OnHitNPCWithProj(proj, target, hit, damageDone);
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
