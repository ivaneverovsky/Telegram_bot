using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viikkkvv_bot.TelegramBotData.Model;

namespace Viikkkvv_bot.Data
{
    class DBService
    {
        DBConnection _db = new DBConnection();
        private List<object> dbData = new List<object>();
        public async Task<List<Subscribers>> GetSubsAsync()
        {
            string request = @"SELECT * FROM [exampledb].[dbo].[Subscribers]";
            List<Subscribers> subs = new List<Subscribers>();

            await _db.CreateConnection();
            dbData = await _db.SendCommandRequest(request);
            _db.CloseConnection();

            if (dbData != null)
            {
                for (int i = 0; i < dbData.Count; i++)
                {
                    object[] value = (object[])dbData[i];
                    Subscribers subscr = new Subscribers(
                        value[0].ToString(),
                        value[1].ToString(),
                        value[2].ToString(),
                        value[3].ToString()
                        );
                    subs.Add(subscr);
                }
                dbData.Clear();
            }

            return subs;
        }
        public async Task SetSubAsync(string username, string email, string birthdate, string phone)
        {
            string request = @"insert into [exampledb].[dbo].[Subscribers] (Username, Email, Birthdate, Phone) values (N'" + username + "', N'" + email + "', N'" + birthdate + "', N'" + phone + "')";

            await _db.CreateConnection();
            await _db.SendCommandSet(request);
            _db.CloseConnection();
        }

        public async Task DeleteSubAsync()
        {
            string request = @"delete from [exampledb].[dbo].[Subscribers]";

            await _db.CreateConnection();
            await _db.SendCommandDelete(request);
            _db.CloseConnection();
        }
    }
}
