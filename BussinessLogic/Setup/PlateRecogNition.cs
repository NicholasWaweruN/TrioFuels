using System.Drawing;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;


namespace BussinessLogic.Setup 
{
	public class PlateRecognition
	{
		public PlateRecognition()
		{ }
		public string ExtractPlateNumber(string base64Image)
		{
			try
			{
				// Step 1: Decode the base64 image
				byte[] imageBytes = Convert.FromBase64String(base64Image);
				using MemoryStream ms = new(imageBytes);
				using Bitmap bitmap = new(ms);
				// Step 2: Convert Bitmap to Pix format by saving to a memory stream as TIFF
				using MemoryStream tiffStream = new();
				bitmap.Save(tiffStream, ImageFormat.Tiff);
				tiffStream.Position = 0;

				// Step 3: Load Pix from TIFF memory stream
				using Pix pixImage = Pix.LoadTiffFromMemory(tiffStream.ToArray());
				// Step 4: Apply OCR using Tesseract
				using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
				using var page = engine.Process(pixImage);
				string plateText = page.GetText();
				return plateText.Trim(); // Clean up the extracted text
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error extracting plate number: {ex.Message}");
				return string.Empty;
			}


		}
		public  string PlateNumber(string base64Image)
		{
			PlateRecognition recognizer = new();
			string plateNumber = recognizer.ExtractPlateNumber(base64Image);

			if (plateNumber != null)
			{
				return "Detected Plate Number: " + plateNumber;
			}
			else
			{
				return "Plate number recognition failed.";
			}
		}

	}
}