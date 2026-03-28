namespace StayFocus.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string Author { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int HelpfulCount { get; set; }
    }

    public class HelpRequest
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public string RequesterEmail { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedDate { get; set; }
    }
}
