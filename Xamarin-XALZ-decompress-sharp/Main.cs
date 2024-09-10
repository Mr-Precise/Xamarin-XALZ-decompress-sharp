using System;
using System.IO;
using System.Text;
using K4os.Compression.LZ4;

namespace Xamarin_XALZ_decompress_sharp
{
    class MainClass
    {
        private static void WriteUsageAndExit()
        {
            Console.WriteLine("How to run & use: Xamarin_XALZ_decompress_sharp.exe xamarin-compressed-input.dll xamarin-uncompressed-output.dll");
            Console.WriteLine("On linux/macOS: mono ./Xamarin_XALZ_decompress_sharp.exe xamarin-compressed-input.dll xamarin-uncompressed-output.dll");
            Environment.Exit(2);
        }

        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                WriteUsageAndExit();
            }

            string inputFilePath = args[0];
            string outputFilePath = args[1];
            byte[] expectedMagic = Encoding.ASCII.GetBytes("XALZ");

            byte[] data;
            var xalzFile = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
            data = new byte[xalzFile.Length];
            xalzFile.Read(data, 0, data.Length);

            // Check magic bytes
            if (!CompareArrays(data, 0, expectedMagic, 0, 4))
            {
                Console.WriteLine("Input file is invalid (does not contain expected magic bytes), aborting...");
                Environment.Exit(1);
            }

            // Extract header
            byte[] headerIndex = new byte[4];
            Array.Copy(data, 4, headerIndex, 0, 4);
            int uncompressedLength = BitConverter.ToInt32(data, 8);
            byte[] payload = new byte[data.Length - 12];
            Array.Copy(data, 12, payload, 0, payload.Length);

            Console.WriteLine($"Header index: {BitConverter.ToString(headerIndex)}");
            Console.WriteLine($"Size of compressed payload: {payload.Length} bytes");
            Console.WriteLine($"Uncompressed length according to header: {uncompressedLength} bytes");

            // Decompression
            byte[] decompressed = new byte[uncompressedLength];
            int decodedLength = LZ4Codec.Decode(payload, decompressed);

            if (decodedLength != uncompressedLength)
            {
                Console.WriteLine("Decompression failed or length does not match, aborting...");
                Environment.Exit(1);
            }

            // Write the result to a file
            var outputFile = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            outputFile.Write(decompressed, 0, decompressed.Length);

            Console.WriteLine("Result written to file: " + outputFile.Name);
        }

        // Comparing arrays
        private static bool CompareArrays(byte[] dataIn, int dataInOffset, byte[] magicBytes, int magicBytesOffset, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (dataIn[dataInOffset + i] != magicBytes[magicBytesOffset + i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
