using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace BinaryTechnologies.NPCs.Town
{
    [AutoloadHead]
    public class Ancestor : ModNPC
    {
        public override string Texture => "BinaryTechnologies/NPCs/Town/Ancestor";

        public override string HeadTexture => "BinaryTechnologies/NPCs/Town/AncestorHead";

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 24;
            NPC.height = 46;
            NPC.aiStyle = 7;
            NPC.damage = 20;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.DyeTrader;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancestor");
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 500;
            NPCID.Sets.AttackType[NPC.type] = 3;
            NPCID.Sets.AttackTime[NPC.type] = 15;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness.LikeBiome(BiomeID.NormalUnderground); 
            NPC.Happiness.DislikeBiome(BiomeID.Ocean); 
            NPC.Happiness.LoveBiome(BiomeID.Snow);
            NPC.Happiness.HateBiome(BiomeID.Desert);

            NPC.Happiness.HateNPC(NPCID.Demolitionist); 
            NPC.Happiness.DislikeNPC(NPCID.Guide); 
            NPC.Happiness.LikeNPC(NPCID.Cyborg);
            NPC.Happiness.LoveNPC(NPCID.Steampunker);
            NPC.Happiness.LoveNPC(NPCID.Mechanic);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue(BinaryTechnologies.TransPath + "Bestiary.Ancestor"))
            });
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(0, 2))
            {
                case 0:
                    return "Oleg";
                case 1:
                    return "Arthur";
                default:
                    return "Robert";
            }
        }

        public override string GetChat()
        {
            
            string Ancestor1 = Language.GetTextValue(BinaryTechnologies.TransPath + "Ancestor1");
            string Ancestor2 = Language.GetTextValue(BinaryTechnologies.TransPath + "Ancestor2");
            string Ancestor3 = Language.GetTextValue(BinaryTechnologies.TransPath + "Ancestor3");
            string Ancestor4 = Language.GetTextValue(BinaryTechnologies.TransPath + "Ancestor4");
            string Ancestor5 = Language.GetTextValue(BinaryTechnologies.TransPath + "Ancestor5");
            string Ancestor6 = Language.GetTextValue(BinaryTechnologies.TransPath + "Ancestor6");
            var guide = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
            if (guide != null && Main.rand.Next(4) == 0)
            {
                return Ancestor3;
            }
            if (!Main.dayTime && Main.rand.Next(3) == 0)
            {
                return Ancestor4;
            }
            switch (Main.rand.Next(4))
            {
                case 0:
                    return Ancestor1;
                case 1:
                    return Ancestor5;
                case 2:
                    return Ancestor6;
                default:
                    return Ancestor2;
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
        }

        private bool CheckByteShard()
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                // Player has to have either an ExampleItem or an ExampleBlock in order for the NPC to spawn
                if (player.inventory.Any(item => item.type == ModContent.ItemType<Items.ByteShard>()))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return (Main.hardMode && CheckByteShard());
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 20;
            randExtraCooldown = 25;
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 32;
            itemHeight = 32;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = ModContent.Request<Texture2D>("BinaryTechnologies/Items/Weapons/BitSword").Value;
            itemSize = 32;
            scale = 0.8f;
            if (NPC.spriteDirection == -1) offset = new Vector2(10f, 8f);
            else if (NPC.spriteDirection == 1) offset = new Vector2(-8f, 8f);
            else offset = Vector2.Zero;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            int sale = 2;
            if (NPC.downedMechBossAny) sale++;
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) sale++;
            if (NPC.downedPlantBoss) sale++;
            if (NPC.downedMoonlord) sale++;

            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.BitShard>());
            shop.item[nextSlot].shopCustomPrice = shop.item[nextSlot].value / sale;
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.ByteShard>());
            shop.item[nextSlot].shopCustomPrice = shop.item[nextSlot].value / (sale - 1);
            nextSlot++;

            if (NPC.downedMechBossAny)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.KilobyteShard>());
                shop.item[nextSlot].shopCustomPrice = shop.item[nextSlot].value / (sale - 2);
                nextSlot++;
            }

            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.MegabyteShard>());
                shop.item[nextSlot].shopCustomPrice = shop.item[nextSlot].value / (sale - 3);
                nextSlot++;
            }

            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.GigabyteShard>());
                shop.item[nextSlot].shopCustomPrice = shop.item[nextSlot].value / (sale - 4);
                nextSlot++;
            }
        }
    }
}
