using System.Net;

namespace MetroidBrowser
{
	internal class RomMap
	{
		internal const int MapWidth = 32;
		internal const int MapHeight = 32;
		//internal const int MapAddress = 0x253E;
		internal const int MapAddress = 0xA53E;
		internal const int MapBank = 0;

		internal const int AreaCount = 5;
		internal const int AreaAddress = 0x9598;

		internal const int RoomCount = 256;

		internal static readonly int[] AreaSpecialItems = new int[AreaCount];
		internal static readonly int[] AreaRooms = new int[AreaCount];
		internal static readonly int[] AreaStructures = new int[AreaCount];
		internal static readonly int[] AreaMacros = new int[AreaCount];
		internal static readonly int[] AreaEnemies = new int[AreaCount];
		internal static readonly int[] AreaEnemies2 = new int[AreaCount];
		internal static readonly int[] AreaEnemyPlacement = new int[AreaCount];
		internal static readonly int[] AreaEnemyAnimations = new int[AreaCount];

		internal static void Load()
		{
			var address = Rom.Address(MapBank, MapAddress);

			for (int y = 0; y < MapHeight; y++)
			{
				for (int x = 0; x < MapWidth; x++)
				{
					var value = Rom.Contents[address++];

					Map.Rooms[x, y] = value;
				}
			}
				
			for (int x = 0; x < AreaCount; x++)
			{
				address = Rom.Address(AreaBanks[x], AreaAddress);

				AreaSpecialItems[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				AreaRooms[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				AreaStructures[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				AreaMacros[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				AreaEnemies[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				AreaEnemies2[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				AreaEnemyPlacement[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
				AreaEnemyAnimations[x] = Rom.Contents[address++] | (Rom.Contents[address++] << 8);
			}
		}

		internal static void LoadRoom(int area, int room)
		{
			var address = Rom.Address(AreaBanks[area], AreaRooms[area]) + (room << 1);

			address = Rom.Contents[address] | (Rom.Contents[address + 1] << 8);

			address = Rom.Address(AreaBanks[area], address);

			Room.Color = Rom.Contents[address++];

			var locations = new List<int>();
			var structures = new List<int>();
			var colors = new List<int>();

			while (Rom.Contents[address] != 0xFD)
			{
				locations.Add(Rom.Contents[address++]);
				structures.Add(Rom.Contents[address++]);
				colors.Add(Rom.Contents[address++]);
			}

			Room.ObjectLocations = locations.ToArray();
			Room.ObjectStructures = structures.ToArray();
			Room.ObjectColors = colors.ToArray();

			address++;

			Room.Doors = new int[] { -1, -1 };

			while (Rom.Contents[address] == 0x02)
			{
				address++;
				var door = Rom.Contents[address++];

				var index = door >> 4 - 0x0a;
				var type = door & 0x0F;

				Room.Doors[index] = type;
			}

			var sprites = new List<int>();
			var enemies = new List<int>();
			locations = new List<int>();

			while (Rom.Contents[address] != 0xFF)
			{
				sprites.Add(Rom.Contents[address++]);
				enemies.Add(Rom.Contents[address++]);
				locations.Add(Rom.Contents[address++]);
			}

			Room.EnemySprites = sprites.ToArray();
			Room.EnemyTypes = enemies.ToArray();
			Room.EnemyLocations = enemies.ToArray();
		}

		internal static void LoadStructure(int area, int structure)
		{
			var address = Rom.Address(AreaBanks[area], AreaStructures[area]) + (structure << 1);

			address = Rom.Contents[address] | (Rom.Contents[address + 1] << 8);

			address = Rom.Address(AreaBanks[area], address);

			Structure.Columns = Rom.Contents[address++];

			var tiles = new List<int>();

			while (Rom.Contents[address] != 0xFF)
				tiles.Add(Rom.Contents[address++]);

			Structure.Tiles = tiles.ToArray();
		}

		internal static readonly string[] AreaNames = new string[]
		{
			"Brinstar",
			"Norfair",
			"Tourian",
			"Kraid",
			"Ridley"
		};

		internal static readonly int[] AreaBanks = new int[]
		{
			1,
			2,
			3,
			4,
			5
		};
	}
}