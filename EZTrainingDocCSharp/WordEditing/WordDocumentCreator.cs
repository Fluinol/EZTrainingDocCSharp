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

                        // Now create subranges for hyperlinks, relative to navRange
                        int navStart = navRange.Start;
                        int prevStart = navStart;
                        int prevEnd = prevStart + prevText.Length;
                        int nextStart = prevEnd + separator.Length;
                        int nextEnd = nextStart + nextText.Length;

                        // "Previous" hyperlink (only if not the first step)
                        if (i > 0)
                        {
                            Word.Range prevRange = doc.Range(prevStart, prevEnd);
                            // doc.Hyperlinks.Add(prevRange, SubAddress: $"Step_{i:D2}", TextToDisplay: prevText);
                        }
                        else
                        {
                            // For the first step, make "Previous" plain text (not a hyperlink)
                        }

                        // "Next" hyperlink (only if not the last step)
                        if (i < screenshots.Count - 1)
                        {
                            Word.Range nextRange = doc.Range(nextStart, nextEnd);
                            // doc.Hyperlinks.Add(nextRange, SubAddress: $"Step_{(i + 2):D2}", TextToDisplay: nextText);
                        }
                        else
                        {
                            // Replace "Next" text with "End" for the last step
                            Word.Range nextRange = doc.Range(nextStart, nextEnd);
                            nextRange.Text = "End";
                        }

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