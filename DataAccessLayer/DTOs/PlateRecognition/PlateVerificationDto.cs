using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTOs.PlateRecognition
{
	public record PlateVerificationDto(
		bool Matched,
		string RecognizedPlate,
		string? MatchedVehicleRegistration,
		double Confidence,
		List<string> CandidatePlates
	);
}
