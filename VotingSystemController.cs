namespace VotingSystemConsoleApp
{
    public class VotingSystemController
    {
        UsersModel loggedInUser;
        public void WelcomeController()
        {
            string WelcomeMessage = "Welcome to Console Voting System".ToUpper();
            Console.WriteLine(".........." + WelcomeMessage + " ..........");
            LoginController();
        }
        public void LoginController()
        {
            Console.WriteLine("Enter Username :: ");
            string Uname = Console.ReadLine();
            Console.WriteLine("Enter Password :: ");
            string Passwd = Console.ReadLine();
            LinqHelper linqHelper = new LinqHelper();
            UsersModel User = linqHelper.LoginCheck(Uname, Passwd);
            if (User == null)
            {
                Console.WriteLine("Invalid Credentials..!! Try again ..");
                LoginController();
            }
            else
            {
                loggedInUser = User;
                Console.WriteLine("Login Successfull..!!");
                if (User.Role == 2)
                {
                    AdminHomeController();
                }
                else
                {
                    NormalHomeController();
                }

            }
        }
        public void AdminMenus()
        {
            int choice;
            Console.WriteLine("1. To Add a Candidate :: ");
            Console.WriteLine("2. To Add a Voter :: ");
            Console.WriteLine("3. To Update a Candidate :: ");
            Console.WriteLine("4. To Update a Voter :: ");
            Console.WriteLine("5. To Delete a Candidate :: ");
            Console.WriteLine("6. To Delete a Voter :: ");
            Console.WriteLine("7. Display Votes :: ");
            Console.WriteLine("8. Logout");
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                choice = 0;
            }

            switch (choice)
            {
                case 1:
                    AddCandidate(1);
                    break;
                case 2:
                    AddCandidate(0);
                    break;
                case 3:
                    FileHelper Filehelper = new FileHelper();
                    int status = Filehelper.DisplayCandidates();
                    if (status == 1)
                    {
                        UpdateUser(1);
                        Filehelper.DisplayCandidates();
                    }
                    break;
                case 4:
                    FileHelper FileHelper1 = new FileHelper();
                    int status1 = FileHelper1.DisplayVoters();
                    if (status1 == 1)
                    {
                        UpdateUser(0);
                        FileHelper1.DisplayVoters();
                    }
                    break;
                case 5:
                    FileHelper FileHelper2 = new FileHelper();
                    int status3 = FileHelper2.DisplayCandidates();
                    if (status3 == 1)
                    {
                        DeleteUser(1);
                        FileHelper2.DisplayCandidates();
                    }
                    break;
                case 6:
                    FileHelper FileHelper3 = new FileHelper();
                    int status4 = FileHelper3.DisplayVoters();
                    if (status4 == 1)
                    {
                        DeleteUser(0);
                        FileHelper3.DisplayVoters();
                    }
                    break;
                case 7:
                    Console.WriteLine("Vote Display");
                    DisplayVotes();
                    break;
                case 8:
                    Console.WriteLine("Logout");
                    break;
                default:
                    Console.WriteLine("Invalid.. Choice !! ");
                    break;
            }
        }
        public void AdminHomeController()
        {
            Console.WriteLine("Hi, Admin ..!");
            AdminMenus();
        }

        public void AddCandidate(int role)
        {
            int Cid, Age;
            Console.WriteLine("Enter ID :: ");
            try
            {
                Cid = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Name :: ");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter age :: ");
                Age = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Address :: ");
                string Address = Console.ReadLine();
                Console.WriteLine("Enter Username :: ");
                string Username = Console.ReadLine();
                Console.WriteLine("Enter Password :: ");
                string Password = Console.ReadLine();
                UsersModel user1 = new UsersModel(Cid, Name, Age, Address, Username, Password, role);
                FileHelper fileHelper = new FileHelper();
                fileHelper.AddCandidate(user1);
                AdminMenus();
            }
            catch (Exception)
            {

            }
        }
        public void UpdateUser(int role)
        {
            int uid;
            Console.WriteLine("Enter Id to Update :: ");
            try
            {
                uid = int.Parse(Console.ReadLine());
                FileHelper helper = new FileHelper();
                List<UsersModel> usersModels = helper.GetUsersDataFromFile();
                int SearchStatus = 0;
                foreach (UsersModel user in usersModels)
                {
                    if (user.Id == uid)
                    {
                        Console.WriteLine("Enter Updated Name :: ");
                        user.Name = Console.ReadLine();
                        Console.WriteLine("Enter Updated Age :: ");
                        user.Age = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Updated Address :: ");
                        user.Address = Console.ReadLine();
                        Console.WriteLine("Enter Updated Username :: ");
                        user.UserName = Console.ReadLine();
                        Console.WriteLine("Enter Updated Password :: ");
                        user.Password = Console.ReadLine();
                        SearchStatus = 1;
                    }
                    if (SearchStatus == 1)
                        break;
                }
                FileHelper helper1 = new FileHelper();
                helper1.UpdateUsers(usersModels);
                if (role == 1)
                {
                    Console.WriteLine("Candidate Updated Successfully ..!");
                }
                else if (role == 0)
                {
                    Console.WriteLine("Voter Updated Successfully ..!");
                }
            }
            catch (Exception)
            {

            }
        }

        public void DeleteUser(int role)
        {
            int deleteId;
            try
            {
                deleteId = int.Parse(Console.ReadLine());
                FileHelper fileHelper = new FileHelper();
                List<UsersModel> users = fileHelper.GetUsersDataFromFile();
                foreach (UsersModel user in users)
                {
                    if (user.Id == deleteId)
                    {
                        users.Remove(user);
                        break;
                    }
                }
                fileHelper.UpdateUsers(users);
                if (role == 0)
                {
                    Console.WriteLine("Voter Deleted Successfully .. !");
                }
                else
                {
                    Console.WriteLine("Candidate Deleted Successfully .. !");
                }
            }
            catch (Exception)
            {

            }
        }
        public void NormalHomeController()
        {
            if (loggedInUser.VoteLimit == 0)
            {
                Console.WriteLine("You have already voted.. !!! ");

            }
            else
            {
                Console.WriteLine("Candidates to Vote :: ");
                FileHelper fileHelper = new FileHelper();
                int status = fileHelper.DisplayCandidatesForVote();
                if (status == 0)
                {
                    Console.WriteLine("No Candidates To Vote as of Now");
                }
                else
                {
                    Console.WriteLine("Enter Id To Vote For :: ");
                    UsersModel candidate = null;
                    int voteId = int.Parse(Console.ReadLine());
                    try
                    {
                        foreach (UsersModel users in fileHelper.GetUsersDataFromFile())
                        {
                            if (users.Id == voteId)
                            {
                                CandidateVotes candidateVotes = new CandidateVotes(loggedInUser, users);
                                candidate = users;
                            }
                        }
                        fileHelper.CandidateVotesInsertion(loggedInUser, candidate);
                        LinqHelper linq1 = new LinqHelper();
                        fileHelper.UpdateUsers(linq1.GetAfterLost(loggedInUser.Id));
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }
        public void DisplayVotes()
        {
            FileHelper fileHelper = new FileHelper();
            List<VoteCounts> voteCounts = fileHelper.CandidateVotesLoad();
            //foreach (VoteCounts vote in voteCounts)
            //{
            //    Console.WriteLine(vote.CandidateId);
            //}
            LinqHelper linqHelper=new LinqHelper();
            linqHelper.DisplayCandidateVotes(voteCounts);
        }

    }
}
