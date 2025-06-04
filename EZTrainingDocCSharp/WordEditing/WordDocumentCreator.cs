using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;

namespace EZTrainingDocCSharp.WordEditing
{
    public class WordDocumentCreator
    {
        public void Create(string outputFolder, List<Bitmap> screenshots, Action<string> updateStatus)
        {
            Word.Application wordApp = null;
            Word.Document doc = null;
            object missing = System.Reflection.Missing.Value;

            try
            {
                updateStatus?.Invoke("Creating Word document...");
                wordApp = new Word.Application();
                doc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                Word.Paragraph para = doc.Content.Paragraphs.Add(ref missing);
                para.Range.Text = "Document Created: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                para.Range.Font.Bold = 1;
                para.Range.InsertParagraphAfter();

                if (screenshots.Count > 0)
                {
                    for (int i = 0; i < screenshots.Count; i++)
                    {
                        string imgPath = Path.Combine(Path.GetTempPath(), $"screenshot_{i}.png");
                        screenshots[i].Save(imgPath, ImageFormat.Png);

                        var title = doc.Content.Paragraphs.Add(ref missing);
                        //Navigation line
                        title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        title.Range.Text = $"← Previous (Step {i:D2}) | Next (Step {i + 2:D2}) →".PadRight(50);
                        title.Range.InsertParagraphAfter();
                        //Step line
                        title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        title.Range.Text = $"Step {i + 1}:";
                        title.Range.InsertParagraphAfter();
                        //Description line
                        title.Range.Text = $"{i + 1}: AddDescriptionForScreenShotHere";
                        title.Range.InsertParagraphAfter();
                        //Screenshot line
                        var paraImg = doc.Content.Paragraphs.Add(ref missing);
                        paraImg.Range.InlineShapes.AddPicture(imgPath, ref missing, ref missing, ref missing);
                        paraImg.Range.InsertParagraphAfter();

                        Word.Range endRange = doc.Content;
                        endRange.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                        endRange.InsertBreak(Word.WdBreakType.wdPageBreak);

                        File.Delete(imgPath);
                    }
                }
                else
                {
                    var noImg = doc.Content.Paragraphs.Add(ref missing);
                    noImg.Range.Text = "No screenshots captured.";
                    noImg.Range.InsertParagraphAfter();
                }

                string fileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
                object filePath = Path.Combine(outputFolder, fileName);
                doc.SaveAs2(ref filePath);

                updateStatus?.Invoke("Saved: " + filePath);
            }
            catch (Exception ex)
            {
                updateStatus?.Invoke("Error: " + ex.Message);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(false);
                    Marshal.ReleaseComObject(doc);
                }

                if (wordApp != null)
                {
                    wordApp.Quit();
                    Marshal.ReleaseComObject(wordApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
