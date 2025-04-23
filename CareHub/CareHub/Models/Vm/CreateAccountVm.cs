namespace CareHub.Models.Vm
{
    public class CreateAccountVm
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string UserRole { get; set; } = "";
        public string UserType { get; set; } = "";
        public string Name { get; set; } = "";

    }
}
