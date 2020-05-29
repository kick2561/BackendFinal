
using StoreBuy.Domain;


using StoreBuy.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using StoreBuy.Utilities;
using System.Net.Http.Formatting;


namespace StoreBuy.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

        IUserRepository UserRepository = null;
        IOTPRepository OTPRepository = null;
        public UserController(IUserRepository UserRespository,
            IOTPRepository OTPRepository)
        {
            this.OTPRepository = OTPRepository;
            this.UserRepository = UserRespository;
        }


        [HttpGet]
        [Route("GetAllUserDetails")]
        public IEnumerable<Users> GetAllUserDetails()
        {
            try
            {
                return UserRepository.GetAll();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }


        [HttpGet]
        [Route("GetUserByUserName")]
        public Users GetUserByUserName(string UserName)
        {
            try
            {
                return UserRepository.GetUserByUserName(UserName);
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }



        [HttpPost]
        [Route("UserLogin")]
        public HttpResponseMessage UserLogin(FormDataCollection Collection)
        {
            try
            {
                Users userDetails = UserRepository.GetUserByUserName(Collection["Email"]);



                if (userDetails != null)
                {
                    var UserPassword = Collection["UserPassword"];
                    if (UserPassword.Equals(userDetails.UserPassword))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, "successfully sent");


                    }
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Successfully not sent");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(FormDataCollection Collection)
        {
            try
            {
                var UserId = Int64.Parse(Collection["UserId"]);
                string OldPassword = Collection["OldPassword"];
                string NewPassword = Collection["NewPassword"];


                Users user = UserRepository.GetById(UserId);
                if (user != null)
                {
                    if (OldPassword.Equals(user.UserPassword))
                    {
                        user.UserPassword = NewPassword;
                    }
                    else
                        return BadRequest();
                    UserRepository.InsertOrUpdate(user);
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }




        [HttpPost]
        [Route("UserCheck")]
        public HttpResponseMessage UserCheck(FormDataCollection Collection)
        {
            try
            {
                var email = Collection["Email"];
                Users CheckUser = UserRepository.GetUserByUserName(email);
                if (CheckUser != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Found, "User Already Found");


                }
                var OTP = Utility.GetRandomNumber();

                OTPValidator OTPToInsert = new OTPValidator();
                OTPToInsert.Email = Collection["Email"];
                OTPToInsert.CurrentOtp = OTP;
                OTPToInsert.DateTime = DateTime.Now;
                OTPRepository.InsertOrUpdate(OTPToInsert);
                var IsSent = Utility.SendEmail(email, Resources.OTPBody + OTP, Resources.EmailVerificationSubject);
                if (IsSent)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Successfully OTP sent");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, "Successfully OTP sent");


                }


            }
            catch (Exception exception)
            {


                throw exception;
            }
        }



        [HttpPost]
        [Route("UserRegister")]
        public HttpResponseMessage UserRegister([FromBody]Users User)
        {
            try
            {
                UserRepository.InsertOrUpdate(User);
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully Created");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "Not Inserted");


            }
        }


        [HttpPut]
        [Route("UpdateUserDetails")]
        public IHttpActionResult UpdateUserDetails([FromBody]Users user)
        {


            try
            {
                UserRepository.InsertOrUpdate(user);
                return Ok(user);
            }


            catch (Exception Exception)
            {
                throw Exception;
            }
        }


        [HttpPost]
        [Route("ForgotPassword")]
        public HttpResponseMessage ForgotPassword(FormDataCollection Collection)
        {

            var Email = Collection["Email"];
            Users User = UserRepository.GetUserByUserName(Email);
            if (User == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User doesn't exist");
            var OTP = Utility.GetRandomNumber();
            OTPValidator OTPToInsert = new OTPValidator
            {
                Email = Email,
                CurrentOtp = OTP,
                DateTime = DateTime.Now
            };
            OTPRepository.InsertOrUpdate(OTPToInsert);
            string body = Resources.OTPBody + OTP;
            var IsEmailSent = Utility.SendEmail(Email, body, Resources.OTPSubject);
            if (IsEmailSent)
            {
                return Request.CreateResponse(HttpStatusCode.Found, "Successfully sent");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User with UserId:" + Email + " not found");
            }
        }
        [HttpPost]
        [Route("VerifyOTP")]
        public HttpResponseMessage VerifyOTP(FormDataCollection Collection)
        {
            DeleteExpiredOTPs();
            var OTP = OTPRepository.GetById(Collection["Email"]);
            if (OTP == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "OTP Expired");
            }
            if (Int64.Parse(Collection["OTPReceived"]) == OTP.CurrentOtp)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Found, "Entered OTP is not valid");
            }


        }


        void DeleteExpiredOTPs()
        {
            var AllOTPs = OTPRepository.GetAll();
            foreach (OTPValidator OTP in AllOTPs)
            {
                DateTime strDate = OTP.DateTime;
                var span = DateTime.Now - strDate;
                if (span.Minutes >= Int32.Parse(Resources.OTPTimeSpan))
                {
                    OTPRepository.Delete(OTP.Email);
                }
            }
        }


        [HttpPost]
        [Route("ResetPassword")]
        public HttpResponseMessage ResetPassword(FormDataCollection Collection)//email and newpassword required
        {
            try
            {
                var NewPassword = Collection["NewPassword"];
                Users User = UserRepository.GetUserByUserName(Collection["Email"]);
                if (User != null)
                {
                    User.UserPassword = NewPassword;
                    UserRepository.InsertOrUpdate(User);
                    return Request.CreateResponse(HttpStatusCode.OK, "Successfully changed Password");
                }

                return Request.CreateResponse(HttpStatusCode.NotModified, "Password not changed");
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }




    }
}












