using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Ensek.UnitTest.ConsumerMeterDataService;

public static class TestHelper
{
    internal static IFormFile CreateTestFormFile(string fileName, string content)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(content);

        return new FormFile(
            baseStream: new MemoryStream(bytes),
            baseStreamOffset: 0,
            length: bytes.Length,
            name: "Data",
            fileName: fileName
        );
    }
}