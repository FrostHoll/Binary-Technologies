using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using Terraria.Audio;

namespace BinaryTechnologies.NPCs.Bosses.TechnoSphere
{
    [AutoloadBossHead]
    public class TechnoSphere : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 12;

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override string Texture => "BinaryTechnologies/NPCs/Bosses/TechnoSphere/SphereCore";

        public override string BossHeadTexture => "BinaryTechnologies/NPCs/Bosses/TechnoSphere/SphereCore_Head";

        public override void SetDefaults()
        {
            NPC.width = 64;
            NPC.height = 64;
            NPC.damage = 30;
            NPC.defense = 20;
            NPC.lifeMax = 2200;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.SpawnWithHigherTime(30);
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
            NPC.scale = 1.5f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue(BinaryTechnologies.TransPath + "Bestiary.TechnoSphere"))
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, 169);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X += Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y += Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }

        public bool SecondPhase
        {
            get => NPC.ai[0] == 1f;
            set => NPC.ai[0] = value ? 1f : 0f;
        }

        public int ShieldLeft
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        public float MovingPosX
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public float MovingPosY
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public float MovingTimer
        {
            get => NPC.localAI[3];
            set => NPC.localAI[3] = value;
        }

        public float LaserTimer
        {
            get => NPC.localAI[1];
            set => NPC.localAI[1] = value;
        }

        public Vector2 Destination
        {
            get => new Vector2(NPC.ai[1], NPC.ai[2]);
            set
            {
                NPC.ai[1] = value.X;
                NPC.ai[2] = value.Y;
            }
        }

        public Vector2 LastDestination { get; set; } = Vector2.Zero;

        private float speed = 20f;

        private void SpawnShields()
        {
            float radius = 120f;

            if (ShieldLeft == 0 && !SecondPhase)
            {
                ShieldLeft = 4;
                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {

                    return;
                }


                int index;
                TechnoSphereShield shield;
                index = NPC.NewNPC(NPC.GetSource_FromAI(), 
                    (int)NPC.Center.X, 
                    (int)NPC.Center.Y, 
                    ModContent.NPCType<TechnoSphereShield>(), 
                    0, 
                    NPC.whoAmI);
                shield = Main.npc[index].ModNPC as TechnoSphereShield;
                if (shield != null)
                {
                    shield.Radius = new Vector2(radius, 0f);
                }
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
                index = NPC.NewNPC(NPC.GetSource_FromAI(), 
                    (int)NPC.Center.X, 
                    (int)NPC.Center.Y, 
                    ModContent.NPCType<TechnoSphereShield>(), 
                    0, 
                    NPC.whoAmI);
                shield = Main.npc[index].ModNPC as TechnoSphereShield;
                if (shield != null)
                {
                    shield.Radius = new Vector2(-radius, 0f);
                }
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
                index = NPC.NewNPC(NPC.GetSource_FromAI(), 
                    (int)NPC.Center.X, 
                    (int)NPC.Center.Y, 
                    ModContent.NPCType<TechnoSphereShield>(), 
                    0, 
                    NPC.whoAmI);
                shield = Main.npc[index].ModNPC as TechnoSphereShield;
                if (shield != null)
                {
                    shield.Radius = new Vector2(0f, radius);
                }
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
                index = NPC.NewNPC(NPC.GetSource_FromAI(), 
                    (int)NPC.Center.X, 
                    (int)NPC.Center.Y, 
                    ModContent.NPCType<TechnoSphereShield>(), 
                    0, 
                    NPC.whoAmI);
                shield = Main.npc[index].ModNPC as TechnoSphereShield;
                if (shield != null)
                {
                    shield.Radius = new Vector2(0f, -radius);
                }
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        private void MovingAround()
        {
            if (!NPC.HasValidTarget) return;



            MovingTimer--;
            if (MovingTimer < 0f)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float YOffset = 200f;
                    float XMaxOffset = 300f;
                    MovingPosX = Main.rand.NextFloat(Main.player[NPC.target].position.X - XMaxOffset,
                        Main.player[NPC.target].position.X + XMaxOffset);
                    MovingPosY = Main.player[NPC.target].position.Y - YOffset;
                    if (!SecondPhase) MovingTimer = 180f;
                    else MovingTimer = 60f;
                    //Main.NewText(MovingPosX + " " + MovingPosY);
                    NPC.netUpdate = true;
                }
            }
            Vector2 destinationVector = Destination - NPC.Center;

            float movingSpeed = Math.Min(200f, destinationVector.Length()) / speed;

            NPC.velocity = destinationVector.SafeNormalize(Vector2.Zero) * movingSpeed;

            if (Destination != LastDestination)
            {
                NPC.TargetClosest();
                if (Main.netMode != NetmodeID.Server)
                {
                    NPC.position += NPC.netOffset;

                    Dust.QuickDustLine(NPC.Center + destinationVector.SafeNormalize(Vector2.Zero) * NPC.width, Destination, destinationVector.Length() / 20f, Color.Yellow);

                    NPC.position -= NPC.netOffset;
                }
            }
            LastDestination = Destination;
        }

        private void ShootLaser()
        {
            if (SecondPhase)
            {
                Player player = Main.player[NPC.target];

                if (LaserTimer <= 0f)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {

                        Vector2 projDirection = Vector2.Normalize(player.position - NPC.Center) * 8f;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(),
                            new Vector2(NPC.Center.X, NPC.Center.Y),
                            projDirection, ProjectileID.DeathLaser,
                            NPC.damage / 4, 0f, Main.myPlayer);
                        LaserTimer = Main.expertMode ? 45f : (Main.masterMode ? 30f : 60f);
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    LaserTimer--;
                }
            }
        }

        public static int MinionType()
        {
            return ModContent.NPCType<NPCs.Bosses.TechnoSphere.TechnoSphereShield>();
        }

        private void CheckShields()
        {
            if (SecondPhase)
            {
                return;
            }

            float sum = 0f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC otherNPC = Main.npc[i];
                if (otherNPC.active && otherNPC.type == MinionType() && otherNPC.ModNPC is TechnoSphereShield minion)
                {
                    if (minion.ParentIndex == NPC.whoAmI)
                    {
                        sum++;
                    }
                }

            }

            ShieldLeft = (int)sum;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
        }

        private void Rotate()
        {
            if (SecondPhase)
            {
                NPC.rotation -= MathHelper.ToRadians(4f);
            }
            else NPC.rotation -= MathHelper.ToRadians(2f);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.5f;
            float frameSpeed = 3;

            int startFrame = 0;
            int finalFrame = 11;

            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }

        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (Main.rand.NextBool(5)) Dust.NewDust(NPC.position, NPC.Hitbox.Width, NPC.Hitbox.Height, 131, 0f, 0f, 255, new Color(177, 255, 0), 0.8f);

            Rotate();

            CheckShields();

            if (player.dead)
            {
                NPC.velocity.Y -= 0.04f;
                NPC.velocity.X = 0f;
                NPC.EncourageDespawn(10);
                return;
            }

            if (!SecondPhase)
            {
                NPC.defense = 9999;
            }

            if (ShieldLeft == 0 && MovingPosX != 0f && !SecondPhase)
            {
                SecondPhase = true;
                SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                speed /= 2;
                NPC.defense = 20;
                NPC.netUpdate = true;
            }

            SpawnShields();

            //NPC.dontTakeDamage = !SecondPhase;

            MovingAround();

            ShootLaser();
        }

        public void ShieldKilled()
        {
            Main.NewText($"Shield killed. Left: {ShieldLeft}");
            ShieldLeft--;
            if(Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
        }



        public override void OnKill()
        {
            
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule nonExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            nonExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.ElectMaterial>(), 3, 1, 1));
            nonExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.ByteShard>(), 1, 2, 4));
            npcLoot.Add(nonExpertRule);

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Items.TechnoSphereBag>()));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.TechnoSphereRelic>()));
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<Pets.TechnoSpherePet.TechnoSpherePetItem>(), 4));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            base.BossLoot(ref name, ref potionType);
        }
    }
}