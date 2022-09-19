namespace MetroidBrowser
{
	internal class Ppu
	{
		internal static byte[] Vram = new byte[1024 * 16];
		internal static byte[] Oam = new byte[256];

		internal const int BackgroundPaletteAddress = 0x3F00;
		internal const int SpritePaletteAddress = 0x3F10;

		internal static Color GetBackgroundColor(int palette, int value)
		{
			var address = BackgroundPaletteAddress;

			if (value != 0)
				address += (palette * 4) + value;

			var index = Vram[address];

			return Colors[index];
		}

		internal static readonly Color[] Colors = new Color[]
		{
			// 0x00
			Color.FromArgb(84,84,84),
			Color.FromArgb(0, 30, 116),
			Color.FromArgb(8, 16, 144),
			Color.FromArgb(48, 0, 136),
			Color.FromArgb(68, 0, 100),
			Color.FromArgb(92, 0, 48),
			Color.FromArgb(84, 4, 0),
			Color.FromArgb(60, 24, 0),
			Color.FromArgb(32, 42, 0),
			Color.FromArgb(8, 58, 0),
			Color.FromArgb(0, 64, 0),
			Color.FromArgb(0, 60, 0),
			Color.FromArgb(0, 50, 60),
			Color.FromArgb(0, 0, 0),
			Color.FromArgb(0, 0, 0),
			Color.FromArgb(0, 0, 0),

			// 0x10
			Color.FromArgb(152, 150, 152),
			Color.FromArgb(8, 76, 196),
			Color.FromArgb(48, 50, 236),
			Color.FromArgb(92, 30, 228),
			Color.FromArgb(136, 20, 176),
			Color.FromArgb(160, 20, 100),
			Color.FromArgb(152, 34, 32),
			Color.FromArgb(120, 60, 0),
			Color.FromArgb(84, 90, 0),
			Color.FromArgb(40, 114, 0),
			Color.FromArgb(8, 124, 0),
			Color.FromArgb(0, 118, 40),
			Color.FromArgb(0, 102, 120),
			Color.FromArgb(0, 0, 0),
			Color.FromArgb(0, 0, 0),
			Color.FromArgb(0, 0, 0),

			// 0x20
			Color.FromArgb(236, 238, 236),
			Color.FromArgb(76, 154, 236),
			Color.FromArgb(120, 124, 236),
			Color.FromArgb(176, 98, 236),
			Color.FromArgb(228, 84, 236),
			Color.FromArgb(236, 88, 180),
			Color.FromArgb(236, 106, 100),
			Color.FromArgb(212, 136, 32),
			Color.FromArgb(160, 170, 0),
			Color.FromArgb(116, 196, 0),
			Color.FromArgb(76, 208, 32),
			Color.FromArgb(56, 204, 108),
			Color.FromArgb(56, 180, 204),
			Color.FromArgb(60, 60, 60),
			Color.FromArgb(0, 0, 0),
			Color.FromArgb(0, 0, 0),

			// 0x30
			Color.FromArgb(236, 238, 236),
			Color.FromArgb(168, 204, 236),
			Color.FromArgb(188, 188, 236),
			Color.FromArgb(212, 178, 236),
			Color.FromArgb(236, 174, 236),
			Color.FromArgb(236, 174, 212),
			Color.FromArgb(236, 180, 176),
			Color.FromArgb(228, 196, 144),
			Color.FromArgb(204, 210, 120),
			Color.FromArgb(180, 222, 120),
			Color.FromArgb(168, 226, 144),
			Color.FromArgb(152, 226, 180),
			Color.FromArgb(160, 214, 228),
			Color.FromArgb(160, 162, 160),
			Color.FromArgb(0, 0, 0),
			Color.FromArgb(0, 0, 0)
		};
	}
}