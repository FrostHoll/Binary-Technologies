using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using BinaryTechnologies.NPCs;

namespace BinaryTechnologies.Tiles
{
    public class TEPortal : ModTileEntity
    {
        public bool stateChanged = false;
        private int _portalState = 0;

        public int PortalState
        {
            get => _portalState;
            set => _portalState = value < 2 && value >= 0 ? value : 0;
        }

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.IsActive && tile.type == ModContent.TileType<Tiles.TilePortal>() && tile.frameX == 0 && tile.frameY == 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("PortalState", _portalState);
        }

        public override void LoadData(TagCompound tag)
        {
            try
            {
                _portalState = tag.Get<int>("PortalState");
            }
            catch (System.Exception)
            {
                Main.NewText("PortalState not found");
            }

        }

        public override void Update()
        {
            if (stateChanged)
            {
                NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID, Position.X, Position.Y);
                stateChanged = false;
            }
            
        }


        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(_portalState);
        }

        public override void NetReceive(BinaryReader reader)
        {
            _portalState = reader.ReadInt32();
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                //maybe add SendData NPCSync?
                return -1;
            }
            return Place(i, j);
        }
    }

    public class TilePortal : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.Height = 10;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.Origin = new Point16(0, 5);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TEPortal>().Hook_AfterPlacement, -1, 0, true);
            TileObjectData.addTile(Type);
            AnimationFrameHeight = 180;
            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(200, 200, 200), name);
            DustType = DustID.Stone;
        }

        public int _portalState = 0;

        public override void NearbyEffects(int i, int j, bool closer)
        {
            //Main.NewText("in range");
            TEPortal entity = GetPortalEntity(i, j);
            _portalState = entity.PortalState;
            Main.LocalPlayer.GetModPlayer<BinaryTechnologiesPlayer>().standingNearPortalState = entity.PortalState != 0;
        }

        private TEPortal GetPortalEntity(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int left = i - tile.frameX / 18;
            int top = j - tile.frameY % AnimationFrameHeight / 18;
            int index = ModContent.GetInstance<TEPortal>().Find(left, top);
            if (index == -1)
            {
                Main.NewText("Portal Entity not found");
                throw new System.Exception("Portal Entity not found");
            }
            return (TEPortal)ModTileEntity.ByID[index];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameX / AnimationFrameHeight > 0)
            {
                
                r = 1f;
                g = 1f;
                b = 1f;
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {

            if (_portalState == 0)
            {
                frame = 0;
                return;
            }

            if (_portalState == 1)
            {
                if (++frameCounter >= 9)
                {
                    frameCounter = 0;

                    if (++frame > 2) frame = 1;
                }
            }
            
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;

            TEPortal entity = GetPortalEntity(i, j);
            int byteShard = ModContent.ItemType<Items.ByteShard>();
            if (entity.PortalState == 0 && player.ConsumeItem(byteShard))
            {
                entity.PortalState = 1;
                entity.stateChanged = true;
                Main.NewText("Portal was activated by Byte Shard!");
            }
            else
            {
                switch (entity.PortalState)
                {
                    case 0:
                        Main.NewText("Portal is not active.");
                        break;
                    case 1:
                        Main.NewText("Portal was activated by Byte Shard.");
                        break;
                    default:
                        Main.NewText("Something went wrong");
                        break;
                }
            }
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.cursorItemIconID = ModContent.ItemType<Items.ByteShard>();
            player.cursorItemIconText = "";
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeable.Portal>());
            TEPortal entity = GetPortalEntity(i, j);
            if (entity.PortalState > 0)
            {
                int shard;
                switch (entity.PortalState)
                {
                    case 1:
                        shard = ModContent.ItemType<Items.ByteShard>();
                        break;
                    default:
                        shard = ModContent.ItemType<Items.ByteShard>();
                        break;
                }
                Item.NewItem(i * 16, j * 16, 32, 16, shard);
            }
            ModContent.GetInstance<TEPortal>().Kill(i, j);
        }
    }
}
