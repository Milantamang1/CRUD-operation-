using CMS.Models;
using System.Data.SqlClient;

namespace CMS
{
    public class ADonet
    {
        const string connectionString = "Server=Milan-Desktop\\SQLEXPRESS;Database=Employee;user=sa;password=abc123##;connect timeout=500;";
        public List<EmployeeModel> GetList()
        {
            var list = new List<EmployeeModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Employee", conn);
                    var record = sqlCommand.ExecuteReader();

                    while (record.Read())
                    {
                        var employee = new EmployeeModel();
                        employee.id = (int)record["Id"];
                        employee.Name = (string)record["Name"];
                        employee.Age = (int)record["Age"];
                        list.Add(employee);
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, such as logging or displaying an error message.
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return list;
        }

        public void Insert(string Name, int age)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("insert into Employee(Name,Age) Values(@Name,@Age)", conn);
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                sqlCommand.Parameters.AddWithValue("@Age", age);

                sqlCommand.ExecuteNonQuery();
            }
        }
        public void Edit(int id, string name, int age)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("update Employee set Name=@name,Age=@Age where id=@id", conn);
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@Name", name);
                sqlCommand.Parameters.AddWithValue("@Age", age);

                sqlCommand.ExecuteNonQuery();

            }
        }
        public EmployeeModel GetById(int id)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("select *from Employee where id=@id", conn);
                sqlCommand.Parameters.AddWithValue("@id", id);

                var record = sqlCommand.ExecuteReader();
                EmployeeModel blogOb = new EmployeeModel();
                while (record.Read())
                {
                    blogOb.id = (int)record["Id"];
                    blogOb.Name = (string)record["Name"];
                    blogOb.Age = (int)record["age"];
                    // list.Add(blogOb);
                }
                return blogOb;
            }
        }
        public void Delete(int id)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("delete from Employee where id=@id", conn);
                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlCommand.ExecuteNonQuery();

            }
        }

    }
}
