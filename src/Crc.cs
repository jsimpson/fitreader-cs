namespace FitReader
{
    internal static class Crc
    {
        private static int[] crcTable = new int[16]
        {
            0x0000,
            0xcc01,
            0xd801,
            0x1400,
            0xf001,
            0x3c00,
            0x2800,
            0xe401,
            0xa001,
            0x6c00,
            0x7800,
            0xb401,
            0x5000,
            0x9c01,
            0x8801,
            0x4400,
        };

        internal static int Calculate(EndianBinaryReader binaryReader, int start, int end)
        {
            var crc = 0;

            for (var i = start; i < end; i++)
            {
                var b = binaryReader.ReadUInt8();

                var tmp = crcTable[crc & 0xF];

                crc = (crc >> 4) & 0x0FFF;
                crc = crc ^ tmp ^ crcTable[b & 0xF];

                tmp = crcTable[crc & 0xF];

                crc = (crc >> 4) & 0x0FFF;
                crc = crc ^ tmp ^ crcTable[(b >> 4) & 0xF];
            }

            return crc;
        }
    }
}