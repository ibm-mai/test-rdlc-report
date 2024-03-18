using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using Npgsql;

namespace WebApplicationRDLC2
{
    public class ServiceReport
    {
        string connectionString = "Server=localhost;Port=5432;Database=test;UID=postgres;PWD=admin";
        public byte[] CreateReportFile(string pathRdlc) {

            var conn = new NpgsqlConnection(connectionString);
            string query = "SELECT * FROM test.user_info order by id ASC;";
            System.Console.WriteLine("Query Success");
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            NpgsqlDataAdapter pda = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            pda.Fill(dt);

            LocalReport report = new LocalReport();
            report.ReportPath = pathRdlc;
            ReportDataSource reportDataSource = new ReportDataSource("UserInfo", dt);
            report.DataSources.Add(reportDataSource);

            string extension;
            string encoding;
            string mimeType;
            string[] streamIds;
            Warning[] warnings;

            byte[] mybytes = report.Render("PDF", null,
            out mimeType,
            out encoding,
            out extension,
            out streamIds,
            out warnings);

            System.Console.WriteLine("Create Report Success");
            return mybytes;
        }
    }
}