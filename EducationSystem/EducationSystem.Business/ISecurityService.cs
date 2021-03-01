namespace EducationSystem.Business
{
    public interface ISecurityService
    {
        string GetHash(string password);
        bool VerifyHashAndPassword(string hashedPwdFromDatabase, string userEnteredPassword);
    }
}