using System;
using System.Collections.Generic;
using System.Data;
using Infrastructure.Data.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data.Repositories
{
    public class DonationSqlServerRepository : IDonationRepository
    {
        private const string ConnectionString =
            //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Donation3Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Cliente\source\repos\ProjectDonation3\Infrastructure.Data\AppData\Donation3Database.mdf;Integrated Security=True";
        

        public IEnumerable<DonationModel> GetAll(
            bool orderAscendant,
            string search = null)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            var commandText = "SELECT * FROM Donation";

            if (!string.IsNullOrWhiteSpace(search))
            {
                commandText += " WHERE Name LIKE @search OR Description LIKE @search";

                command.Parameters
                    .Add("@search", SqlDbType.NVarChar)
                    .Value = $"%{search}%";
            }

            var order = orderAscendant ? "ASC" : "DESC";
            commandText += $" ORDER BY Name {order}";

            command.CommandType = CommandType.Text;
            command.CommandText = commandText;

            var reader = command.ExecuteReader();

            var donations = new List<DonationModel>();
            while (reader.Read())
            {
                var donation = new DonationModel
                {
                    Id = reader.GetFieldValue<int>("Id"),
                    Name = reader.GetFieldValue<string>("Name"),
                    Description = reader.GetFieldValue<string>("Description"),
                    Courier = reader.GetFieldValue<double>("Courier"),
                    Quantity = reader.GetFieldValue<int>("Quantity"),
                    DateOfRegister = reader.GetFieldValue<DateTime>("DateOfRegister"),
                    Status = reader.GetFieldValue<bool>("Status")
                };

                donations.Add(donation);
            }
            return donations;
        }

       
        DonationModel IDonationRepository.GetById(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Id, Name, Description, Courier, Quantity, DateOfRegister, Status FROM Donation WHERE Id = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            var reader = command.ExecuteReader();

            var canRead = reader.Read();
            if (!canRead)
            {
                return null;
            }

            var donation = new DonationModel
            {
                Id = reader.GetFieldValue<int>("Id"),
                Name = reader.GetFieldValue<string>("Name"),
                Description = reader.GetFieldValue<string>("Description"),
                Courier = reader.GetFieldValue<double>("Courier"),
                Quantity = reader.GetFieldValue<int>("Quantity"),
                DateOfRegister = reader.GetFieldValue<DateTime>("DateOfRegister"),
                Status = reader.GetFieldValue<bool>("Status")
            };

            return donation;
        }

        DonationModel IDonationRepository.Create(DonationModel donationModel)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = @"INSERT INTO Donation
	(Name, Description, Courier, Quantity, DateOfRegister, Status)
	OUTPUT INSERTED.Id
	VALUES (@name, @description, @courier, @quantity, @dateOfRegister, @status);";

            command.Parameters
                .Add("@name", SqlDbType.NVarChar)
                .Value = donationModel.Name;
            command.Parameters
                .Add("@description", SqlDbType.NVarChar)
                .Value = donationModel.Description;
            command.Parameters
                .Add("@courier", SqlDbType.Float)
                .Value = donationModel.Courier;
            command.Parameters
                .Add("@quantity", SqlDbType.Int)
                .Value = donationModel.Quantity;
            command.Parameters
                .Add("@dateOfRegister", SqlDbType.DateTime2)
                .Value = donationModel.DateOfRegister;
            command.Parameters
                .Add("@status", SqlDbType.Bit)
                .Value = donationModel.Status;

            var scalarResult = command.ExecuteScalar();

            donationModel.Id = (int)scalarResult;

            return donationModel;
        }

        DonationModel IDonationRepository.Edit(DonationModel donationModel)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = @"UPDATE Donation
	SET Name = @name, Description = @description, Courier = @courier, 
	Quantity = @quantity, DateOfRegister = @dateOfRegister, Status = @status
	WHERE Id = @id;";

            command.Parameters
                .Add("@name", SqlDbType.NVarChar)
                .Value = donationModel.Name;
            command.Parameters
                .Add("@description", SqlDbType.NVarChar)
                .Value = donationModel.Description;
            command.Parameters
                .Add("@courier", SqlDbType.Float)
                .Value = donationModel.Courier;
            command.Parameters
                .Add("@quantity", SqlDbType.Int)
                .Value = donationModel.Quantity;
            command.Parameters
                .Add("@dateOfRegister", SqlDbType.DateTime2)
                .Value = donationModel.DateOfRegister;
            command.Parameters
                .Add("@status", SqlDbType.Bit)
                .Value = donationModel.Status;
            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = donationModel.Id;


            command.ExecuteScalar();

            return donationModel;
        }


        void IDonationRepository.Delete(int id)
        {
            using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Donation WHERE Id = @id;";

            command.Parameters
                .Add("@id", SqlDbType.Int)
                .Value = id;

            command.ExecuteScalar();
        }
    }
}
