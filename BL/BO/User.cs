namespace BO;

public class User
{
    public bool isManager { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}