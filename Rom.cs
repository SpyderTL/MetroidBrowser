namespace MetroidBrowser
{
	internal static class Rom
	{
		internal const int BankCount = 6;

		internal static byte[] Contents = Array.Empty<byte>();

		internal static int Address(int bank, int offset)
		{
			return ((bank * 0x4000) + offset) - 0x8000;
		}
	}
}