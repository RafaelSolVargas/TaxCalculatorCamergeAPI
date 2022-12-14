namespace TaxCalculator.Domain.Entities;

public class User {
    public int? id { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public Role role { get; set; }

    public User(int id, string email, string password, Role role) {
        this.id = id;
        this.email = email;
        this.password = password;
        this.role = role;
    }
}
