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

                        // --- Navigation line ---
                        // Add a new paragraph for navigation
                        Word.Paragraph navPara = doc.Content.Paragraphs.Add();
                        Word.Range navRange = navPara.Range;

                        // Insert the full nav text
                        string prevText = $"← Previous (Step {i:D2})";
                        string nextText = $"Next (Step {i + 2:D2}) →";
                        string separator = " | ";
                        string fullText = prevText + separator + nextText;
                        //navRange.InsertAfter(fullText);

                        // Calculate exact positions
                        int start = navRange.Start;
                        int prevStart = start;
                        int prevEnd = prevStart + prevText.Length;
                        int nextStart = prevEnd + separator.Length;
                        int nextEnd = nextStart + nextText.Length;

                        // Create subranges for the two links
                        Word.Range prevRange = doc.Range(prevStart, prevEnd);
                        Word.Range nextRange = doc.Range(nextStart, nextEnd);

                        // Add hyperlinks
                        if (i==0)
                            doc.Hyperlinks.Add(prevRange, SubAddress: $"Step_{(i + 1):D2}", TextToDisplay: prevText);
                        else
                            doc.Hyperlinks.Add(prevRange, SubAddress: $"Step_{i:D2}", TextToDisplay: prevText);
                        doc.Hyperlinks.Add(nextRange, SubAddress: $"Step_{(i + 2):D2}", TextToDisplay: nextText);


                        // Add bookmark to nav line
                        string currentBookmarkName = $"Step_{(i + 1):D2}";
                        doc.Bookmarks.Add(currentBookmarkName, navPara.Range);

                        // Align and break
                        navPara.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        navPara.Range.InsertParagraphAfter();


                        // --- Step line ---
                        var stepLine = doc.Content.Paragraphs.Add(ref missing);
                        stepLine.Range.set_Style(Word.WdBuiltinStyle.wdStyleHeading2);
                        stepLine.Range.Text = $"Step {i + 1}:";
                        stepLine.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;                                          
                        stepLine.Range.InsertParagraphAfter();

                        // --- Description line ---
                        var stepDescriptionLine = doc.Content.Paragraphs.Add(ref missing);
                        stepDescriptionLine.Range.Text = $"{i + 1}: AddDescriptionForScreenShotHere";
                        stepDescriptionLine.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        stepDescriptionLine.Range.InsertParagraphAfter();

                        // --- Screenshot line ---
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