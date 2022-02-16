namespace CarDataApplicationAPI.Models.ModelsFromDB
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

        public ChangePasswordModel(int userid, string newpassword, string oldpassword)
        {
            this.UserId = userid;
            this.NewPassword = newpassword;
            this.OldPassword = oldpassword;
        }
    }
}
