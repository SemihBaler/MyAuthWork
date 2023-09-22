namespace MyAuthWork.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                UserName = "Semih_Admin",
                Email = "semihbaler@gmail.com",
                Password = "69586__semmer",
                GivenName = "Semih",
                Surname = "Baler",
                Role="Administrator"
            },
            new UserModel()
            {
                 UserName = "Merve_Seller",
                Email = "merve@gmail.com",
                Password = "1234",
                GivenName = "Merve",
                Surname = "Baler",
                Role="Seller"
            }
        };
    }
}
