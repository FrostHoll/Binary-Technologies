using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace BinaryTechnologies
{
    public class BinaryTechnologiesNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool electShock = false;

        public override void ResetEffects(NPC npc)
        {
            electShock = false;
            base.ResetEffects(npc);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            base.UpdateLifeRegen(npc, ref damage);

            if (electShock)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 30;
                if (damage < 2)
                {
                    damage = 2;
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (electShock)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
        }

    }
}
