using System.IO;
using LiteDB;

namespace TimeRegistrationAcceptanceTests
{
    public abstract class RepositoryTestUtils
    {
        public static LiteDatabase CreateNewDatabase()
        {
            var filename = Path.GetTempFileName();
            var connectionString = $"Filename={filename}.db; Mode=Exclusive";
            return new LiteDatabase(connectionString);
        }
    }
}