using CSharpFileDownloader;
using Domain;
using PuppeteerSharp;


Console.WriteLine("Iniciando bot...");

string _outputDir = "C:\\_projects\\FileDownloader\\assets\\results";

Console.WriteLine("Lendo arquivo excel");

List<Question> questionList = Excel.ExcelFileReader();
Console.WriteLine($"Arquivo lido com sucesso, {questionList.Count} registros encontrados");

Console.WriteLine("Iniciando download do WebAgent");

using (var browserFetcher = new BrowserFetcher())
{
    await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
    var browser = await Puppeteer.LaunchAsync(new LaunchOptions
    {
        Headless = true,
        Args = new[] { "--kiosk-printing" }
    });

    using (var page = await browser.NewPageAsync())
    {
        foreach (var question in questionList)
        {
            Console.WriteLine($"Realizando download da questão {question.Index} de {question.Topic}");
            await page.GoToAsync(question.Link);
            await page.WaitForSelectorAsync(".GMJ0AO2BBIG.GMJ0AO2BOHH.proof.GMJ0AO2BKFG.GMJ0AO2BL-G.GMJ0AO2BE4D");
            Console.WriteLine("Gerando PDF da questão...");

            var targetDir = $"{_outputDir}\\{question.Topic}";
            Directory.CreateDirectory(targetDir);

            await page.PdfAsync($"{targetDir}\\{question.Index}.pdf");
        }
    }
}

