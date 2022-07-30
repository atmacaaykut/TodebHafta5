using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgroundJobs.Abstract;

namespace BackgroundJobs.Concrete
{
    public class SendMailService:ISendMailService
    {
        public void SendMail(int userId, string name)
        {
            //mail atıldığını farz ediyoruz
            Console.WriteLine($"{userId} li {name} isimli kişiye mail atıldı");
        }
    }
}
