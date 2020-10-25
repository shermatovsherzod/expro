using Expro.Common;
using Expro.Models;
using System;

namespace Expro.ViewModels
{
    public class AttachmentDetailsVM
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }

        public AttachmentDetailsVM()
        { }

        public AttachmentDetailsVM(Attachment model)
        {
            if (model == null)
            {
                this.Url = AppData.Configuration.NoPhotoUrl;
                return;
            }

            this.ID = model.ID;
            if (this.ID > 0)
                //this.Url = model.Path + "/" + model.GUID + "." + model.Extension;
                this.Url = "https://firebasestorage.googleapis.com/v0/b/expro-d7dd0.appspot.com/o/" + model.Path.Replace("/", "%2F") + "%2F" + model.FileName + "." + model.Extension + "?" + model.UrlParameters;
            else
                this.Url = AppData.Configuration.NoPhotoUrl;

            Path = model.Path + "/" + model.FileName + "." + model.Extension;
            DisplayName = model.DisplayName;
        }
    }

    public class UploadFileVM
    {
        public string FileName { get; set; } //"generatedName.ext"
        public string DisplayName { get; set; } //"originalName.ext"
        public string Extension { get; set; } //"ext"
        public string Path { get; set; } //"path/to/file/file.txt"
        public long Size { get; set; } //byte
        public string ContentType { get; set; } //"image/jpeg"
        public string DownloadUrl { get; set; } //"https://gerlgnenr.com?param1=val1&param2=val2..."
        public string FileType { get; set; } //Constants.FileTypes.VIDEO_REQUEST_VIDEO
        public int? ModelID { get; set; }
        public string GUID { get; set; }

        public Attachment ToModel()
        {
            Attachment attachment = new Attachment()
            {
                GUID = !string.IsNullOrWhiteSpace(this.GUID) ? this.GUID : Guid.NewGuid().ToString(),
                FileName = this.FileName,
                DisplayName = this.DisplayName,
                Size = this.Size,
                MimeType = this.ContentType,
                Extension = this.Extension
            };

            string path = "";
            string[] tmp = this.Path.Split('/');
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                path += tmp[i];
                if (i < tmp.Length - 2)
                    path += "/";
            }
            attachment.Path = path;

            tmp = this.DownloadUrl.Split('?');
            if (tmp.Length > 1)
                attachment.UrlParameters = tmp[1];

            return attachment;
        }
    }
}
