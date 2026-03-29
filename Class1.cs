using System;
using System.Drawing;

class Program
{
    static void Main()
    {
        // Receipt details
        string stationName = "ZimmerMan OtoGas Station";
        string receiptNumber = "123456";
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string vehicleNumberPlate = "XYZ 1234";
        double litres = 20.5;
        double totalAmount = 50.75;
        double vat = 0;

        // Create a bitmap image
        int width = 400, height = 500;
        using (Bitmap bitmap = new Bitmap(width, height))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Set background color
                graphics.Clear(Color.FromArgb(234, 234, 234));

                // Fonts and brushes
                Font headerFont = new Font("Arial", 14, FontStyle.Bold);
                Font textFont = new Font("Arial", 12);
                Brush textBrush = Brushes.Black;
                Brush accentBrush = new SolidBrush(Color.Fuchsia);

                // Draw the header
                graphics.DrawString("Gas Station Receipt", headerFont, accentBrush, new PointF(10, 10));
                graphics.DrawString($"Station: {stationName}", textFont, textBrush, new PointF(10, 50));
                graphics.DrawString($"Receipt No: {receiptNumber}", textFont, textBrush, new PointF(10, 80));
                graphics.DrawString($"Date: {date}", textFont, textBrush, new PointF(10, 110));
                graphics.DrawString($"Number Plate: {vehicleNumberPlate}", textFont, textBrush, new PointF(10, 140));

                // Draw transaction details
                graphics.DrawString($"Litres: {litres:F2}", textFont, textBrush, new PointF(10, 180));
                graphics.DrawString($"Total Amount: ${totalAmount:F2}", textFont, textBrush, new PointF(10, 210));
                graphics.DrawString($"VAT: ${vat:F2}", textFont, textBrush, new PointF(10, 240));

                // Draw footer
                graphics.DrawString("Thank you for your visit!", textFont, accentBrush, new PointF(10, 280));

                // Save the bitmap to a file
                string outputPath = "Receipt.png";
                bitmap.Save(outputPath);

                Console.WriteLine($"Receipt saved as {outputPath}");
            }
        }
    }
}
