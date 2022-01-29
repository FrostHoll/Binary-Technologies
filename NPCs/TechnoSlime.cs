using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace BinaryTechnologies.NPCs
{
    public class TechnoSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Slime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];

        }

        public override void SetDefaults()
        {            
            NPC.width = 24;
            NPC.height = 18;
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.damage = 9;
            NPC.defense = 3;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.alpha = 40;
            NPC.value = Item.buyPrice(copper: 40);
            AnimationType = NPCID.BlueSlime;
            AIType = NPCID.BlueSlime;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue(BinaryTechnologies.TransPath + "Bestiary.BrokenRobot"))
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDaySlime.Chance * 0.2f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Slime, newColor: Color.Orange, Scale: 0.8f);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X += Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y += Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.EnergyCore>(), 8, 1, 1));
        }

        public float ShootTimer
        {
            get => NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }

        private static float shootingRange = 150f;

        public override bool PreAI()
        {
            if (NPC.HasPlayerTarget)
            {
                Vector2 toPlayer = Main.player[NPC.target].position - NPC.position;
                float distance = toPlayer.Length();
                if (distance < shootingRange)
                {
                    ShootTimer--;

                    if (ShootTimer < 0f)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            SoundEngine.PlaySound(SoundID.Item17, NPC.position);
                            Vector2 projVector = -Vector2.UnitY * 4f;
                            projVector = projVector.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-15f, 15f)));
                            Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), NPC.Top, projVector, ModContent.ProjectileType<Projectiles.SlimeProjectile>(), NPC.damage / 2, 0.5f, Main.myPlayer);
                        }                        
                        ShootTimer = 120f;
                    }
                    NPC.velocity.X = 0f;
                    return false;
                }
            }

            return true;
        }
    }
}