using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using AddressBook.Models;
using WebMatrix.WebData;
using System.Net.Mail;

namespace AddressBook.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
                {
                    string hashPass = AddressBook.Cyrpto.Hash(u.PasswordHashed);
                    var v = dc.Users.Where(a => a.Email.Equals(u.Email) && a.PasswordHashed.Equals(hashPass)).FirstOrDefault();
                    if (v != null)
                    {
                        
                        Session["LogedUserID"] = v.Id.ToString();
                        Session["LogedUserFirstName"] = v.FirstName.ToString();

                        var lName = v != null ? Convert.ToString(v.LastName) : null;

                        if (lName != null)
                        {
                            Session["LogedUserLastName"] = v.LastName.ToString();
                        }

                        @Session["IsAdmin"] = v.IsAdmin.ToString();

                        return RedirectToAction("AfterLogin");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Invalid login attempt");
                        return View();
                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/");

        }

        public ActionResult GetSessionId(bool pid)
        {
            if (pid == true)
            {
                
                int id;
                id = Convert.ToInt32(Session["LogedUserID"]);

                return RedirectToAction("MyProfile", "Home", new { id = id });
            }

            else
                return View();
        }

        // GET: Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            
                using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
                {
                    var v = dc.Users.Where(a => a.Email.Equals(model.Email)).FirstOrDefault();
                    if (v != null)
                    {
                        // Generae password token that will be used in the email link to authenticate user
                        var token = AddressBook.Cyrpto.RandomString();
                        // Generate the html link sent via email
                        string html = "<!DOCTYPE html><html lang=" + "en" + "> <head><meta charset=" + "UTF-8"
                            + "><title>Title</title> </head> <body> <p>Your temporary password : <span>" + token + "</span></p> </body> </html>";
                        string resetLink = html + "<a href='"
                           + Url.Action("ResetPassword", "Account", new { rt = model.Email }, "http")
                           + "'>Reset Password Link</a>";

                        // Email stuff
                        string subject = "Reset your password for IDC Address Book";
                        string body = resetLink;
                        string from = "shalvijain04@gmail.com";

                        MailMessage message = new MailMessage(from, model.Email);
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient();

                        // Attempt to send the email
                        try
                        {
                            client.Send(message);
                            v.resetPassword = token;
                            dc.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", "Issue sending email: " + e.Message);
                        }
                    }
                    else
                    {
                        /* Note: You may not want to provide the following information
                    * since it gives an intruder information as to whether a
                    * certain email address is registered with this website or not.
                    * If you're really concerned about privacy, you may want to
                    * forward to the same "Success" page regardless whether an
                    * user was found or not. This is only for illustration purposes.
                    */
                    ModelState.AddModelError("", "No user found by that email.");
                    }
                    ViewBag.Message = "Reset Password link sent to your email";
                }
            

            /* You may want to send the user to a "Success" page upon the successful
            * sending of the reset email link. Right now, if we are 100% successful
            * nothing happens on the page. :P
            */
            return View(model);
        }



        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string rt)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ReturnToken = rt;
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
                {
                    var v = dc.Users.Where(a => a.Email.Equals(model.ReturnToken) && a.resetPassword.Equals(model.TempPassword)).FirstOrDefault();
                    if (v != null)
                    {
                        v.PasswordHashed = AddressBook.Cyrpto.Hash(model.Password);
                        v.resetPassword = null;
                        dc.SaveChanges();
                        ViewBag.Message = "Password Successfully Reset";
                        Session["LogedUserID"] = v.Id.ToString();
                        Session["LogedUserFirstName"] = v.FirstName.ToString();
                        var lName = v != null ? Convert.ToString(v.LastName) : null;

                        if (lName != null)
                        {
                            Session["LogedUserLastName"] = v.LastName.ToString();
                        }

                        @Session["IsAdmin"] = v.IsAdmin.ToString();

                        return RedirectToAction("AfterLogin");
                    }
                    else
                    {
                        ViewBag.Message = "We can't find you!";
                       
                    }
                }
            }
            return View(model);
            
        }

    }
}
