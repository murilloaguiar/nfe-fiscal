using NfeFiscal.Models.Enums;
using NfeFiscal.Responses;
using System.Text;
using System.Text.Json;

namespace NfeFiscal.Helpers;

public class FileHelper
{

    public static string ExportInvoiceInJson(InvoiceResponse invoice)
    {
        var basePath = $@"{Environment.CurrentDirectory}\exports\json\";
        if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

        var filePath = $@"{basePath}invoice-{invoice.Id}.json";



        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (var writer = new StreamWriter(fileStream))
        {
            var dataJson = JsonSerializer.Serialize(invoice, new JsonSerializerOptions { WriteIndented = true });


            writer.Write(dataJson);
                
        }
        return filePath;
    }

    public static string ExportInvoiceInTxt(InvoiceResponse invoice)
    {
        var basePath = $@"{Environment.CurrentDirectory}\exports\txt\";
        if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

        var filePath = $@"{basePath}invoice-{invoice.Id}.txt";



        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (var writer = new StreamWriter(fileStream, Encoding.UTF8))
        {


            writer.WriteLine(invoice.ToString());

            if (invoice.Items.Count() > 0)
            {
                foreach (var item in invoice.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }


        }

        return filePath;
    }

    public static void DeleteFile(string filePath)
	{
		try{

			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}
		}catch(Exception ex){
			throw new Exception(ex.Message);
		}
	}

	public static string GetContentType(ExportFormat format)
	{
        if (format.Equals(ExportFormat.json))
            return "application/json";

		return "plain/text";
		
	}
}
