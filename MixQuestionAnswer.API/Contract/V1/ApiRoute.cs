
namespace MixQuestionAnswer.API.Contract.V1
{
    public class ApiRoute
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/users{Id}";
            public const string Register = Base + "/users/register";
            public const string Update = Base + "/users/edit{Id}";
            public const string Delete = Base + "/users/delete{Id}";
            public const string AddRole = Base + "/users/addRole{userId}";
            public const string ChangePassword = Base + "/users/changepassword{userId}";
            public const string Authenticate = Base + "/users/authenticate";

        }
    }
}
