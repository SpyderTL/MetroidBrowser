namespace MetroidBrowser
{
	internal static class SongReader
	{
		internal static int[] Position = new int[4];
		internal static CommandType Command;
		internal static int Note;
		internal static int Loop;
		internal static int Duration;

		internal static bool ReadSquare()
		{
			if (Position[0] >= Song.Square.Length)
				return false;

			Read(Song.Square[Position[0]]);

			Position[0]++;

			return true;
		}

		internal static bool ReadSquare2()
		{
			if (Position[1] >= Song.Square2.Length)
				return false;

			Read(Song.Square2[Position[1]]);

			Position[1]++;

			return true;
		}

		internal static bool ReadTriangle()
		{
			if (Position[2] >= Song.Triangle.Length)
				return false;

			Read(Song.Triangle[Position[2]]);

			Position[2]++;

			return true;
		}

		internal static bool ReadNoise()
		{
			if (Position[3] >= Song.Noise.Length)
				return false;

			var command = Song.Noise[Position[3]];

			if (command == 0x00)
				Command = CommandType.EndTrack;
			else if (command == 0xff)
				Command = CommandType.EndLoop;
			else if (command == 0x01)
				Command = CommandType.Rest;
			else if ((command & 0xc0) == 0xc0)
			{
				Command = CommandType.BeginLoop;
				Loop = command & 0x3f;
			}
			else if ((command & 0xf0) == 0xb0)
			{
				Command = CommandType.Duration;
				Duration = command & 0x0f;
			}
			else
			{
				Command = CommandType.Note;
				Note = command & 0x0f;
			}

			Position[3]++;

			return true;
		}

		private static void Read(byte command)
		{
			if (command == 0x00)
				Command = CommandType.EndTrack;
			else if (command == 0xff)
				Command = CommandType.EndLoop;
			else if (command == 0x02)
				Command = CommandType.Rest;
			else if ((command & 0xc0) == 0xc0)
			{
				Command = CommandType.BeginLoop;
				Loop = command & 0x3f;
			}
			else if ((command & 0xf0) == 0xb0)
			{
				Command = CommandType.Duration;
				Duration = command & 0x0f;
			}
			else
			{
				Command = CommandType.Note;
				Note = command >> 1;
			}
		}

		internal static void Reset()
		{
			Position = new int[4];
		}

		internal enum CommandType
		{
			EndTrack,
			EndLoop,
			BeginLoop,
			Duration,
			Rest,
			Note
		}
	}
}