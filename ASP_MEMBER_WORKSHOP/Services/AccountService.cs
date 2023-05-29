using ASP_MEMBER_WORKSHOP.Entitiy;
using ASP_MEMBER_WORKSHOP.Interfaces;
using ASP_MEMBER_WORKSHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MEMBER_WORKSHOP.Services
{
    public class AccountService : IAccountService
    {

        private DatabaseEntities db = new DatabaseEntities();

        public bool Login(LoginModel model)
        {
            try
            {
                var memberItem = this.db.Members.SingleOrDefault(m => m.email.Equals(model.email)); //ตรวจสอบ email
                if (memberItem != null)
                {
                    return PasswordHashModel.Verify(model.password,memberItem.password);
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        // ลงทะเบียน
        public void Register(RegisterModel model)
        {
            try
            {
                this.db.Members.Add(new Members
                {
                    firstname = model.firstname,
                    lastname = model.lastname,
                    email = model.email,
                    password = model.password,
                    position = "",
                    image = null,
                    role = RoleAccount.Member,
                    created = DateTime.Now,
                    updated = DateTime.Now,
                });

                this.db.SaveChanges(); //คำสั่ง Save ข้อมูลลง Database
            }
            catch (Exception ex) 
            {
                throw ex.GetErrorException();
            }

        }
    }
}