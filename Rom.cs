namespace MetroidBrowser
{
	internal static class Rom
	{
		internal const int BankCount = 8;

		internal static byte[] Contents = Array.Empty<byte>();

		internal static int Address(int bank, int offset)
		{
			if(offset >= 0xC000)
				return ((bank * 0x4000) + offset) - 0xC000;

			return ((bank * 0x4000) + offset) - 0x8000;
		}
	}
}