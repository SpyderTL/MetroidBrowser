namespace MetroidBrowser
{
	internal class SongPlayer
	{
		internal static bool Playing;

		internal static event Action<int, int>? NoteOn;
		internal static event Action<int, int>? NoteOff;
		internal static event Action? Stopped;

		private static readonly System.Threading.Timer Timer = new(Timer_OnTick, null, Timeout.Infinite, Timeout.Infinite);

		private static int[] Delay = new int[4];
		private static int[] Duration = new int[4];
		private static int[] Note = new int[4];
		private static int[] LoopPosition = new int[4];
		private static int[] LoopCounter = new int[4];
		private static long Last = 0;

		internal static void Play()
		{
			Delay = new int[4];
			Duration = new int[4];
			Note = Enumerable.Repeat(-1, 4).ToArray();
			LoopPosition = Enumerable.Repeat(-1, 4).ToArray();
			LoopCounter = new int[4];
			Last = Environment.TickCount64;

			SongReader.Reset();

			Timer.Change(10, 10);

			Playing = true;
		}

		internal static void Stop()
		{
			Timer.Change(Timeout.Infinite, Timeout.Infinite);

			for (int i = 0; i < 4; i++)
			{
				if (Note[i] != -1)
					NoteOff?.Invoke(i, Note[i]);
			}

			Playing = false;

			Stopped?.Invoke();
		}

		private static void Timer_OnTick(object? state)
		{
			Timer.Change(Timeout.Infinite, Timeout.Infinite);

			if (!Playing)
				return;

			var tickCount = Environment.TickCount64;
			var elapsed = tickCount - Last;

			for(int i = 0; i < Delay.Length; i++)
				Delay[i] -= (int)elapsed;

			while (Delay[0] <= 0 && Playing)
			{
				if (!SongReader.ReadSquare())
					break;

				Read(0);
			}

			while (Delay[1] <= 0 && Playing)
			{
				if (!SongReader.ReadSquare2())
					break;

				Read(1);
			}

			while (Delay[2] <= 0 && Playing)
			{
				if (!SongReader.ReadTriangle())
					break;

				Read(2);
			}

			while (Delay[3] <= 0 && Playing)
			{
				if (!SongReader.ReadNoise())
					break;

				Read(3);
			}

			Last = tickCount;

			if(Playing)
				Timer.Change(10, 10);
		}

		private static void Read(int channel)
		{
			switch (SongReader.Command)
			{
				case SongReader.CommandType.Note:
					if (Note[channel] != -1)
						NoteOff?.Invoke(channel, Note[channel]);
					NoteOn?.Invoke(channel, SongReader.Note);
					Delay[channel] += RomSongs.Duration[Song.DurationOffset + Duration[channel]] * 17;
					Note[channel] = SongReader.Note;
					break;

				case SongReader.CommandType.Rest:
					if (Note[channel] != -1)
						NoteOff?.Invoke(channel, Note[channel]);
					Delay[channel] += RomSongs.Duration[Song.DurationOffset + Duration[channel]] * 17;
					Note[channel] = -1;
					break;

				case SongReader.CommandType.Duration:
					Duration[channel] = SongReader.Duration;
					break;

				case SongReader.CommandType.BeginLoop:
					LoopPosition[channel] = SongReader.Position[channel];
					LoopCounter[channel] = SongReader.Loop;
					break;

				case SongReader.CommandType.EndLoop:
					LoopCounter[channel]--;

					if (LoopCounter[channel] > 0)
					{
						SongReader.Position[channel] = LoopPosition[channel];
					}
					break;

				case SongReader.CommandType.EndTrack:
					if (!Song.Loop)
						Stop();

					SongReader.Reset();
					break;
			}
		}
	}
}