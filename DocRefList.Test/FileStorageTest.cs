using System.IO;
using System.Linq;
using DocRefList.Storage;
using Xunit;

namespace DocRefList.Test
{
    public class FileStorageTest
    {
        [Fact]
        public void Test_Open_FileStorage()
        {
            FileStorage storage = new FileStorage("D:\\Test");

            DocumentStorage attach = storage[1];

            FileItem item = new FileItem()
            {
                FileName = "форма временного пропуска.doc",
                MimeType = "doc"
            };

            using (FileStream fileStream = new FileStream(Path.Combine("D:\\Test", item.FileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileStream.CopyTo(item.Stream);
            }


            attach.AddFile(item);

            int count = attach.GetGuids().Count();

            Assert.True(count == 1);
        }
    }
}
