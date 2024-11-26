using Microsoft.EntityFrameworkCore;

namespace FinanceManagementSystem.Models
{
    public class FinanceDBContext:DbContext
    {
        public FinanceDBContext() { }

        public FinanceDBContext(DbContextOptions<FinanceDBContext> options) : base(options) { }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=FinanceDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    }
}
