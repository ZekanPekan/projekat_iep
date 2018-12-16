using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zeka.Utils;

namespace Zeka.Models
{
    public partial class Auction
    {   
        public static Auction Create(FormCreateAuction fcAuction, Guid user_id)
        {
            Auction auction = new Auction();
            auction.title = fcAuction.title;
            auction.starting_price = fcAuction.starting_price;
            auction.duration = fcAuction.duration;
            auction.picture = new byte[fcAuction.image.ContentLength];
            fcAuction.image.InputStream.Read(auction.picture, 0, fcAuction.image.ContentLength);
            auction.current_price = 0;
            auction.created = DateTime.Now;
            auction.state = KeysUtils.AuctionReady();
            SystemConf config = SystemConf.GetSystemConf();
            auction.currency = config.currency;
            auction.tokenValue = config.token_value;
            auction.token_price =(int)Math.Ceiling(auction.starting_price / auction.tokenValue);
            auction.user_id = user_id;
            return auction;
        }

        public static List<Auction> getAll()
        {
            using (Database db = new Database())
            {
              return db.Auction.ToList();
            }
        }


        public void save()
        {
            using (Database db = new Database())
            {
                db.Auction.Add(this);
                db.SaveChanges();
            }
        }
    }
 
    public class FormCreateAuction
    {
        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fill in the Title")]
        [MaxLength(100, ErrorMessage = "100 characters is maximum")]
        public string title { get; set; }

        [Display(Name = "Starting price")]
        [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage = "Starting price must be greater than 0")]
        public decimal starting_price { get; set; }

        [Display(Name = "Duration in seconds")]
        [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage = "Duration must be greater than 0")]
        public long duration { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please select image")]
        public HttpPostedFileBase image { get; set; }

    }
}