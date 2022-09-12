using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Presentation.Controllers
{
    public class UploadController : Controller
    {
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public class ChunkMetaData
        {
            public string UploadUid { get; set; }
            public string FileName { get; set; }
            public string RelativePath { get; set; }
            public string ContentType { get; set; }
            public long ChunkIndex { get; set; }
            public long TotalChunks { get; set; }
            public long TotalFileSize { get; set; }
        }

        public class FileResult
        {
            // Because the chunks are sent in a specific order,
            // the server is expected to send back a response
            // with the meta data of the chunk that is uploaded.
            public bool uploaded { get; set; }
            public string fileUid { get; set; }
            public string FileName { get; set; }
            public string FileNameUniuqe { get; set; }
            public string ActionType { get; set; }
        }

        public void AppendToFile(string fullPath, IFormFile content)
        {
            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    content.CopyTo(stream);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public ActionResult ChunkSave(IEnumerable<IFormFile> files, string metaData)
        {
            if (metaData == null)
            {
                return Save(files);
            }

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(metaData));

            JsonSerializer serializer = new JsonSerializer();
            ChunkMetaData chunkData;
            using (StreamReader streamReader = new StreamReader(ms))
            {
                chunkData = (ChunkMetaData)serializer.Deserialize(streamReader, typeof(ChunkMetaData));
            }
            //my
            var newFileName = chunkData.UploadUid + "_" + chunkData.FileName;

            string path = String.Empty;
            // The Name of the Upload component is "files".
            if (files != null)
            {
                foreach (var file in files)
                {
                    path = Path.Combine(WebHostEnvironment.WebRootPath, "DocumentFile", newFileName);
                    AppendToFile(path, file);
                }
            }

            FileResult fileBlob = new FileResult();

            // This response indicates to the Upload
            // that it can proceed either
            // with the next chunk ("uploaded" = false) or with the next file ("uploaded" = true).
            fileBlob.uploaded = chunkData.TotalChunks - 1 <= chunkData.ChunkIndex;
            fileBlob.fileUid = chunkData.UploadUid;
//my

            fileBlob.FileName = chunkData.FileName;
            fileBlob.FileNameUniuqe = newFileName;
            fileBlob.ActionType = "Save";
            return Json(fileBlob);
        }

        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(WebHostEnvironment.WebRootPath, "DocumentFile", fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }
            // Return an empty string to signify success
            //return Content("");
            //my
            FileResult fileBlob = new FileResult();

            fileBlob.FileNameUniuqe = fileNames[0];
            fileBlob.ActionType = "Remove";
            // Return an empty string to signify success
            return Json(fileBlob);
        }

        public ActionResult Save(IEnumerable<IFormFile> files)
        {
            // The Name of the Upload component is "files".
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    // Some browsers send file names with full path.
                    // The demo is interested only in the file name.
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                    var physicalPath = Path.Combine(WebHostEnvironment.WebRootPath, "DocumentFile", fileName);
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }
                }
            }
            // Return an empty string to signify success.
            return Content("");
        }
    }
}