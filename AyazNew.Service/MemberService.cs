using AyazNew.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazNew.Service
{
    public class MemberService : BaseService<Member>
    {
        public ServiceResult<Member> Auth(string userName, string password)
        {
            var member = repository.Select(x => x.UserName == userName && x.Password == password);
            if (member.Any())
                return new ServiceResult<Member>(member.First());

            return new ServiceResult<Member>(ServiceResultCode.RecordNotFound, "Kullanıcı adı veya şifre yanlış");
        }
        public ServiceResult<Member> GetUserName(string username)
        {
            var member = repository.Select(x => x.UserName == username);
            if (member.Any())
                return new ServiceResult<Member>(member.First());
            return new ServiceResult<Member>(ServiceResultCode.RecordNotFound, "Kayıt Bulunamadı");

        }
        public ServiceResult<Member> Ban(int id)
        {
            var member = repository.FindById(id);
            if (member == null)
                return new ServiceResult<Member>(ServiceResultCode.RecordNotFound);
            member.Status = DataStatus.Banned;
            repository.Update(member);
            repository.SaveChanges();
            return new ServiceResult<Member>(ServiceResultCode.Success, "Successfully Banned");
        }
        public ServiceResult<Member> Unban(int id)
        {
            var member = repository.FindById(id);
            if (member == null)
                return new ServiceResult<Member>(ServiceResultCode.RecordNotFound);
            member.Status = DataStatus.Active;
            repository.Update(member);
            repository.SaveChanges();
            return new ServiceResult<Member>(ServiceResultCode.Success, "Successfully Unbanned");
        }
    }
}
