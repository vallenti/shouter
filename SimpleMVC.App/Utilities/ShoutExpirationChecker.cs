using SimpleMVC.App.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMVC.App.Utilities
{
    public static class ShoutExpirationChecker
    {
        public async static void Run()
        {
            while (true)
            {
                await Task.Run(() => CleanExpiredShouts());
                Thread.Sleep(60000); // 1 minute
            }
        }
        private static void CleanExpiredShouts()
        {
            using (var context = new ShouterContext())
            {
                DateTime? now = DateTime.Now;

                var shouts = context.Shouts
                    .Where(s => Nullable.Compare(s.ExpiresOn, now) < 0)
                    .ToList();

                context.Shouts.RemoveRange(shouts);
                context.SaveChanges();
                Console.WriteLine(DateTime.Now);
            }
        }
    }
}
