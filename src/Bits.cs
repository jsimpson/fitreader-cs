using System.Collections.Generic;

namespace FitReader
{
    internal static class Bits
    {
        internal static IDictionary<long, long> Masks = new Dictionary<long, long>()
        {
            { 7, 0b10000000 },
            { 6, 0b01000000 },
            { 5, 0b00100000 },
            { 4, 0b00010000 },
            { 3, 0b00001000 },
            { 2, 0b00000100 },
            { 1, 0b00000010 },
            { 0, 0b00000001 }
        };

        public static byte ReadBit(byte data, byte position)
        {
            return (byte) ((data & Masks[position]) >> position);
        }

        public static byte ReadBits(byte data, int[] range)
        {
            long mask = 0;
            for (int i = range[0]; i >= range[1]; i--)
            {
                mask += Masks[i];
            }

            return (byte) ((data & mask) >> range[1]);
        }
    }
}