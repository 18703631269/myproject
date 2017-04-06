using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class SharpZip
    {

        private static bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {
                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));
                s.PutNextEntry(entry);
                s.Flush();
                filenames = Directory.GetFiles(FolderToZip);
                foreach (string file in filenames)
                {
                    fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(file)));
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            folders = Directory.GetDirectories(FolderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
                {
                    return false;
                }
            }
            return res;
        }
        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="FolderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="ZipedFile">压缩后的文件名，全路径格式</param>
        private static bool ZipFileDictory(string FolderToZip, string ZipedFile, int level)
        {
            bool res;
            if (!Directory.Exists(FolderToZip))
            {
                return false;
            }
            ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
            s.SetLevel(level);
            res = ZipFileDictory(FolderToZip, s, "");
            s.Finish();
            s.Close();
            return res;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        private static bool ZipFile(string FileToZip, string ZipedFile, int level)
        {
            if (!File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            FileStream ZipFile = null;
            ZipOutputStream ZipStream = null;
            ZipEntry ZipEntry = null;
            bool res = true;
            try
            {
                ZipFile = File.OpenRead(FileToZip);
                byte[] buffer = new byte[ZipFile.Length];
                ZipFile.Read(buffer, 0, buffer.Length);
                ZipFile.Close();

                ZipFile = File.Create(ZipedFile);
                ZipStream = new ZipOutputStream(ZipFile);
                ZipEntry = new ZipEntry(Path.GetFileName(FileToZip));
                ZipStream.PutNextEntry(ZipEntry);
                ZipStream.SetLevel(level);

                ZipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return res;
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="FileToZip">待压缩的文件目录</param>
        /// <param name="ZipedFile">生成的目标文件</param>
        /// <param name="level">6</param>
        public static bool Zip(String FileToZip, String ZipedFile, int level)
        {
            if (Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, level);
            }
            else if (File.Exists(FileToZip))
            {
                return ZipFile(FileToZip, ZipedFile, level);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 文件加密压缩
        /// </summary>
        /// <param name="filedir">要压缩文件目录</param>
        /// <param name="filepath">文件存放路径</param>
        /// <param name="password">压缩密码</param>
        /// <returns></returns>
        public void ZipFileMain(string filedir, string filepath, string password)
        {
            string[] filenames = Directory.GetFiles(filedir);

            ZipOutputStream s = new ZipOutputStream(File.Create(filepath));

            s.SetLevel(6); // 0 - store only to 9 - means best compression 

            s.Password = password;

            foreach (string file in filenames)
            {
                //打开压缩文件 
                FileStream fs = File.OpenRead(file);

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Array arr = file.Split('\\');
                string le = arr.GetValue(arr.Length - 1).ToString();
                ZipEntry entry = new ZipEntry(le);
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                fs.Close();
                s.PutNextEntry(entry);
                s.Write(buffer, 0, buffer.Length);
            }
            s.Finish();
            s.Close();
        }
    }
}
