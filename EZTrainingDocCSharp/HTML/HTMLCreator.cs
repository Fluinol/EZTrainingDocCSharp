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
                sb.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
                sb.AppendLine("h1 { margin-bottom: 10px; font-size: 2em; }");
                sb.AppendLine("p { margin: 4px 0 8px 0; font-size: 1em; }");
                sb.AppendLine(".nav { text-align: right; margin-bottom: 4px; margin-top: 4px; }");
                sb.AppendLine(".step { font-size: 1.1em; font-weight: bold; margin-top: 10px; margin-bottom: 4px; }");
                sb.AppendLine(".desc { margin-bottom: 4px; }");
                sb.AppendLine(".screenshot { margin-bottom: 4px; }");
                sb.AppendLine("</style>");
                sb.AppendLine(@"<script>
function saveHTML() {
    var htmlContent = document.documentElement.outerHTML;
    var blob = new Blob([htmlContent], {type: 'text/html'});
    var a = document.createElement('a');
    a.href = URL.createObjectURL(blob);
    a.download = 'EZ_Training_Doc_Edited.html';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}
</script>");
                sb.AppendLine("</head><body>");

                // Add logo and title block
                sb.AppendLine("<div style='display:flex;align-items:center;margin-bottom:10px;'>");
                sb.AppendLine("<img src='https://yourcompany.com/logo.png' alt='Company Logo' style='height:48px;margin-right:16px;'/>");
                sb.AppendLine("<h1 contenteditable='true' style='margin:0;'>EZ Training Document</h1>");
                sb.AppendLine("</div>");
                sb.AppendLine($"<p contenteditable='true'>Document Created: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>");
                sb.AppendLine("<p contenteditable='true'>Edit title and step description and click 'Save HTML' to download saved version</p>");

                // Move the Save button below the edit instructions, aligned left
                sb.AppendLine("<div style='margin:12px 0;text-align:left;'>");
                sb.AppendLine("<button onclick='saveHTML()'>Save HTML</button>");
                sb.AppendLine("</div>");

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
                            //add step 01 link 
                            sb.AppendLine($"<a href='#nav_{1}'>← Previous (Step {1:D2})</a> | ");
                        
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
                        sb.AppendLine($"<div class='screenshot'><img src='data:image/png;base64,{base64Image}' alt='Screenshot {i + 1}' style='max-width:75%;height:75%;'/></div>");
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