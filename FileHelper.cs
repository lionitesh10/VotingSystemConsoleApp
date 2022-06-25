using VotingSystemConsoleApp;
namespace VotingSystemConsoleApp
{
    class FileHelper
    {
        List<UsersModel> users=new List<UsersModel> ();
        public List<UsersModel> GetUsersDataFromFile()
        {
            users.Clear ();
            FileStream fileStream = new FileStream("..//..//..//AppFiles//users.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fileStream);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            string str=reader.ReadLine();
            while(str != null)
            {
                string[] values = str.Split(',');
                int id=int.Parse(values[0]);
                string name=values[1];
                int age=int.Parse(values[2]);
                string address=values[3];
                string uname = values[4];
                string passwd=values[5];
                int role = int.Parse(values[6]);
                int voteLimit=int.Parse(values[7]);
                UsersModel user = new UsersModel(id, name, age, address, uname, passwd, role);
                user.VoteLimit = voteLimit;
                users.Add(user);
                str= reader.ReadLine();
            }
            reader.Close();
            fileStream.Close();
            return users;
        }
        public void DisplayUsers()
        {
            FileHelper fileHelper = new FileHelper();
            List<UsersModel> users = fileHelper.GetUsersDataFromFile();
            foreach(UsersModel user in users)
            {
                Console.WriteLine("Id = {0} , Name = {1} , Age = {2} , Address = {3} , Username = {4} , Role {5} ",user.Id,user.Name,user.Age,user.Address,user.UserName,user.Role);
            }
        }

        public int DisplayCandidates()
        {
            LinqHelper linqHelper= new LinqHelper();
            List<UsersModel> users = linqHelper.GetList(1);
            if (users.Count != 0)
            {
                Console.WriteLine("Candidates List :: ");
                foreach (UsersModel user in users)
                {
                    Console.WriteLine("Id = {0} , Name = {1} , Age = {2} , Address = {3} , Username = {4} ", user.Id, user.Name, user.Age, user.Address, user.UserName);
                }
                return 1;
            }
            else
            {
                Console.WriteLine("No Candidates :: ");
                return 0;
            }
        }

        public int DisplayVoters()
        {
            LinqHelper linqHelper = new LinqHelper();
            List<UsersModel> users = linqHelper.GetList(0);
            if (users.Count != 0)
            {
                Console.WriteLine("Voters List :: ");
                foreach (UsersModel user in users)
                {
                    Console.WriteLine("Id = {0} , Name = {1} , Age = {2} , Address = {3} , Username = {4} ", user.Id, user.Name, user.Age, user.Address, user.UserName);
                }
                return 1;
            }
            else
            {
                Console.WriteLine("No Voters ");
                return 0;
            }
        }

        public void AddCandidate(UsersModel user)
        {
            FileStream fileStream = new FileStream("..//..//..//AppFiles//users.txt", FileMode.Append,FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            string str = user.Id + "," + user.Name + "," + user.Age + "," + user.Address + "," + user.UserName + "," + user.Password + "," + user.Role+","+user.VoteLimit;
            _ = streamWriter.NewLine;
            streamWriter.WriteLine(str);
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
            if (user.Role == 0)
            {
                Console.WriteLine("Voter Added Successfully !!! .. ");
            }
            else
            {
                Console.WriteLine("Candidate Added Successfully !! .. ");
            }
        }
        
        public void UpdateUsers(List<UsersModel> usersModels)
        {

            FileStream fileStream = new FileStream("..//..//..//AppFiles//users.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            foreach (UsersModel user in usersModels)
            {
                string str = user.Id + "," + user.Name + "," + user.Age + "," + user.Address + "," + user.UserName + "," + user.Password + "," + user.Role+","+user.VoteLimit;
                _ = streamWriter.NewLine;
                streamWriter.WriteLine(str);
            }
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
            Console.WriteLine("Users File successfully updated");
        }

        public int DisplayCandidatesForVote()
        {
            LinqHelper linqHelper = new LinqHelper();
            List<UsersModel> users = linqHelper.GetList(1);
            if (users.Count != 0)
            {
                Console.WriteLine("Candidates List :: ");
                foreach (UsersModel user in users)
                {
                    Console.WriteLine("Id = {0} , Name = {1} , Age = {2} , Address = {3}", user.Id, user.Name, user.Age, user.Address, user.UserName);
                }
                return 1;
            }
            else
            {
                Console.WriteLine("No Candidates :: ");
                return 0;
            }
        }

        public void CandidateVotesInsertion(UsersModel voter,UsersModel candidate)
        {
            FileStream fileStream = new FileStream("..//..//..//AppFiles//candidatesVotes.txt", FileMode.Append, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(voter.Id+","+candidate.Id);
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
            Console.WriteLine("Vote Successfully Updated");
        }

        public void CandidateVotesLoad()
        {

        }
    }
}
