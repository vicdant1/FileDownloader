using Domain;
using ExcelDataReader;
using System.Data;

namespace CSharpFileDownloader
{
    public static class Excel
    {

        public static List<Question> ExcelFileReader()
        {

            List<Question> questionList = new List<Question>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open("C:\\_projects\\FileDownloader\\assets\\questions.xlsx", FileMode.Open, FileAccess.Read))
            using(var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet();
                var table = result.Tables.Cast<DataTable>().FirstOrDefault();

                int rowCounter = 0;
                foreach (DataRow row in table.Rows)
                {
                    if(rowCounter == 0)
                    {
                        rowCounter++;
                        continue;
                    }

                    string topic = row.ItemArray[0].ToString();
                    int index = Convert.ToInt32(row.ItemArray[1]);
                    string link = row.ItemArray[2].ToString().Replace("&print=true", "");

                    questionList.Add(new Question(topic, index, link));
                }

            }

            return questionList;
        }
    }
}
