using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using BinaryTechnologies.Tiles;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using Terraria.Utilities;

namespace BinaryTechnologies.NPCs.Town
{
    [AutoloadHead]
    public class Ancestor : ModNPC
    {
        public const string ShopName = "Shop";

        public override string Texture => "BinaryTechnologies/NPCs/Town/Ancestor";

        public override string HeadTexture => "BinaryTechnologies/NPCs/Town/AncestorHead";

        private const string DialoguePath = "Dialogue.Ancestor.";

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
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 500;
            NPCID.Sets.AttackType[NPC.type] = 3;
            NPCID.Sets.AttackTime[NPC.type] = 15;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Cyborg, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Steampunker, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Mechanic, AffectionLevel.Love)
            ;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue(BinaryTechnologies.TransPath + "Bestiary.Ancestor"))
            });
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                "Oleg",
                "Arthur",
                "Robert"
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add(Language.GetTextValue(BinaryTechnologies.TransPath + DialoguePath + "StandardDialogue1"));
            chat.Add(Language.GetTextValue(BinaryTechnologies.TransPath + DialoguePath + "StandardDialogue2"));
            chat.Add(Language.GetTextValue(BinaryTechnologies.TransPath + DialoguePath + "StandardDialogue3"));
            chat.Add(Language.GetTextValue(BinaryTechnologies.TransPath + DialoguePath + "StandardDialogue4"));

            var guide = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
            if (guide != null)
            {
                chat.Add(Language.GetTextValue(BinaryTechnologies.TransPath + DialoguePath + "GuideDialogue"));
            }

            if (!Main.dayTime)
            {
                chat.Add(Language.GetTextValue(BinaryTechnologies.TransPath + DialoguePath + "NightDialogue"));
            }

            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = ShopName;
            }
        }

        private bool CheckPortal()
        {
            foreach (TileEntity te in TileEntity.ByID.Values)
            {
                if (te.type == ModContent.TileEntityType<TEPortal>())
                {
                    TEPortal entity = te as TEPortal;
                    if (entity.PortalState > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return (CheckPortal());
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

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = ModContent.Request<Texture2D>("BinaryTechnologies/Items/Weapons/BitSword").Value;
            itemSize = 32;
            scale = 0.8f;
            if (NPC.spriteDirection == -1) offset = new Vector2(10f, 8f);
            else if (NPC.spriteDirection == 1) offset = new Vector2(-8f, 8f);
            else offset = Vector2.Zero;
        }

        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, ShopName)
                .Add<Items.BitShard>()
                .Add<Items.ByteShard>()
                .Add<Items.KilobyteShard>(Condition.DownedMechBossAny)
                .Add<Items.MegabyteShard>(Condition.DownedMechBossAll)
                .Add<Items.GigabyteShard>(Condition.DownedPlantera)
            ;

            npcShop.Register();
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            int sale = 2;
            if (NPC.downedMechBossAny) sale++;
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) sale++;
            if (NPC.downedPlantBoss) sale++;
            if (NPC.downedMoonlord) sale++;

            foreach (Item item in items)
            {
                if (item == null || item.type == ItemID.None) continue;

                if (item.type == ModContent.ItemType<Items.BitShard>())
                {
                    int value = item.shopCustomPrice ?? item.value;
                    item.shopCustomPrice = value / sale;
                }
                if (item.type == ModContent.ItemType<Items.ByteShard>())
                {
                    int value = item.shopCustomPrice ?? item.value;
                    item.shopCustomPrice = value / (sale - 1);
                }
                if (item.type == ModContent.ItemType<Items.KilobyteShard>())
                {
                    int value = item.shopCustomPrice ?? item.value;
                    item.shopCustomPrice = value / (sale - 2);
                }
                if (item.type == ModContent.ItemType<Items.MegabyteShard>())
                {
                    int value = item.shopCustomPrice ?? item.value;
                    item.shopCustomPrice = value / (sale - 3);
                }
                if (item.type == ModContent.ItemType<Items.GigabyteShard>())
                {
                    int value = item.shopCustomPrice ?? item.value;
                    item.shopCustomPrice = value / (sale - 4);
                }
            }
        }
    }
}
