#r "System.Configuration"
#r "System.Data"
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
public static void Run(HttpRequestMessage req, TraceWriter log)
{
    SqlConnection conn = null;
    var connStr = "" ;

    log.Info("Trying to connect to a database...");

    if (ConfigurationManager.ConnectionStrings["myConnString"]== null)
    {
        connStr = $"Server=tcp:xxx.database.windows.net,1433;Initial Catalog=xxxx;Persist Security Info=False;User ID=xxx;Password=xxxx;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=5;";
        conn = new SqlConnection(connStr);
        log.Info("Using manually inputed connection string...");
    }
    else
    {
        connStr = ConfigurationManager.ConnectionStrings["myConnString"].ConnectionString;
        log.Info("Using app setting for the connection string...");
    }

    conn = new SqlConnection(connStr);

    try   //TESTING THE CONNNECTION STRING that it is not null
    {
        conn.Open();
        log.Info($"Connection Created to server {conn.DataSource} and database {conn.Database}");
    }
    catch (Exception e)
    {
        log.Info("Connection failed...");
        log.Info(e.ToString());
    } 
}
