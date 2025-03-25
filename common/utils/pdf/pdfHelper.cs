using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Borders;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.Layout.Properties;
using iText.IO.Image;
using entities.models;
using iText.IO.Font.Constants;

namespace common.utils.pdf
{
    public class pdfHelper
    {
        public static float mmToPts(float mm) => mm * 72 / 25.4f;

        public byte[] generatePdf(List<documentModel> documents, List<sectionModel> sections, List<contentModel> contents, byte[] sH, byte[] sV, byte[] sH1)
        {
            try
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Table tableP;
                    Cell cell;
                    Paragraph paragraph;
                    Text paragraphText;
                    var PDFFile = new byte[] { };
                    var PDFWriter = new PdfWriter(memory);
                    var pdfDocument = new PdfDocument(PDFWriter);
                    var drawingColor = System.Drawing.Color.FromName("black");
                    var baseFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    var page = new Document(pdfDocument, PageSize.LETTER);
                    page.SetMargins(mmToPts(5), mmToPts(10), mmToPts(5), mmToPts(10));

                    foreach (var document in documents)
                    {
                        pdfDocument.AddNewPage();
                        tableP = new Table(new float[] { 49f, 2f, 49.5f });
                        tableP.SetWidth(new UnitValue(2, 100));

                        var tableHeader = new Table(new float[] { 100 });
                        tableHeader.SetWidth(new UnitValue(2, 100));

                        var headerSection = sections.Where(x => x.order == 1).FirstOrDefault();
                        if (headerSection != null)
                        {
                            var header = headerSection.image!;
                            if (header != null && header.Length != 0)
                            {
                                var headerImage = new Image(ImageDataFactory.Create(header));
                                headerImage.SetAutoScale(true);
                                headerImage.SetBorder(new SolidBorder(0));
                                cell = new Cell();
                                cell.Add(headerImage);
                                cell.SetBorder(Border.NO_BORDER);
                                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                cell.SetWidth(new UnitValue(2, 100));
                                tableHeader.AddCell(cell);
                            }
                        }

                        var cellA = new Cell(1, 3);
                        cellA.SetBorder(Border.NO_BORDER);
                        cellA.SetHeight(100);
                        cellA.Add(tableHeader);
                        cellA.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        tableP.AddCell(cellA);

                        var cellB = new Cell();
                        var cellC = new Cell();
                        var cellD = new Cell();

                        var table1 = new Table(new float[] { 100 });
                        table1.SetWidth(new UnitValue(2, 100));

                        var cellE = new Cell();
                        var tableS1 = new Table(new float[] { 100 });
                        tableS1.SetWidth(new UnitValue(2, 100));

                        var mainImg = new Image(ImageDataFactory.Create(document.mainImage));
                        mainImg.SetAutoScale(true);
                        mainImg.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        mainImg.SetBorder(new SolidBorder(0));

                        cell = new Cell();
                        cell.Add(mainImg);
                        cell.SetBorder(Border.NO_BORDER);
                        cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        cell.SetHeight(350);
                        tableS1.AddCell(cell);

                        var contentSection2 = contents.Where(x => x.sectionId == 2).First();
                        var baseFont1 = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                        cellE.SetBorder(Border.NO_BORDER);
                        cellE.SetHeight(150);
                        tableS1.SetTextAlignment(TextAlignment.CENTER);
                        tableS1.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        cellE.Add(tableS1);
                        table1.AddCell(cellE);

                        paragraphText = new Text(document.subtitle);
                        paragraphText.SetFont(baseFont1);
                        paragraphText.SetFontSize(22);
                        paragraphText.SetFontColor(new DeviceRgb(drawingColor.R, drawingColor.G, drawingColor.B));

                        paragraph = new Paragraph(paragraphText);
                        paragraph.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        
                        var cellF = new Cell();
                        cellF.SetBorder(Border.NO_BORDER);
                        cellF.SetPaddingTop(30);
                        cellF.SetHeight(30);
                        cellF.Add(paragraph);
                        tableS1.AddCell(cellF);

                        var cellG = new Cell();
                        var tableS3 = new Table(new float[] { 100 });
                        tableS3.SetWidth(new UnitValue(2, 100));

                        foreach (var contentSection3 in contents.Where(x => x.sectionId == 3).ToList())
                        {
                            switch (contentSection3.order)
                            {
                                case 1:
                                    contentSection3.content = document.title;
                                    paragraphText = new Text(contentSection3.content);
                                    paragraphText.SetFont(baseFont);
                                    paragraphText.SetFontSize(22);
                                    paragraphText.SetFontColor(new DeviceRgb(drawingColor.R, drawingColor.G, drawingColor.B));
                                    cell = new Cell();
                                    cell.SetBorder(Border.NO_BORDER);
                                    paragraph = new Paragraph(paragraphText);
                                    paragraph.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                    cell.SetPaddingTop(30);
                                    cell.SetPaddingBottom(0);
                                    cell.Add(paragraph);
                                    tableS3.AddCell(cell);
                                    cell = new Cell();
                                    cell.SetBorder(Border.NO_BORDER);
                                    cell.SetPaddingTop(0);
                                    cell.SetHeight(15);
                                    tableS3.AddCell(cell);
                                    break;
                                case 2:
                                    paragraphText = new Text(contentSection3.content);
                                    paragraphText.SetFont(baseFont);
                                    paragraphText.SetFontSize(14);
                                    paragraphText.SetFontColor(new DeviceRgb(drawingColor.R, drawingColor.G, drawingColor.B));
                                    cell = new Cell();
                                    cell.SetBorder(Border.NO_BORDER);
                                    paragraph = new Paragraph(paragraphText);
                                    paragraph.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                    cell.Add(paragraph);
                                    cell.SetPaddingBottom(0);
                                    tableS3.AddCell(cell);
                                    break;
                                case 3:
                                    paragraphText = new Text(contentSection3.content);
                                    paragraphText.SetFont(baseFont);
                                    paragraphText.SetFontSize(14);
                                    paragraphText.SetFontColor(new DeviceRgb(drawingColor.R, drawingColor.G, drawingColor.B));
                                    cell = new Cell();
                                    cell.SetBorder(Border.NO_BORDER);
                                    paragraph = new Paragraph(paragraphText);
                                    paragraph.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                    cell.Add(paragraph);
                                    cell.SetPaddingBottom(0);
                                    tableS3.AddCell(cell);
                                    cell = new Cell();
                                    cell.SetBorder(Border.NO_BORDER);
                                    cell.SetPaddingTop(0);
                                    cell.SetHeight(15);
                                    tableS3.AddCell(cell);
                                    break;
                            }
                        }

                        cellF.SetBorder(Border.NO_BORDER);
                        cellF.SetHeight(new UnitValue(2, 100));
                        cellF.SetPaddingBottom(0);
                        tableS3.SetTextAlignment(TextAlignment.CENTER);
                        cellF.Add(tableS3);
                        table1.AddCell(cellF);

                        var tableS4 = new Table(new float[] { 400 });
                        tableS4.SetWidth(new UnitValue(2, 100));

                        foreach (var contentSection4 in contents.Where(x => x.sectionId == 4).ToList())
                        {
                            cell = new Cell();
                            cell.SetBorder(Border.NO_BORDER);
                            cell.SetPaddingBottom(0);

                            if (contentSection4.order != 0)
                            {
                                paragraphText = new Text(contentSection4.content);
                                paragraphText.SetFont(baseFont);
                                paragraphText.SetFontSize(8);
                                paragraphText.SetFontColor(new DeviceRgb(drawingColor.R, drawingColor.G, drawingColor.B));
                                paragraph = new Paragraph(paragraphText);
                                paragraph.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                cell.Add(paragraph);
                                tableS4.AddCell(cell);
                            }
                        }

                        cellG.SetBorder(Border.NO_BORDER);
                        cellG.SetHeight(new UnitValue(2, 100));
                        cellG.SetPaddingBottom(0);
                        cellG.Add(tableS4);
                        table1.AddCell(cellG);

                        cellB.SetBorder(Border.NO_BORDER);
                        cellB.SetHeight(new UnitValue(2, 100));
                        table1.SetTextAlignment(TextAlignment.CENTER);
                        cellB.Add(table1);

                        var tableSeparatorV = new Table(new float[] { 100 });
                        tableSeparatorV.SetWidth(new UnitValue(2, 100));
                        var SV = new Image(ImageDataFactory.Create(sV));
                        SV.SetBorder(new SolidBorder(0));
                        SV.SetAutoScale(true);
                        cell = new Cell();
                        cell.Add(SV);
                        cell.SetBorder(Border.NO_BORDER);
                        cell.SetHeight(530);
                        cell.SetWidth(new UnitValue(2, 100));
                        cell.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                        tableSeparatorV.AddCell(cell);
                        tableSeparatorV.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                        cellC.SetBorder(Border.NO_BORDER);
                        cellC.SetHeight(new UnitValue(2, 100));
                        cellC.Add(tableSeparatorV);
                        cellC.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

                        var table2 = new Table(new float[] { 100 });
                        table2.SetWidth(new UnitValue(2, 100));
                        var cellH = new Cell();
                        var cellI = new Cell();
                        var tableS5 = new Table(new float[] { 100 });
                        tableS5.SetWidth(new UnitValue(2, 100));
                        
                        var section5 = sections.Where(x => x.order == 5).FirstOrDefault();
                        if (section5 != null)
                        {
                            byte[] seccion5Image = section5.image;

                            if (seccion5Image != null && seccion5Image.Length != 0)
                            {
                                Image S5 = new Image(ImageDataFactory.Create(seccion5Image));
                                S5.SetBorder(new SolidBorder(0));
                                S5.SetAutoScale(true);


                                cell = new Cell();
                                cell.Add(S5);
                                cell.SetBorder(Border.NO_BORDER);
                                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                cell.SetPaddingLeft(0);
                                tableS5.AddCell(cell);
                            }
                        }

                        var SH1 = new Image(ImageDataFactory.Create(sH1));
                        SH1.SetBorder(new SolidBorder(0));
                        SH1.SetAutoScale(false);
                        cell = new Cell();
                        cell.Add(SH1);
                        cell.SetBorder(Border.NO_BORDER);
                        cell.SetVerticalAlignment(VerticalAlignment.BOTTOM);
                        cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        tableS5.AddCell(cell);

                        cellH.SetBorder(Border.NO_BORDER);
                        cellH.SetHeight(250);
                        cellH.Add(tableS5);
                        table2.AddCell(cellH);

                        var tableS6 = new Table(new float[] { 100 });
                        tableS6.SetWidth(new UnitValue(2, 100));

                        var section6 = sections.Where(x => x.order == 6).FirstOrDefault();
                        if (section6 != null)
                        {
                            byte[] seccion6Image = section6.image!;

                            if (seccion6Image != null && seccion6Image.Length != 0)
                            {
                                Image S6 = new Image(ImageDataFactory.Create(seccion6Image));
                                S6.SetBorder(new SolidBorder(0));
                                S6.SetAutoScale(true);

                                cell = new Cell();
                                cell.Add(S6);
                                cell.SetBorder(Border.NO_BORDER);
                                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                tableS6.AddCell(cell);
                            }
                        }

                        cellI.SetBorder(Border.NO_BORDER);
                        cellI.SetHeight(250);
                        cellI.Add(tableS6);
                        table2.AddCell(cellI);

                        cellD.SetBorder(Border.NO_BORDER);
                        table2.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        cellD.Add(table2);

                        tableP.AddCell(cellB);
                        tableP.AddCell(cellC);
                        tableP.AddCell(cellD);

                        var tableFooter = new Table(new float[] { 100 });
                        tableFooter.SetWidth(new UnitValue(2, 100));

                        var cellJ = new Cell(1, 3);

                        var SH = new Image(ImageDataFactory.Create(sH));
                        SH.SetBorder(new SolidBorder(0));
                        SH.SetAutoScale(true);
                        cell = new Cell();
                        cell.Add(SH);
                        cell.SetBorder(Border.NO_BORDER);

                        tableFooter.AddCell(cell);

                        var footerSection = sections.Where(x => x.order == 7).FirstOrDefault();
                        if (footerSection != null)
                        {
                            var footer = footerSection.image;

                            if (footer != null && footer.Length != 0)
                            {
                                var Footer = new Image(ImageDataFactory.Create(footer));
                                Footer.SetBorder(new SolidBorder(0));
                                Footer.SetAutoScale(true);
                                Footer.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                cell = new Cell();
                                cell.Add(Footer);
                                cell.SetBorder(Border.NO_BORDER);
                                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                tableFooter.AddCell(cell);
                                tableFooter.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                            }
                        }

                        cellJ.SetBorder(Border.NO_BORDER);
                        cellJ.SetHeight(80);
                        cellJ.Add(tableFooter);
                        cellJ.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        cellJ.SetTextAlignment(TextAlignment.CENTER);
                        tableP.AddCell(cellJ);
                        page.Add(tableP);
                    }

                    page.Close();
                    PDFFile = memory.ToArray();
                    return PDFFile;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}