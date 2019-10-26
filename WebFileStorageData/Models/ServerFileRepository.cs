using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace WebFileStorageData.Models
{
    public class ServerFileRepository : IFileRepository
    {
        public IList<string> FileNames { get; set; }
        public IList<string> FileData { get; set; }
        public string SelectedFile { get; set; }
        private string repository = "FileStorage";

        public ServerFileRepository()
        {
            string[] files = Directory.GetFiles(repository, "*.txt", SearchOption.AllDirectories);
            FileNames = new List<string>();
            foreach (string file in files)
            {
                FileNames.Add(file);
            }
            FileData = null;
            SelectedFile = FileNames.Count() != 0 ? FileNames.First() : null;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void GetFileData()
        {
            if (string.IsNullOrEmpty(SelectedFile)) return;

            var enc1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251);

            using (StreamReader fs = new StreamReader(SelectedFile, enc1251))
            {
                FileData = new List<string>();

                while (true)
                {
                    string temp = fs.ReadLine();
                    if (temp == null) break;
                    FileData.Add(temp);
                }
            }
        }

        public bool SetFileData(List<KeyValuePair<string, StringValues>> dataList)
        {
            if (dataList == null) return false;
            int nSelFile = dataList.FindIndex(m => m.Key == "selectedFile");
            if (nSelFile == -1) return false;
            SelectedFile = dataList[nSelFile].Value.ToString();
            GetFileData();
            ChangeFileData(dataList);
            var enc1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251);            

            using (StreamWriter writer = new StreamWriter(SelectedFile, false, enc1251))
            { 
                foreach (var line in FileData)
                {
                    string tt = line.Trim(' ');
                    writer.WriteLine(tt);
                }
            }
            return true;
        }

        void ChangeFileData(List<KeyValuePair<string, StringValues>> dataList)
        {
            int lineCurrent = 0, column = 0;

            foreach (var data in dataList)
            {
                var sCoord = data.Key.Split(',');

                if (!int.TryParse(sCoord[0], out lineCurrent) || !int.TryParse(sCoord[1], out column)) continue;
                while (lineCurrent >= FileData.Count())
                {
                    FileData.Add("\t\t");
                }

                string str = FileData[lineCurrent];
                List<string> elements = str.Split('\t').ToList();
                StringBuilder newStr = new StringBuilder();

                while(column > elements.Count() - 1)
                {
                    elements.Add("");
                }

                for(int i = 0; i < elements.Count(); i++)
                {
                    if (i != 0) newStr.Append("\t");
                    newStr.Append(i != column ? elements[i] : data.Value.ToString());
                }

                FileData[lineCurrent] = newStr.ToString();
            }
        }
    }
}
