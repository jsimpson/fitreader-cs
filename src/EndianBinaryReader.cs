using System;
using System.IO;

namespace FitReader
{
    public class EndianBinaryReader
    {
        BinaryReader binaryReader;

        int position { get; set; }

        public int Position
        {
            get
            {
                return this.position;
            }
        }

        public EndianBinaryReader(Stream input)
        {
            this.position = 0;
            this.binaryReader = new BinaryReader(input);
        }

        public char[] ReadChars(int length)
        {
            this.position += length;
            return this.binaryReader.ReadChars(length);
        }

        public sbyte ReadInt8()
        {
            var data = this.binaryReader.ReadSByte();
            this.position += 1;
            return data;
        }

        public byte ReadUInt8()
        {
            var data = this.binaryReader.ReadByte();
            this.position += 1;
            return data;
        }

        public short ReadInt16(bool isLittleEndian = false)
        {
            this.position += 2;

            if (isLittleEndian)
            {
                return this.binaryReader.ReadInt16();
            }
            else
            {
                byte[] buf = new byte[2];
                this.binaryReader.Read(buf, 0, 2);
                Array.Reverse(buf);

                return BitConverter.ToInt16(buf);
            }
        }

        public ushort ReadUInt16(bool isLittleEndian = false)
        {
            this.position += 2;

            if (isLittleEndian)
            {
                return this.binaryReader.ReadUInt16();
            }
            else
            {
                byte[] buf = new byte[2];
                this.binaryReader.Read(buf, 0, 2);
                Array.Reverse(buf);

                return BitConverter.ToUInt16(buf);
            }
        }

        public int ReadInt32(bool isLittleEndian = false)
        {
            this.position += 4;

            if (isLittleEndian)
            {
                return this.binaryReader.ReadInt32();
            }
            else
            {
                byte[] buf = new byte[4];
                this.binaryReader.Read(buf, 0, 4);
                Array.Reverse(buf);

                return BitConverter.ToInt32(buf);
            }
        }

        public uint ReadUInt32(bool isLittleEndian = false)
        {
            this.position += 4;

            if (isLittleEndian)
            {
                return this.binaryReader.ReadUInt32();
            }
            else
            {
                byte[] buf = new byte[4];
                this.binaryReader.Read(buf, 0, 4);
                Array.Reverse(buf);

                return BitConverter.ToUInt32(buf);
            }
        }
    }
}