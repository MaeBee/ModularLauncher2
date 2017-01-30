using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ModularLauncherUtil
{
    public class Zipper
    {
        /// <summary>
        /// Unpacks a zip file to the specified directory.
        /// </summary>
        /// <param name="SourceFile">The full path to the source file.</param>
        public static void Unzip(string SourceFile)
        {
            Unzip(SourceFile, SourceFile.Substring(0, SourceFile.LastIndexOf("\\")));
        }

        /// <summary>
        /// Unpacks a zip file to the specified directory.
        /// </summary>
        /// <param name="SourceFile">The full path to the source file.</param>
        /// <param name="TargetPath">The path to extract the contents of the zip file to.</param>
        public static void Unzip(string SourceFile, string TargetPath)
        {
            string pTargetPath = TargetPath;
            if (!pTargetPath.EndsWith("\\"))
            {
                pTargetPath += "\\";
            }

            if (!Directory.Exists(pTargetPath))
            {
                Directory.CreateDirectory(pTargetPath);
            }

            using (ZipInputStream s = new ZipInputStream(File.OpenRead(SourceFile)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(pTargetPath + directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(pTargetPath + theEntry.Name))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Zip(string SourcePath, string TargetFile)
        {

        }
    }
}
