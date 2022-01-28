using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace BinaryTechnologies.NPCs.Bosses.TechnoSphere
{
    class TechnoSphereShield : ModNPC
    {
        public int ParentIndex
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public bool HasParent => ParentIndex > -1;

        public override string Texture => "BinaryTechnologies/NPCs/Bosses/TechnoSphere/SphereShield";

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 20;
            NPC.damage = 30;
            NPC.defense = 10;
            NPC.lifeMax = 1500;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = -1;
            NPC.friendly = false;
            NPC.scale = 1.7f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield");
            Main.npcFrameCount[Type] = 5;

            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,

                    BuffID.Confused
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue(BinaryTechnologies.TransPath + "Bestiary.TechnoSphereShield"))
            });
        }

        private int i = 0;
        public Vector2 radius = new Vector2(100f, 0f);

        public float LaserTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public override void AI()
        {
            if (!HasParent) return;

            if(!NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.TechnoSphere.TechnoSphere>()))
            {
                NPC.EncourageDespawn(5);
                NPC.velocity.Y -= 0.4f;
                return;
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                NPC.velocity.Y -= 0.04f;
                NPC.EncourageDespawn(10);
                return;
            }
            NPC sphere = Main.npc[ParentIndex];          
            Vector2 dist = sphere.Center - NPC.Center;
            if (sphere != null)
            {
                NPC.rotation = dist.AngleTo(new Vector2(1f, 0f)) + MathHelper.PiOver2;
                radius = radius.RotatedBy(MathHelper.ToRadians(1f));
                NPC.Center = sphere.Center + radius;
            }

            if (LaserTimer <= 0f)
            {
                Vector2 projDirection = Vector2.Normalize(player.position - NPC.Center) * 8f;
                Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.Center.X, NPC.Center.Y), projDirection, ProjectileID.DeathLaser, NPC.damage / 4, 0f, Main.myPlayer);
                LaserTimer = 120f + Main.rand.NextFloat(30f, 60f) * NPC.GetLifePercent();
                NPC.netUpdate = true;
            }
            else
            {
                LaserTimer--;
            }
        }

        public override void OnKill()
        {
            if (!HasParent) return;
            TechnoSphere sphere = Main.npc[ParentIndex].ModNPC as TechnoSphere;
            if (sphere != null)
            {
                sphere.ShieldKilled();
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, 169);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (LaserTimer > 10f)
            {
                NPC.frame.Y = 0;
                return;
            }
            

            float frameSpeed = 2;
            int startFrame = 1;
            int finalFrame = 4;

            NPC.frameCounter += 1d;

            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0d;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }

    }
}
