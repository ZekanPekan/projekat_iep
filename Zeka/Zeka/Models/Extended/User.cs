using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Zeka.Utils;

namespace Zeka.Models
{
    public partial class User
    {
        public static bool EmailExists(string email)
        {
            using (Models.Database db = new Models.Database())
            {
                var exists = db.User.Where(u => u.email == email).FirstOrDefault();
                return exists != null;
            }
        }

        public static User CreateFrom(FormRegisterUser fr)
        {
            User u = new User();
            u.password = CryptUtils.Hash(fr.password);
            u.email = fr.email;
            u.last_name = fr.lastName;
            u.first_name = fr.firstName;
            u.tokens = 0;
            u.admin_flag = false;
            return u;
        }

        public Boolean ValidatePassword(String pass)
        {
            return String.Equals(this.password, CryptUtils.Hash(pass));
        }

        public static User getByEmail(String email)
        {
            using (Models.Database db = new Models.Database())
            {
                var user = db.User.Where(u => u.email == email).FirstOrDefault();
                return user;
            }
        }

        public static Boolean Pay(int amount,Guid id)
        {
                User user = getById(id);
                if (user == null)
                    return false;

                if (user.tokens < amount)
                    return false;

                user.tokens -= amount;
                user.saveChanges();
                return true;
        }

        public static User getById(Guid key)
        {
            using(Models.Database db = new Models.Database())
            {
                var user = db.User.Where(u => u.user_id == key).FirstOrDefault();
                return user;
            }
        }

        public void save()
        {
            using (Database db = new Database())
            {
                db.User.Add(this);
                db.SaveChanges();
            }
        }

        public void saveChanges()
        {
            using (Database db = new Database())
            {
                db.Entry(this).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }

    public class FormRegisterUser
    {
        [Display(Name = "First name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in first name")]
        [MaxLength(50, ErrorMessage = "50 characters is maximum")]
        public string firstName { get; set; }


        [Display(Name = "Last name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in last name")]
        [MaxLength(50, ErrorMessage = "50 characters is maximum")]
        public string lastName { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "50 characters is maximum")]
        public string email { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in password")]
        [DataType(DataType.Password)]
        [MinLength(7, ErrorMessage = "7 characters minimum")]
        public string password { get; set; }
    }

    public class FormChangeUser
    {
        [Display(Name = "First name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in first name")]
        [MaxLength(50, ErrorMessage = "50 characters is maximum")]
        public string firstName { get; set; }


        [Display(Name = "Last name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in last name")]
        [MaxLength(50, ErrorMessage = "50 characters is maximum")]
        public string lastName { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "50 characters is maximum")]
        public string email { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Fill in password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class FormLoginUser
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in password")]
        [DataType(DataType.Password)]
        [MinLength(7, ErrorMessage = "7 characters minimum")]
        public string password { get; set; }
    }

}