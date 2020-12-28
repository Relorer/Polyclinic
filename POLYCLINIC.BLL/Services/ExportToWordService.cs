using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.BLL.Services
{
    public class ExportToWordService
    {
        private readonly string path;
        private readonly XWPFDocument doc = new XWPFDocument();

        public ExportToWordService(string path)
        {
            this.path = path; 
        }

        public ExportToWordService WriteLine(string text, int fontSize, ParagraphAlignment alignment)
        {
            var par = doc.CreateParagraph();
            par.Alignment = alignment;
            XWPFRun run = par.CreateRun();
            run.FontFamily = "Calibri";
            run.FontSize = fontSize;
            run.SetText(text);
            return this;
        }

        public ExportToWordService UseCustome(Action<XWPFDocument> action)
        {
            action.Invoke(doc);
            return this;
        }

        public ExportToWordService Save()
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                doc.Write(fs);
            }
            return this;
        }
    }
}
