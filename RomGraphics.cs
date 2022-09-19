using System.Net;

namespace MetroidBrowser
{
	internal class RomGraphics
	{
		internal const int BlockCount = 29;
		internal const int BlockTableBank = 7;
		internal const int BlockTableAddress = 0xC6E0;

		internal const int AreaPaletteAddress = 0x9560;

		internal static readonly int[] BlockBanks = new int[BlockCount];
		internal static readonly int[] BlockRomAddresses = new int[BlockCount];
		internal static readonly int[] BlockPpuAddresses = new int[BlockCount];
		internal static readonly int[] BlockLengths = new int[BlockCount];

		internal static void Load()
		{
			var address = Rom.Address(BlockTableBank, BlockTableAddress);

			for (int x = 0; x < BlockCount; x++)
			{
				BlockBanks[x] = Rom.Contents[address++];
				BlockRomAddresses[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				BlockPpuAddresses[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				BlockLengths[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
			}
		}

		internal static void LoadArea(int area)
		{
			foreach (var block in AreaBlocks[area])
				LoadBlock(block);

			LoadPalette(area, 0);
		}

		internal static void LoadBlock(int block)
		{
			var source = Rom.Address(BlockBanks[block], BlockRomAddresses[block]);
			var destination = BlockPpuAddresses[block];

			Array.Copy(Rom.Contents, source, Ppu.Vram, destination, BlockLengths[block]);
		}

		internal static void LoadPalette(int area, int palette)
		{
			var address = Rom.Address(RomMap.AreaBanks[area], AreaPaletteAddress) + (palette << 1);

			address = Rom.Contents[address] | (Rom.Contents[address + 1] << 8);

			address = Rom.Address(RomMap.AreaBanks[area], address);

			var destination = (Rom.Contents[address++] << 8) | Rom.Contents[address++];
			var length = Rom.Contents[address++];

			Array.Copy(Rom.Contents, address, Ppu.Vram, destination, length);
		}

		internal static int Address(int block, int offset)
		{
			return Rom.Address(BlockBanks[block], BlockRomAddresses[block]) - BlockPpuAddresses[block] + offset;
		}

		internal static readonly int[] TitleBlocks = new int[] { 21 };
		internal static readonly int[] SamusBlocks = new int[] { 0, 27, 20, 23, 24, 25, 22 };

		internal static readonly int[] BrinstarBlocks = new int[] { 3, 4, 5, 6, 25, 22 };
		internal static readonly int[] NorfairBlocks = new int[] { 4, 5, 7, 8, 9, 19, 16 };
		internal static readonly int[] TourianBlocks = new int[] { 5, 10, 11, 12, 13, 14, 26, 28, 25, 22 };
		internal static readonly int[] KraidBlocks = new int[] { 4, 5, 10, 15, 16, 17, 25, 22 };
		internal static readonly int[] RidleyBlocks = new int[] { 4, 5, 10, 18, 19, 25, 22 };

		internal static readonly int[] EndGameBlocks = new int[] { 1, 2, 25, 22 };
		internal static readonly int[] PasswordBlocks = new int[] { 23, 22 };

		internal static readonly int[][] AreaBlocks = new int[][]
		{
			BrinstarBlocks,
			NorfairBlocks,
			TourianBlocks,
			KraidBlocks,
			RidleyBlocks
		};
	}
}