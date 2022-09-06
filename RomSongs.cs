using System.Net;

namespace MetroidBrowser
{
	internal class RomSongs
	{
		internal const int SongCount = 12;
		internal const int DurationCount = 64;
		internal const int SongTableAddress = 0xBBFA;
		internal const int HeaderTableAddress = 0xBD31;
		internal const int DurationTableAddress = 0xBEF7;

		internal static int[] Duration = new int[DurationCount];

		internal static int[] DurationOffset = new int[SongCount];
		internal static int[] Loop = new int[SongCount];
		internal static int[] TriangleReleaseEnable = new int[SongCount];
		internal static int[] TriangleReleaseDisable = new int[SongCount];
		internal static int[] SquareEnvelope = new int[SongCount];
		internal static int[] SquareEnvelope2 = new int[SongCount];
		internal static int[] SquareAddress = new int[SongCount];
		internal static int[] SquareAddress2 = new int[SongCount];
		internal static int[] TriangleAddress = new int[SongCount];
		internal static int[] NoiseAddress = new int[SongCount];

		internal static void Load()
		{
			for (int duration = 0; duration < DurationCount; duration++)
				Duration[duration] = Rom.Contents[Rom.Address(0, DurationTableAddress) + duration];

			for (int song = 0; song < SongCount; song++)
			{
				var songAddress = Rom.Address(SongBanks[song], SongTableAddress) + song;
				var headerOffset = Rom.Contents[songAddress];
				var headerAddress = Rom.Address(SongBanks[song], HeaderTableAddress) + headerOffset;

				var durationOffset = Rom.Contents[headerAddress];
				var loop = Rom.Contents[headerAddress + 1];
				var release = Rom.Contents[headerAddress + 2];
				var squareEnvelope = Rom.Contents[headerAddress + 3];
				var squareEnvelope2 = Rom.Contents[headerAddress + 4];
				var squareAddress = Rom.Contents[headerAddress + 5] | (Rom.Contents[headerAddress + 6] << 8);
				var squareAddress2 = Rom.Contents[headerAddress + 7] | (Rom.Contents[headerAddress + 8] << 8);
				var triangleAddress = Rom.Contents[headerAddress + 9] | (Rom.Contents[headerAddress + 10] << 8);
				var noiseAddress = Rom.Contents[headerAddress + 11] | (Rom.Contents[headerAddress + 12] << 8);

				DurationOffset[song] = durationOffset;
				Loop[song] = loop;
				TriangleReleaseEnable[song] = release & 0x0f;
				TriangleReleaseDisable[song] = release >> 4;
				SquareEnvelope[song] = squareEnvelope;
				SquareEnvelope2[song] = squareEnvelope2;
				SquareAddress[song] = squareAddress;
				SquareAddress2[song] = squareAddress2;
				TriangleAddress[song] = triangleAddress;
				NoiseAddress[song] = noiseAddress;
			}
		}

		internal static void LoadSong(int song)
		{
			Song.DurationOffset = DurationOffset[song];
			Song.Loop = Loop[song] != 0;
			Song.Envelope = SquareEnvelope[song];
			Song.Envelope2 = SquareEnvelope2[song];

			var address = Rom.Address(SongBanks[song], SquareAddress[song]);
			var commands = new List<byte>();

			while (true)
			{
				var command = Rom.Contents[address++];

				commands.Add(command);

				if (command == 0x00)
					break;
			}

			Song.Square = commands.ToArray();

			address = Rom.Address(SongBanks[song], SquareAddress2[song]);
			commands.Clear();

			while (true)
			{
				var command = Rom.Contents[address++];

				commands.Add(command);

				if (command == 0x00)
					break;
			}

			Song.Square2 = commands.ToArray();

			address = Rom.Address(SongBanks[song], TriangleAddress[song]);
			commands.Clear();

			while (true)
			{
				var command = Rom.Contents[address++];

				commands.Add(command);

				if (command == 0x00)
					break;
			}

			Song.Triangle = commands.ToArray();

			if (NoiseAddress[song] != 0)
			{
				address = Rom.Address(SongBanks[song], NoiseAddress[song]);
				commands.Clear();

				while (true)
				{
					var command = Rom.Contents[address++];

					commands.Add(command);

					if (command == 0x00)
						break;
				}

				Song.Noise = commands.ToArray();
			}
			else
				Song.Noise = Array.Empty<byte>();
		}

		internal static readonly string[] SongNames = new string[]
		{
			"Ridley",
			"Tourian",
			"Item",
			"Kraid",
			"Norfair",
			"Escape",
			"Mother Brain",
			"Brinstar",
			"Start",
			"Item 2",
			"Ending",
			"Title"
		};

		internal static readonly int[] SongBanks = new int[]
		{
			4,
			0,
			0,
			4,
			2,
			3,
			3,
			1,
			0,
			0,
			0,
			0
		};
	}
}