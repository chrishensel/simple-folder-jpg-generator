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
using System.Linq;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
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

            ImageFactory fac = new ImageFactory();
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
                    FileInfo folderJpg = new FileInfo(Path.Combine(fileDir.FullName, FolderJpgFileName));

                    if (folderJpg.Exists)
                    {
                        filesSkipped++;
                        continue;
                    }

                    var taggedFile = TagLib.File.Create(file.FullName);

                    IPicture[] pictures = taggedFile.Tag.Pictures;
                    IPicture pic = pictures.FirstOrDefault(_ => _.Type == PictureType.FrontCover) ?? pictures.FirstOrDefault();

                    if (pic == null)
                    {
                        continue;
                    }

                    using (MemoryStream srcImgStream = new MemoryStream(pic.Data.Data))
                    {
                        fac.Load(srcImgStream);
                        fac.Format(new JpegFormat());

                        using (FileStream fs = new FileStream(folderJpg.FullName, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            fac.Save(fs);
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
