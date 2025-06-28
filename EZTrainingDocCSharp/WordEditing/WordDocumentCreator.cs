using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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
                        Word.Paragraph navPara = doc.Content.Paragraphs.Add();
                        Word.Range navRange = navPara.Range;

                        // Compose navigation text
                        string prevText = $"← Previous (Step {i:D2})";
                        string nextText = $"Next (Step {i + 2:D2}) →";
                        string separator = " | ";
                        string navText = prevText + separator + nextText;

                        // Insert the navigation text first
                        navRange.Text = navText;

                        // Add bookmark to nav line (after text is inserted)
                        string currentBookmarkName = $"Step_{(i + 1):D2}";
                        doc.Bookmarks.Add(currentBookmarkName, navRange);

                        // Calculate offsets relative to navRange.Start
                        int prevOffset = 0;
                        int prevEndOffset = prevText.Length;
                        int nextOffset = prevText.Length + separator.Length;
                        int nextEndOffset = nextOffset + nextText.Length;

                        // Create subranges BEFORE adding any hyperlinks
                        Word.Range prevRange = navRange.Duplicate;
                        prevRange.SetRange(navRange.Start + prevOffset, navRange.Start + prevEndOffset);

                        Word.Range nextRange = navRange.Duplicate;
                        nextRange.SetRange(navRange.Start + nextOffset, navRange.Start + nextEndOffset);

                        // Now add hyperlinks
                        if (i > 0)
                            doc.Hyperlinks.Add(prevRange, SubAddress: $"Step_{i:D2}", TextToDisplay: prevText);
                        else //First step has no previous link, always step 1
                            doc.Hyperlinks.Add(prevRange, SubAddress: $"Step_{1:D2}", TextToDisplay: $"← Previous (Step {1:D2})");

                        if (i < screenshots.Count - 1)
                            doc.Hyperlinks.Add(nextRange, SubAddress: $"Step_{(i + 2):D2}", TextToDisplay: nextText);
                        else
                            nextRange.Text = "End of the document";
                            //nextRange.Text = $"Last (Step {i + 2:D2}) →";

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
                string fileName = $"EZ_Training_Doc_{DateTime.Now:yyyyMMdd_HHmmss}.docx";
                object filePath = Path.Combine(outputFolder, fileName);
                doc.SaveAs2(ref filePath);
                return filePath.ToString();
            }
            catch (COMException comEx)
            {
                // Display a specific error message for COMExceptions
                string errorMessage = $"COM Error: {comEx.Message}\n" +
                                      $"HRESULT: 0x{comEx.ErrorCode:X8}\n" +
                                      $"Source: {comEx.Source}\n" +
                                      $"Stack Trace:\n{comEx.StackTrace}";

                MessageBox.Show(errorMessage, "COM Error Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
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