using System;

namespace ChristmasLottery
{
    public class UserData
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int? GiftForUserId { get; set; }

        public virtual UserData GiftForUser { get; set; }
        
        public string Wants { get; set; }

        public string Picture { get; set; }
        public string Password { get; set; }

    }
}
