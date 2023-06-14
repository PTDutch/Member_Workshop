using ASP_MEMBER_WORKSHOP.Entitiy;
using ASP_MEMBER_WORKSHOP.Interfaces;
using ASP_MEMBER_WORKSHOP.Models;
using ASP_MEMBER_WORKSHOP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static ASP_MEMBER_WORKSHOP.AuthenticationHandler;

namespace ASP_MEMBER_WORKSHOP.Controllers
{
    [Authorize]
    public class MemberController : ApiController
    {
        private IMemberService memberService;
        public MemberController()
        {
            this.memberService = new MemberService();
        }

        // แสดงข้อมูลผู้ใช้งานที่เข้าสู่ระบบมา
        [Route("api/member/data")]
        public MembersModel GetMemberLogin()
        {
            var userLogin = this.memberService.MemberItems
                .SingleOrDefault(item => item.email.Equals(User.Identity.Name));
            if (userLogin == null) return null;
            return new MembersModel
            {
                id = userLogin.id,
                firstname = userLogin.firstname,
                lastname = userLogin.lastname,
                email = userLogin.email,
                position = userLogin.position,
                image_type = userLogin.image_type,
                image_byte = userLogin.image,
                role = userLogin.role,
                created = userLogin.created,
                updated = userLogin.updated
            };
        }

        // บันทึกข้อมูลโปรไฟล์
        [Route("api/member/profile")]
        public IHttpActionResult postUpdateProfild([FromBody] ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.memberService.UpdateProfile(User.Identity.Name, model);
                    return Ok(this.GetMemberLogin());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
                
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        // เปลี่ยนรหัสผ่าน
        [Route("api/member/change-password")]
        public IHttpActionResult PostChangPassword([FromBody] ChangePasswordModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    this.memberService.ChangPassword(User.Identity.Name, model);
                    return Ok(new { message = "Password has chang." });
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        // แสดงรายการสมาชิกทั้งหมด
        [Authorize(Roles = "Employee,Admin")]
        public GetMemberModel GetMembers([FromUri] MemberFilterOptions filters)
        {
            //return this.memberService.GetMembers();
            if (ModelState.IsValid)
            {
                return this.memberService.GetMembers(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }

        // แสดงรายการสมาชิกคนเดียวจาก Id
        [Authorize(Roles = "Admin")]
        public MembersModel GetMember(int id)
        {
            return this.memberService.MemberItems
                .Select(m => new MembersModel
                {
                    id = m.id,
                    firstname = m.firstname,
                    lastname = m.lastname,
                    email = m.email,
                    position = m.position,
                    image_type = m.image_type,
                    image_byte = m.image,
                    role = m.role,
                    created = m.created,
                    updated = m.updated
                })
                .SingleOrDefault(m => m.id == id);
        }

        // สร้างข้อมูลสมาชิกใหม่
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostCreatMember([FromBody] CreateMemberModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.memberService.CreateMember(model);
                    return Ok("Create successful");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
            }

            return BadRequest(ModelState.GetErrorModelState());
        }

        // ลบข้อมูลสมาชิก
        [Authorize(Roles = "Employee,Admin")]
        public IHttpActionResult DeleteMember(int id)
        {
            try
            {
                this.memberService.DeleteMember(id);
                return Ok("Delete successful");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.Message);
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        // แก้ไขข้อมูลสมาชิก
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutUpdateMember(int id, [FromBody] UpdateMemberModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.memberService.UpdateMember(id, model);
                    return Ok("Update successful");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        // เพิ่มข้อมูลสมาชิก (จำลอง)
        [Route("api/member/generate")]
        public IHttpActionResult PostGenerateMembers()
        {
            try
            {
                var memberItem = new List<Members>();
                var password = PasswordHashModel.Hash("123456");
                var position = new string[] { "Frontend Developer", "Backend Developer" };
                var roles = new RoleAccount[] { RoleAccount.Member, RoleAccount.Employee, RoleAccount.Admin };
                var random = new Random();

                for (var index = 1; index <= 97; index++)
                {
                    memberItem.Add(new Members
                    {
                        email = $"mail-{index}@mail.com",
                        password = password,
                        firstname = $"Firstname {index}",
                        lastname = $"Lastname {index}",
                        position = position[random.Next(0, 2)],
                        role = roles[random.Next(0, 3)],
                        created = DateTime.Now,
                        updated = DateTime.Now
                    });
                }
                var db = new DatabaseEntities();
                db.Members.AddRange(memberItem);    
                db.SaveChanges();
                return Ok("Generate successful");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex);
                return BadRequest(ModelState.GetErrorModelState());
            }
        }
    }
}
