using VotingSystemConsoleApp;
namespace VotingSystemConsoleApp
{
    class LinqHelper
    {
        public UsersModel LoginCheck(string username,string password)
        {
            FileHelper fileHelper = new FileHelper();
            List<UsersModel> users = fileHelper.GetUsersDataFromFile();
            UsersModel users1 = null;
            var users2=from user in users where user.UserName == username && user.Password == password select user;
            //UsersModel user=users.Where(u=>u.UserName == username && u.Password==password);
            
            foreach(var user in users2)
            {
                users1 = (UsersModel)user;
            }

            if (users1==null)
            {
                return null;
            }
            else
            {
                return users1;
            }
        }
        public List<UsersModel> GetList(int role)
        {
            List<UsersModel> users = new List<UsersModel>();
            FileHelper fileHelper = new FileHelper();
            List<UsersModel> candidates = fileHelper.GetUsersDataFromFile();
            var candidatesQuery = from candidate in candidates where candidate.Role == role select candidate;
            foreach(UsersModel candidate in candidatesQuery)
            {
                users.Add(candidate);
            }
            
            return users;
        }

        public List<UsersModel> GetAfterLost(int userId)
        {
            FileHelper fileHelper = new FileHelper();
            List<UsersModel> users = fileHelper.GetUsersDataFromFile();
            foreach (UsersModel user in users)
            {
                if (user.Id == userId)
                {
                    user.VoteLimit = 0;
                }
            }
            return users;
        }

        public void DisplayCandidateVotes(List<VoteCounts> voteCounts)
        {
            FileHelper fileHelper=new FileHelper();
            List<UsersModel> users = fileHelper.GetUsersDataFromFile();

            var NamesVotes = from vote in voteCounts
                             join user in users
                             on vote.CandidateId equals user.Id
                             group vote by vote.CandidateId into namevotes
                             select new
                             {
                                 votes = namevotes.Count(),
                                 NameId = namevotes.Key
                             };
            foreach(var namevotes in NamesVotes)
            {
                UsersModel user1 = null;
                foreach(var user in users)
                {
                    if(user.Id==namevotes.NameId)
                        user1 = user;
                }
                Console.WriteLine("Name :: {0} and Votes :: {1} ",user1.Name,namevotes.votes);
            }
        }

    }
}
