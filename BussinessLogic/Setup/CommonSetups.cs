using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BussinessLogic.Setup
{
	public class CommonSetups : ICommonSetups
	{
		private readonly OTOContext _context;
		private readonly ILogger<CommonSetups> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public CommonSetups(OTOContext context, ILogger<CommonSetups> logger, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<ServiceResponse<object>> AddCodeGenerator(string TypeName)
		{
			try
			{
				var typeexists = _context.Codegenerators.Where(x => x.TypeName.Equals(TypeName)).FirstOrDefault();
				if (typeexists is null)
				{
					var codegenerator = new Codegenerator
					{
						TypeName = TypeName,
						Seed = 1,
						NextNumber = 0,
						Prefix = "D",
						Suffix = string.Empty,
						Length = 2,
						DateCreated = DateTime.UtcNow,
						UserCode = "00001"
					};
					_context.Codegenerators.Add(codegenerator);
					await _context.SaveChangesAsync();
					return ServiceResponse<object>.Information("Code generator created successfully", codegenerator);

				}
				else
				{
					return ServiceResponse<object>.Information("Code generator already exists", null);
				}
			}
			catch (Exception)
			{
				return ServiceResponse<object>.Error("An error occurred while creating the code generator", null);
			}

		}
		public async Task<string> GetCodeGenerator(string TypeName)
		{
			try
			{
				var codegenerator = await _context.Codegenerators.FirstOrDefaultAsync(x => x.TypeName == TypeName);
				if (codegenerator is not null)
				{
					var seed = codegenerator.Seed;
					var nextnumber = codegenerator.NextNumber + seed;
					var prefix = codegenerator.Prefix;
					var suffix = codegenerator.Suffix;
					var length = codegenerator.Length;
					var code = string.Concat(prefix, nextnumber.ToString().PadLeft(length, '0'), suffix);
					codegenerator.NextNumber = nextnumber;
					_context.Codegenerators.Update(codegenerator);
					await _context.SaveChangesAsync();
					return code;
				}
				else
				{
					return string.Empty;
				}
			}
			catch (Exception ex)
			{
				_ = _logger.BeginScope("An error occurred while getting the code generator: {0}", ex.Message);
				return string.Empty;
			}
		}
		public string SentenceCase(string input)
		{
			string[] sentences = input.Split([' '], StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < sentences.Length; i++)
			{
				string sentence = sentences[i].Trim();
				if (!string.IsNullOrEmpty(sentence))
				{
					char[] chars = sentence.ToLower().ToCharArray();
					chars[0] = char.ToUpper(chars[0]);
					sentences[i] = new string(chars);
				}
			}
			string output = string.Join(" ", sentences);
			return output;
		}

		public async Task<string> ConvertIFormFileToBase64Async(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return string.Empty;

			using var memoryStream = new MemoryStream();
			await file.CopyToAsync(memoryStream);
			var fileBytes = memoryStream.ToArray();

			// Convert to Base64 string
			return Convert.ToBase64String(fileBytes);
		}

		public async Task<ServiceResponse<object>> SaveImage(string base64Image, string type, string imageName)
		{
			try
			{
				var image = base64Image.Split(',')[1];
				var bytes = Convert.FromBase64String(image);
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"{type}", $"{imageName}.png");
				await File.WriteAllBytesAsync(path, bytes);
				return ServiceResponse<object>.Success("Image saved successfully", null);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to save image");
				return ServiceResponse<object>.Error("An error occurred while saving the image", null);
			}
		}

		private static string GetRandomAlphanumericString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			Random random = new();
			return new string([.. Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)])]);
		}

		private static readonly Dictionary<int, string> MonthAlphabetMapping = new()
		{
			{ 1, "E" }, { 2, "F" }, { 3, "K" }, { 4, "I" }, { 5, "J" },
			{ 6, "C" }, { 7, "V" }, { 8, "L" }, { 9, "Q" }, { 10, "W" }, { 11, "X" }, { 12, "Z" }
		};

		private static readonly Dictionary<int, string> YearAlphabetMapping = new()
		{
			{ 2023, "P" }, { 2024, "O" }, { 2025, "M" }, { 2026, "S" }, { 2027, "R" },
			{ 2028, "T" }, { 2029, "N" }, { 2030, "Q" }, { 2031, "B" }, { 2032, "J" },
			{ 2033, "R" }, { 2034, "Z" }, { 2035, "K" }, { 2036, "A" }, { 2037, "v" },
			{ 2038, "Y" }
		};

		private static readonly Dictionary<int, string> DayAlphabetMapping = new()
		{
			 { 1, "X" }, { 2, "B" }, { 3, "R" }, { 4, "H" }, { 5, "D" },
			 { 6, "L" }, { 7, "Z" }, { 8, "M" }, { 9, "P" }, { 10, "F" },
			 { 11, "T" }, { 12, "G" }, { 13, "V" }, { 14, "N" }, { 15, "Q" },
			 { 16, "J" }, { 17, "A" }, { 18, "K" }, { 19, "W" }, { 20, "E" },
			 { 21, "Y" }, { 22, "8" }, { 23, "C" }, { 24, "2" }, { 25, "3" },
			 { 26, "4" }, { 27, "1" }, { 28, "9" }, { 29, "5" }, { 30, "6" },
			 { 31, "7" }
		};


		private static readonly Dictionary<int, char> HourAlphabetMapping = new()
		{
			{ 0, 'A' }, { 1, 'B' }, { 2, 'C' }, { 3, 'D' }, { 4, 'E' },
			{ 5, 'F' }, { 6, 'G' }, { 7, 'H' }, { 8, 'I' }, { 9, 'J' },
			{ 10, 'K' }, { 11, 'L' }, { 12, 'M' }, { 13, 'N' }, { 14, 'O' },
			{ 15, 'P' }, { 16, 'Q' }, { 17, 'R' }, { 18, 'S' }, { 19, 'T' },
			{ 20, 'U' }, { 21, 'V' }, { 22, 'W' }, { 23, 'X' }
		};

		private static readonly Dictionary<int, string> SecondAlphabetMapping = new()
		{
			{ 0, "AA" }, { 1, "AB" }, { 2, "AC" }, { 3, "AD" }, { 4, "AE" }, { 5, "AF" },
			{ 6, "AG" }, { 7, "AH" }, { 8, "AI" }, { 9, "AJ" }, { 10, "AK" }, { 11, "AL" },
			{ 12, "AM" }, { 13, "AN" }, { 14, "AO" }, { 15, "AP" }, { 16, "AQ" }, { 17, "AR" },
			{ 18, "AS" }, { 19, "AT" }, { 20, "AU" }, { 21, "AV" }, { 22, "AW" }, { 23, "AX" },
			{ 24, "AY" }, { 25, "AZ" }, { 26, "BA" }, { 27, "BB" }, { 28, "BC" }, { 29, "BD" },
			{ 30, "BE" }, { 31, "BF" }, { 32, "BG" }, { 33, "BH" }, { 34, "BI" }, { 35, "BJ" },
			{ 36, "BK" }, { 37, "BL" }, { 38, "BM" }, { 39, "BN" }, { 40, "BO" }, { 41, "BP" },
			{ 42, "BQ" }, { 43, "BR" }, { 44, "BS" }, { 45, "BT" }, { 46, "BU" }, { 47, "BV" },
			{ 48, "BW" }, { 49, "BX" }, { 50, "BY" }, { 51, "BZ" }, { 52, "CA" }, { 53, "CB" },
			{ 54, "CC" }, { 55, "CD" }, { 56, "CE" }, { 57, "CF" }, { 58, "CG" }, { 59, "CH" }
		};

		private static readonly Dictionary<int, char> MillisecondHundredsMapping = new()
		{
			{ 0, 'A' }, { 1, 'B' }, { 2, 'C' }, { 3, 'D' }, { 4, 'E' }, { 5, 'F' }, { 6, 'G' }, { 7, 'H' }, { 8, 'I' }, { 9, 'J' }
		};

		private static readonly Dictionary<int, char> MillisecondTensUnitsMapping = new()
		{
			{ 0, 'K' }, { 1, 'L' }, { 2, 'M' }, { 3, 'N' }, { 4, 'O' }, { 5, 'P' }, { 6, 'Q' }, { 7, 'R' }, { 8, 'S' }, { 9, 'T' }
		};

		// Minute mapping split into tens and units place for 2-character representation
		private static readonly Dictionary<int, char> TensPlaceMapping = new()
		{
			{ 0, 'A' }, { 1, 'B' }, { 2, 'C' }, { 3, 'D' }, { 4, 'E' }, { 5, 'F' }
		};

		private static readonly Dictionary<int, char> UnitsPlaceMapping = new()
		{
			{ 0, 'G' }, { 1, 'H' }, { 2, 'I' }, { 3, 'J' }, { 4, 'K' }, { 5, 'L' },
			{ 6, 'M' }, { 7, 'N' }, { 8, 'O' }, { 9, 'P' }
		};

		// Method to generate Sale ID using all mappings
		public string GenerateSaleId()
		{
			var date = DateTime.UtcNow;
			var monthLetter = MonthAlphabetMapping[date.Month];
			var yearLetter = YearAlphabetMapping[date.Year];
			var dayLetter = DayAlphabetMapping[date.Day];
			var hourLetter = HourAlphabetMapping[date.Hour];
			var tensMinuteLetter = TensPlaceMapping[date.Minute / 10];
			var unitsMinuteLetter = UnitsPlaceMapping[date.Minute % 10];
			var secondLetter = SecondAlphabetMapping[date.Second];
			var millisecondHundreds = MillisecondHundredsMapping[date.Millisecond / 100];
			var millisecondTens = MillisecondTensUnitsMapping[date.Millisecond % 100 / 10];
			var millisecondUnits = MillisecondTensUnitsMapping[date.Millisecond % 10];
			var millisecondLetters = $"{millisecondHundreds}{millisecondTens}{millisecondUnits}";

			//var second = date.ToString("ss"); // Keeping seconds in numeric form for higher uniqueness
			//var milliseconds = date.ToString("ffff"); // Keeping milliseconds in numeric form for higher uniqueness
			var rand = GetRandomAlphanumericString(2); // Adding 2 random alphanumeric characters for additional uniqueness

			// Creating an array of elements to shuffle
			var uniqueCode = $"{monthLetter}{yearLetter}{dayLetter}{hourLetter}{tensMinuteLetter}{unitsMinuteLetter}{secondLetter}{millisecondLetters}{rand}";
			// Shuffling the elements to increase randomness
			return uniqueCode.ToUpper();
		}


		// Method to generate Sale ID using all mappings

		public string GetHostUrl()
		{
			if (_httpContextAccessor.HttpContext is not null)
			{
				var request = _httpContextAccessor.HttpContext.Request;
				var host = request.Host.Value;
				var scheme = request.Scheme;
				return $"{scheme}://{host}";
			}
			else
			{
				string host = Constants.baseurl;
				return host;
			}
		}

		private static readonly Dictionary<int, string> ShiftMonthAlphabetMapping = new Dictionary<int, string>
		{
			{ 1, "LA" }, { 2, "JB" }, { 3, "VC" }, { 4, "KD" }, { 5, "WE" },
			{ 6, "XF" }, { 7, "VG" }, { 8, "QH" }, { 9, "SI" }, { 10, "BJ" }, { 11, "CK" }, { 12, "FL" }
		};

		private static readonly Dictionary<int, string> ShiftYearAlphabetMapping = new()
		{
			{ 2023, "MN" }, { 2024, "NO" }, { 2025, "OP" }, { 2026, "PQ" }, { 2027, "QR" }, { 2028, "RS" }, { 2029, "ST" }, { 2030, "TU" }
		};

		private static readonly Dictionary<int, char> ShiftDayAlphabetMapping = new()
		{
			{ 1, 'X' }, { 2, 'Y' }, { 3, 'Z' }, { 4, 'A' }, { 5, 'B' },
			{ 6, 'C' }, { 7, 'D' }, { 8, 'E' }, { 9, 'F' }, { 10, 'G' },
			{ 11, 'H' }, { 12, 'I' }, { 13, 'J' }, { 14, 'K' }, { 15, 'L' },
			{ 16, 'M' }, { 17, 'N' }, { 18, 'O' }, { 19, 'P' }, { 20, 'Q' },
			{ 21, 'R' }, { 22, 'S' }, { 23, 'T' }, { 24, 'U' }, { 25, 'V' },
			{ 26, 'W' }, { 27, 'X' }, { 28, 'Y' }, { 29, 'Z' }, { 30, 'A' },
			{ 31, 'B' }
		};

		public string GenerateShiftNumber()
		{
			var date = DateTime.UtcNow;

			// Check if the year exists in the dictionary, otherwise handle error
			if (!ShiftYearAlphabetMapping.TryGetValue(date.Year, out var yearLetter))
			{
				throw new InvalidOperationException($"Year {date.Year} is not supported.");
			}

			var monthLetter = ShiftMonthAlphabetMapping[date.Month];
			var dayLetter = ShiftDayAlphabetMapping[date.Day];
			var timePortion = date.ToString("HHmmssfff");

			// Removed ToUpper() since everything is uppercase already
			var uniqueCode = $"{yearLetter}{monthLetter}{dayLetter}{timePortion}";

			return uniqueCode;
		}

	}
}
