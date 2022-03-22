namespace DevJobs.API.Entities
{
    public class JobApplication
    {
        public JobApplication(string applicantName, string applicantAmail, int idJobVacancy)
        {
            ApplicantName = applicantName;
            ApplicantAmail = applicantAmail;
            IdJobVacancy = idJobVacancy;
        }

        public int Id { get; private set; }
        public string ApplicantName { get; private set; }
        public string ApplicantAmail { get; private set; }
        public int IdJobVacancy { get; private set; }
    }
}