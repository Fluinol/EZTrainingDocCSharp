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

                        // Navigation line
                        var navLine = doc.Content.Paragraphs.Add(ref missing);
                        navLine.Range.Text = $"← Previous (Step {i:D2}) | Next (Step {i + 2:D2}) →";
                        navLine.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

                        // Add bookmark to the navigation line
                        string bookmarkName = $"Step_{(i + 1).ToString("D2")}";
                        doc.Bookmarks.Add(bookmarkName, navLine.Range);

                        navLine.Range.InsertParagraphAfter();

                        // Step line
                        var stepLine = doc.Content.Paragraphs.Add(ref missing);                        
                        // Apply the default "Heading 2" style
                        //stepLine.Range.set_Style("Heading 2");
                        stepLine.Range.set_Style(Word.WdBuiltinStyle.wdStyleHeading2);
                        stepLine.Range.Text = $"Step {i + 1}:";
                        stepLine.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        stepLine.Range.InsertParagraphAfter();

                        // Description line
                        var stepDescriptionLine = doc.Content.Paragraphs.Add(ref missing);
                        stepDescriptionLine.Range.Text = $"{i + 1}: AddDescriptionForScreenShotHere";
                        stepDescriptionLine.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        stepDescriptionLine.Range.InsertParagraphAfter();

                        // Screenshot line
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

        public string Create(string outputFolder, List<Bitmap> screenshots)
        {
            Word.Application wordApp = null;
            Word.Document doc = null;
            object missing = System.Reflection.Missing.Value;

            try
            {
                
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

                        // Navigation line
                        var navLine = doc.Content.Paragraphs.Add(ref missing);
                        navLine.Range.Text = $"← Previous (Step {i:D2}) | Next (Step {i + 2:D2}) →";
                        navLine.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

                        // Add bookmark to the navigation line
                        string bookmarkName = $"Step_{(i + 1).ToString("D2")}";
                        doc.Bookmarks.Add(bookmarkName, navLine.Range);

                        navLine.Range.InsertParagraphAfter();

                        // Step line
                        var stepLine = doc.Content.Paragraphs.Add(ref missing);
                        // Apply the default "Heading 2" style
                        //stepLine.Range.set_Style("Heading 2");
                        stepLine.Range.set_Style(Word.WdBuiltinStyle.wdStyleHeading2);
                        stepLine.Range.Text = $"Step {i + 1}:";
                        stepLine.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        stepLine.Range.InsertParagraphAfter();

                        // Description line
                        var stepDescriptionLine = doc.Content.Paragraphs.Add(ref missing);
                        stepDescriptionLine.Range.Text = $"{i + 1}: AddDescriptionForScreenShotHere";
                        stepDescriptionLine.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        stepDescriptionLine.Range.InsertParagraphAfter();

                        // Screenshot line
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

                return filePath.ToString();



            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
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
