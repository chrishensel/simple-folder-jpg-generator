// The MIT License(MIT)
// 
// Copyright(c) 2016 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using TagLib;

namespace FolderJpgCreator
{
    class Program
    {
        #region Constants

        private const string FolderJpgFileName = "folder.jpg";
        private const string WildcardMp3 = "*.mp3";
        private const string WildcardFlac = "*.flac";

        #endregion

        #region Methods

        static void Main(string[] args)
        {
            int filesProcessed = 0;
            int filesWritten = 0;
            int filesSkipped = 0;

            Console.WriteLine(Properties.Resources.ProcessRequestInputPath);
            Console.Write("> ");

            string path = Console.ReadLine();

            Console.WriteLine(Properties.Resources.ProcessBegin);

            DirectoryInfo dir = new DirectoryInfo(path);

            /* "Supported" extensions. Actually we support a lot more (via TagLibSharp).
             * However these are sufficient for me. You may add any other extension you like.
             */
            IList<string> extensions = new List<string>();
            extensions.Add(WildcardMp3);
            extensions.Add(WildcardFlac);

            foreach (string ext in extensions)
            {
                Console.WriteLine(Properties.Resources.ProcessingWildcardNow, ext);

                IEnumerable<FileInfo> files = dir.EnumerateFiles(ext, SearchOption.AllDirectories);

                foreach (FileInfo file in files)
                {
                    filesProcessed++;

                    DirectoryInfo fileDir = file.Directory;

                    if (fileDir.GetFiles(FolderJpgFileName, SearchOption.TopDirectoryOnly).Length > 0)
                    {
                        filesSkipped++;
                        continue;
                    }

                    var taggedFile = TagLib.File.Create(file.FullName);

                    IPicture[] pictures = taggedFile.Tag.Pictures;

                    if (pictures.Length > 0)
                    {
                        IPicture pic = pictures[0];

                        string newFolderJpgPath = Path.Combine(fileDir.FullName, FolderJpgFileName);

                        /* We assume that it's a JPEG all the time. Anything else should be converted to JPEG.
                         * I've seen cover images that have image/png as well, although rare?
                         */
                        if (pic.MimeType != MediaTypeNames.Image.Jpeg)
                        {
                            Console.WriteLine(Properties.Resources.ErrorPictureNotSupported, fileDir.FullName.Replace(path, string.Empty));
                            continue;
                        }

                        /* Hint: For example, the Windows Media Player did create the folder.jpg file as a *hidden* file.
                         * This seems inconvenient when wanting to getting rid of it. For this reason, the file is created regularly.
                         */
                        using (FileStream fs = new FileStream(newFolderJpgPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            fs.Write(pic.Data.Data, 0, pic.Data.Data.Length);
                        }

                        filesWritten++;
                    }
                }
            }

            Console.WriteLine(Properties.Resources.ProcessFinished, filesProcessed, filesSkipped, filesWritten);
            Console.WriteLine("Press any key to exit . . .");

            Console.ReadKey();
        }

        #endregion
    }
}
