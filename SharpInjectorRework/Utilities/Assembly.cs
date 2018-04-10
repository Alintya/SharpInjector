using System;
using System.IO;

namespace SharpInjectorRework.Utilities
{
    internal class Assembly
    {
        public static DateTime GetLinkerTime(TimeZoneInfo target = null)
        {
            try
            {
                var buffer = new byte[2048];
                using (var stream = new FileStream(System.Reflection.Assembly.GetExecutingAssembly().Location, FileMode.Open, FileAccess.Read))
                    stream.Read(buffer, 0, 2048);

                return TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(
                        BitConverter.ToInt32(buffer, BitConverter.ToInt32(buffer, 60) + 8)), target ?? TimeZoneInfo.Local);
            }
            catch (Exception e)
            {
                Globals.MessageboxExtension.ShowWarning($"failed to get assembly creation date: {e}");
                return DateTime.Now;
            }
        }
    }
}
