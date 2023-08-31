namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (MODECOMMON.MODECOMMONEntities entities = new())
            {
                try
                {
                    List<MODECOMMON.CommonCode> codes = entities.CommonCode.Where(x => x.CodeName.IndexOf("투어마일리지") > -1).OrderBy(x => x.CodeName).ToList().DistinctBy(x => x.CodeName).ToList();
                    // MODECOMMON.CommonCode code1 = entities.CommonCode.Single(author => author.CodeName.StartsWith("test"));

                    foreach (MODECOMMON.CommonCode code in codes)
                    {
                        Console.WriteLine(code.CodeName);
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }
    }
}