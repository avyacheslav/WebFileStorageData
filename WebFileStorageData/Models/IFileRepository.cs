using System.Collections.Generic;

namespace WebFileStorageData.Models
{
    public interface IFileRepository
    {
        IList<string> FileNames { get; }
        IList<string> FileData { get; set; }
        string SelectedFile { get; set; }
        void GetFileData();
        bool SetFileData(List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> dataList);
    }
}
