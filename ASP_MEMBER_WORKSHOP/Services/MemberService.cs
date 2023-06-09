using ASP_MEMBER_WORKSHOP.Entitiy;
using ASP_MEMBER_WORKSHOP.Interfaces;
using ASP_MEMBER_WORKSHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MEMBER_WORKSHOP.Services
{
    public class MemberService : IMemberService
    {
        private DatabaseEntities db = new DatabaseEntities();

        public IEnumerable<Members> MemberItems => this.db.Members.ToList();

        public void UpdateProfile(string email, ProfileModel model)
        {
            try
            {
                var memberItem = this.db.Members.SingleOrDefault(item => item.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found member");
                this.db.Members.Attach(memberItem);
                memberItem.firstname = model.firstname;
                memberItem.lastname = model.lastname;
                memberItem.position = model.position;
                memberItem.updated = DateTime.Now;

                memberItem.image = null;

                this.db.Entry(memberItem).State = System.Data.Entity.EntityState.Modified;
                this.db.SaveChanges();

            }
            catch(Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}
