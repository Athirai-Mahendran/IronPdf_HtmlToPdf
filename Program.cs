using IronPdf;
using IronPdf.Rendering;
using System;
using System.Drawing;
using System.IO;

namespace IronPdf_HtmlToPdf
{
    //    public class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            Console.WriteLine(DateTime.Now);

    //            string localHtmlFilePath = @"C:\Users\home\source\repos\IronPdf_POC\Content\SampleHTML.html";

    //            if (!File.Exists(localHtmlFilePath))
    //            {
    //                Console.WriteLine("File not found: " + localHtmlFilePath);
    //                return;
    //            }

    //            string htmlContent = File.ReadAllText(localHtmlFilePath);

    //            try
    //            {
    //                // Create an instance of the HTML to PDF converter
    //                var renderer = new ChromePdfRenderer();
    //                renderer.RenderHtmlAsPdf(htmlContent);

    //                //renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PaperSize.A4;
    //                renderer.RenderingOptions.Title = "Sample PDF Document";
    //                renderer.RenderingOptions.EnableJavaScript = true;
    //                renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;

    //                Console.WriteLine(DateTime.Now);
    //                Console.WriteLine("PDF successfully created by IronPdf at: " + localHtmlFilePath);
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine("An error occurred: " + ex.Message);
    //            }

    //            Console.ReadLine();
    //        }
    //        }
    //}

    //using System;
    //using System.IO;
    //using System.Drawing;
    //using IronPdf;
    //using IronPdf.Rendering;

    //namespace IronPdf_Converter
    //{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);

            string localHtmlFilePath = @"C:\Users\home\source\repos\Select_Pdf_POC\Content\SampleHTML.html";

            if (!File.Exists(localHtmlFilePath))
            {
                Console.WriteLine("File not found: " + localHtmlFilePath);
                return;
            }

            string htmlContent = File.ReadAllText(localHtmlFilePath);

            try
            {
                // Create an instance of the ChromePdfRenderer
                var renderer = new ChromePdfRenderer();

                // Configure PDF options
                renderer.RenderingOptions.PaperSize = PdfPaperSize.A4;
                renderer.RenderingOptions.MarginTop = 0;
                renderer.RenderingOptions.MarginBottom = 10;
                renderer.RenderingOptions.MarginLeft = 20;
                renderer.RenderingOptions.MarginRight = 20;
                //renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Screen;
                renderer.RenderingOptions.Title = "IronPdf Generated Document";

                // Create PDF document
                var pdf = renderer.RenderHtmlAsPdf(htmlContent);

                // Define header HTML with proper image path
                string headerHtml = $@"
                    <div style='text-align: center; width: 100%;'>
                        <img src='{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "Calrstahl_images", "CSLET_EIAC_HeaderCopy.jpg")}' 
                             style='width: 100%; max-width: 100%;' />
                    </div>";

                // Define footer HTML with proper image path
                string footerHtml = $@"
                    <div style='text-align: center; width: 100%;'>
                        <img src='{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "Calrstahl_images", "CSSSC_EIAC_Footer.jpg")}' 
                             style='width: 100%; max-width: 100%;' />
                        <div style='text-align: right; color: gray; font-size: 8px; margin-top: 5px;'>
                            Page {{page}} of {{total-pages}}
                        </div>
                    </div>";

                HtmlHeaderFooter htmlHeader = new HtmlHeaderFooter();
                htmlHeader.HtmlFragment = headerHtml;


                 HtmlHeaderFooter htmlFooter = new HtmlHeaderFooter();
                htmlFooter.HtmlFragment = footerHtml;

                // Add header and footer
                // Note: Using the correct overload with HTML string
                pdf.AddHtmlHeaders(htmlHeader,1);
                pdf.AddHtmlFooters(htmlFooter,1);

                // Add watermark
                // Note: Using the correct method name and parameters
                pdf.ApplyWatermark(footerHtml,1, IronPdf.Editing.VerticalAlignment.Middle,IronPdf.Editing.HorizontalAlignment.Center);


                //pdf.AddTextWatermark("Text Customized Watermark, IronPdf!",
                //    new WatermarkOptions()
                //    {
                //        FontColor = Color.FromArgb(100, Color.Olive),
                //        FontSize = 48,
                //        Rotation = 45
                //    });





                //// Add header
                //pdf.AddHtmlHeaders(10, @"
                //    <div style='text-align: center;'>
                //        <img src='C:\Users\home\source\repos\IronPdf_Converter\Content\Calrstahl_images\CSLET_EIAC_HeaderCopy.jpg' style='width: 100%;' />
                //    </div>");

                //// Add footer
                //pdf.AddHTMLFooters(10, @"
                //    <div style='text-align: center;'>
                //        <img src='C:\Users\home\source\repos\IronPdf_Converter\Content\Calrstahl_images\CSSSC_EIAC_Footer.jpg' style='width: 100%;' />
                //        <div style='text-align: right; color: gray; font-size: 8px;'>Page {page} of {total-pages}</div>
                //    </div>");

                //// Add watermark
                //pdf.AddTextWatermark("Text Customized Watermark, IronPdf!", Color.Olive, 100);

                // Save the PDF document
                string dateTimeNow = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string outputFilePath = $"D:\\Select.Pdf_POC\\PDF\\IronPdf\\HTMLToPDF_1_{dateTimeNow}.pdf";
                pdf.SaveAs(outputFilePath);

                Console.WriteLine(DateTime.Now);
                Console.WriteLine("PDF successfully created at: " + outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}

