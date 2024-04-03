namespace ASP_Work.Data.Entity
{
    public class PasswordResetToken
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public String TokenValue { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }

        // Navigation property for the related User
        public User User_PasswordResetToken { get; set; } = null!;
    }
}
