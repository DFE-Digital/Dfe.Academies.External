namespace Dfe.Academies.External.Web.Logger
{
    public class LoggerClass : ILoggerClass
    {
        static private Object fileLock = new object();

        public void Logger( string ex)
        {
            try
            {


                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\LogDfeAcademiesExternalWeb");

                string folder = AppDomain.CurrentDomain.BaseDirectory + @"\LogDfeAcademiesExternalWeb" + @"\";

                string fileName = "Error" + DateTime.Now.ToString("dd-MM-yyyy:HHmmssffff").Replace(@":", "-") + ".txt";

                string fullPath = folder + fileName;

                string[] logs = { ex };

                lock(fileLock)
                {
                    File.AppendAllLines(fullPath, logs);
                }

            }
            catch (Exception)
            {
                throw ;
            }

        }

    }
}
