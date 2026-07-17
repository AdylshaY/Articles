namespace Submission.Domain.Entities
{
    using Submission.Domain.ValueObjects;

    public partial class Author
    {
        public static Author Create(string email, string firstName, string lastName, string? title, string affiliation)
        {
            var author =  new Author
            {
                EmailAddress = EmailAddress.Create(email),
                FirstName = firstName,
                LastName = lastName,
                Title = title,
                Affiliation = affiliation,
            };

            //TODO: Add AuthorCreated domain event here if needed

            return author;
        }
    }
}
