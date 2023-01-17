namespace DO;

public struct User
{
    public bool isManager { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public override string ToString() => $@"
isManager    =   {isManager},
Name    =   {Name},
Address      =   {Address},
Email       =   {Email},
Password    =   {Password}
";

}
