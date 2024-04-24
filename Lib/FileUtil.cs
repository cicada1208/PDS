using Microsoft.Win32;
using System;
using System.IO;
using System.Text;

namespace Lib
{
    public class FileUtil
    {
        /// <summary>
        /// 讀取文字檔
        /// </summary>
        public string ReadFile(string path)
        {
            string result = string.Empty;
            using (var sr = new StreamReader(path, Encoding.Default))
                result = sr.ReadToEnd();
            return result;
        }

        /// <summary>
        /// 寫入文字檔
        /// </summary>
        public void WriteFile(string content, string fileName = "", string title = Params.MsgParam.TitleExportFile, string filter = "txt")
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Files(*.{filter})|*.{filter}";
            saveFileDialog.AddExtension = true;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = fileName;
            saveFileDialog.Title = title;
            if (saveFileDialog.ShowDialog() != true) return;

            using (var sw = new StreamWriter(saveFileDialog.FileName, true, Encoding.Default))
                sw.WriteLine(content);
        }

    }
}