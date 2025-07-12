using EZTrainingDocCSharp.ScreenCapture;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace EZTrainingDocCSharp.HTML
{
    public class HTMLCreator
    {
        public string Create(string outputFolder, List<ScreenshotInfo> screenshots)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html><head><meta charset='UTF-8'><title>EZ Training Doc</title>");
                sb.AppendLine("<style>");
                sb.AppendLine("body { font-family: Arial, sans-serif; margin: 40px; }");
                sb.AppendLine(".nav { text-align: right; margin-bottom: 10px; }");
                sb.AppendLine(".step { font-size: 1.2em; font-weight: bold; margin-top: 30px; }");
                sb.AppendLine(".desc { margin-bottom: 10px; }");
                sb.AppendLine(".screenshot { margin-bottom: 30px; }");
                sb.AppendLine("</style></head><body>");
                //Make title editable
                sb.AppendLine("<h1 contenteditable='true'>EZ Training Document</h1>");
                sb.AppendLine($"<h1 contenteditable='true'>Document Created: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</h1>");
                sb.AppendLine($"<h1 contenteditable='true'>Edit step description and CTRL+S (save HTML) locally</h1>");

                if (screenshots.Count > 0)
                {
                    for (int i = 0; i < screenshots.Count; i++)
                    {
                        // Convert image to Base64
                        string base64Image;
                        using (var ms = new MemoryStream())
                        {
                            screenshots[i].Image.Save(ms, ImageFormat.Png);
                            base64Image = Convert.ToBase64String(ms.ToArray());
                        }

                        // Navigation
                        sb.AppendLine($"<div class='nav' id='nav_{i + 1}'>");
                        if (i > 0)
                            sb.AppendLine($"<a href='#nav_{i}'>← Previous (Step {i:D2})</a> | ");
                        else
                            sb.AppendLine($"← Previous (Step 01) | ");
                        if (i < screenshots.Count - 1)
                            sb.AppendLine($"<a href='#nav_{i + 2}'>Next (Step {(i + 2):D2}) →</a>");
                        else
                            sb.AppendLine("End of the document");
                        sb.AppendLine("</div>");

                        // Step heading (no id here)
                        sb.AppendLine($"<div class='step'>Step {i + 1}:</div>");

                        // Description (now editable)
                        sb.AppendLine($"<div class='desc' contenteditable='true'>AddDescriptionHere</div>");

                        // Screenshot (embedded)
                        sb.AppendLine($"<div class='screenshot'><img src='data:image/png;base64,{base64Image}' alt='Screenshot {i + 1}' style='max-width:100%;height:auto;'/></div>");
                        sb.AppendLine("<hr/>");
                    }
                }
                else
                {
                    sb.AppendLine("<p>No screenshots captured.</p>");
                }

                sb.AppendLine("</body></html>");

                string fileName = $"EZ_Training_Doc_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                string filePath = Path.Combine(outputFolder, fileName);
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return filePath;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}