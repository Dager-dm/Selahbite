﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using ENTITY;
using Org.BouncyCastle.Utilities;

namespace BLL
{
    public class ServicioFactura
    {
        public ServicioFactura()
        {
            
        }


        public static void CreateFactura(string Cajero, Pedido pedido)
        {
            float alturaagregada = pedido.Detalles.Count() * 25; 
            float ancho = 80f;
            float alto = 130f + alturaagregada;
            float marginLeft = 10f;
            float marginRight = 10f;
            float marginTop = 3f;
            float marginBottom = 10f;
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(ancho * 2.83465f, alto * 2.83465f); // Convierte mm a puntos

            iTextSharp.text.Document doc = new iTextSharp.text.Document(pageSize, marginLeft, marginRight, marginTop, marginBottom);


            // Crea una instancia de PdfWriter
            PdfWriter.GetInstance(doc, new FileStream("factura_N°" + pedido.NumeroFactura.ToString() + ".pdf", FileMode.Create));


            string verdanaFontPath = "C:\\Windows\\Fonts\\Verdana.ttf";

            BaseFont baseFont = BaseFont.CreateFont(verdanaFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font verdanaFont = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font verdanaFontNoBold = new iTextSharp.text.Font(baseFont, 9, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font verdanaFontBoldbig = new iTextSharp.text.Font(baseFont, 11, iTextSharp.text.Font.BOLD);


            doc.Open();





            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("D:\\Proyecto\\Selahbite\\GUI\\Assets\\Images\\FacturaLogo.png");


            logo.ScaleToFit(90, 90);
            logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            doc.Add(logo);


            iTextSharp.text.Paragraph InfoInicial = new iTextSharp.text.Paragraph("Parrilla Gourmet Salam - Restaurante \nFonseca la Guajira - Calle 13 N°13-89\nTelefono: 3202637816 / 3165348796 \nNit: 17.953.278-1 \nRegimen Simplificado", verdanaFont);
            InfoInicial.Alignment = Element.ALIGN_CENTER;
            doc.Add(InfoInicial);


            Paragraph separator = new Paragraph("---------------------------------------------------");
            doc.Add(separator);


            iTextSharp.text.Paragraph InfoClienteCajero = new iTextSharp.text.Paragraph("Factura N°: "+pedido.NumeroFactura.ToString()+" \nFecha: "+pedido.Fecha.ToString()+" \nCajero: " + Cajero + "\nCliente: " + pedido.Cliente.Nombre.ToString() + "\nC.C: "+pedido.Cliente.Id.ToString(), verdanaFont);
            InfoClienteCajero.Alignment = Element.ALIGN_LEFT;
            doc.Add(InfoClienteCajero);
            doc.Add(separator);


           


            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 210; table.LockedWidth = true;



            foreach (var item in pedido.Detalles)
            {
                PdfPCell cell1 = new PdfPCell(new Phrase(item.Producto.Nombre, verdanaFont));
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.HorizontalAlignment = Element.ALIGN_LEFT; cell1.PaddingTop = 15f;
                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase(item.ValorProductoVendido.ToString(), verdanaFont));
                cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell2.HorizontalAlignment = Element.ALIGN_RIGHT; cell2.PaddingTop = 15f;
                table.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase(item.Producto.Valor.ToString()+" x "+item.Cantidad.ToString()+" Unidades", verdanaFontNoBold));
                cell3.Border = iTextSharp.text.Rectangle.NO_BORDER; cell3.PaddingTop = 5f;
                cell3.Colspan = 2;
                table.AddCell(cell3);

            }
           
            doc.Add(table);
            doc.Add(separator);

            PdfPTable tabletotal = new PdfPTable(2);
            tabletotal.TotalWidth = 210; tabletotal.LockedWidth = true;

            PdfPCell celltotal = new PdfPCell(new Phrase("TOTAL", verdanaFontBoldbig));
            celltotal.HorizontalAlignment = Element.ALIGN_RIGHT; celltotal.PaddingTop = 15f; celltotal.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabletotal.AddCell(celltotal);

            PdfPCell celltotalvalue = new PdfPCell(new Phrase(pedido.Valor.ToString(), verdanaFontBoldbig));
            celltotalvalue.HorizontalAlignment = Element.ALIGN_RIGHT; celltotalvalue.PaddingTop = 15f; celltotalvalue.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabletotal.AddCell(celltotalvalue);


            PdfPCell cellefectivo = new PdfPCell(new Phrase("Efectivo", verdanaFontNoBold));
            cellefectivo.HorizontalAlignment = Element.ALIGN_LEFT; cellefectivo.PaddingTop = 15f; cellefectivo.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabletotal.AddCell(cellefectivo);


            PdfPCell cellefectivovalue = new PdfPCell(new Phrase("$50.000", verdanaFontNoBold));
            cellefectivovalue.HorizontalAlignment = Element.ALIGN_RIGHT; cellefectivovalue.PaddingTop = 15f; cellefectivovalue.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabletotal.AddCell(cellefectivovalue);

            PdfPCell cellcambio = new PdfPCell(new Phrase("CAMBIO", verdanaFontBoldbig));
            cellcambio.HorizontalAlignment = Element.ALIGN_RIGHT; cellcambio.PaddingTop = 15f; cellcambio.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabletotal.AddCell(cellcambio);

            PdfPCell cellcambiovalue = new PdfPCell(new Phrase("$50.000", verdanaFontBoldbig));
            cellcambiovalue.HorizontalAlignment = Element.ALIGN_RIGHT; cellcambiovalue.PaddingTop = 15f; cellcambiovalue.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabletotal.AddCell(cellcambiovalue);

            doc.Add(tabletotal);
            doc.Close();
        }






    }
}