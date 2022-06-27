namespace VotingSystemConsoleApp
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int VoteLimit { get; set; } = 1;
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public UsersModel(int Id, string Name, int Age, string Address,string Username, string Password,int Role)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;  
            this.Address = Address;
            this.UserName = Username;
            this.Password = Password;
            this.Role = Role;
            this.VoteLimit = 1;
        }
    }

    class CandidateVotes
    {
        public UsersModel Voter { get; set; }
        public UsersModel Candidate { get; set; }
        public CandidateVotes(UsersModel Voter,UsersModel candidateModel)
        {
            Voter = Voter;
            Candidate = candidateModel;
        }

    }

    class VoteCounts
    {
        public int VoterId { get; set; }
        public int CandidateId { get; set; }
    }
}
