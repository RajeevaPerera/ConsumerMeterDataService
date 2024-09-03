using System.Text.RegularExpressions;
using Ensek.Models.ConsumerMeterDataService.Internal;

namespace Ensek.BusinessLayer.ConsumerMeterDataService.Parsers;

public class MeterReadingParser : IMeterReadingParser
{
    public (IList<MeterReading> meterReadings, int failedToReadCount) CvsParser(string csvString)
    {
        var meterReadings = new List<MeterReading>();
        var failedToReadCount = 0;

        string[] csvLines = csvString.Split(
            new[] { "\n" },
            StringSplitOptions.None
        );

        for (var row = 1; row <= csvLines.Length; row++)
        {
            // skip over the first row which contains headers
            if (row == 1)
            {
                continue;
            }

            var parts = csvLines[row - 1].Split(',');

            // check the row contains data
            if (parts.Length != 4)
            {
                failedToReadCount++;

                continue;
            }

            // validate the AccountId
            var parsedIdResult = int.TryParse(parts[0], out var parsedId);
            
            // validate meter reading datetime
            var parsedDateTimeResult = DateTime.TryParse(parts[1], out var parsedDateTime);

            // check that the meter reading value is in the correct format and only 5 characters long
            var readingRegex = new Regex("^[A-Za-z0-9]{5}$");
            var readingRegexResult = readingRegex.IsMatch(parts[2]);
            
            //check that meter reading value can parse as an int
            var meterReadingSuccess = int.TryParse(parts[2], out var meterReadingValue);

            if (!parsedIdResult || !parsedDateTimeResult || !readingRegexResult || !meterReadingSuccess )
            {
                failedToReadCount++;
                continue;
            }

            meterReadings.Add(new MeterReading
            {
                AccountId = parsedId,
                MeterReadingDateTime = parsedDateTime,
                MeterReadValue = meterReadingValue,
            });
                
        }

        return (meterReadings, failedToReadCount);
    }
}