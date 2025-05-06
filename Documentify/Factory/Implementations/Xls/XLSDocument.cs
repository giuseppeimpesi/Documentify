using Documentify.Attributes;
using Documentify.Factory.Interfaces;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection;

namespace Documentify.Factory.Implementations.Xls
{
    public class XLSDocument<T> : IDocument<T>
    {
        // Author: Giuseppe Impesi - May 06, 2025
        public FileStream? Create(string path, IList<T> items)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path), "Path cannot be null or empty");

            string extension = Path.GetExtension(path).ToLowerInvariant();

            if (extension != ".xlsx" && extension != ".xls")
                throw new ArgumentException("Invalid file extension. Only .xlsx and .xls are supported");

            FileStream? stream = null;

            int columnIndex = 0;

            try
            {
                using (stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();

                    ISheet sheet = workbook.CreateSheet();

                    // - Builing header 

                    IRow header = sheet.CreateRow(0);

                    typeof(T).GetProperties().ToList().ForEach((PropertyInfo property) =>
                    {
                        string name = property.GetCustomAttribute<HeaderAttribute>()?.Header ?? property.Name;

                        if (property.GetCustomAttribute<ExcludeAttribute>()?.Exclude ?? false)
                            return;

                        header.CreateCell(columnIndex).SetCellValue(name);

                        columnIndex++;
                    });

                    columnIndex = 0;

                    int rowIndex = 1;

                    foreach (T item in items)
                    {
                        if (item is null)
                        {
                            continue;
                        }

                        IRow row = sheet.CreateRow(rowIndex++);

                        PropertyInfo[] properties = item.GetType().GetProperties();

                        foreach (PropertyInfo property in properties)
                        {
                            if (property.GetCustomAttribute<ExcludeAttribute>()?.Exclude ?? false)
                                continue;

                            Object? value = property.GetValue(item);

                            if (value != null)
                            {
                                row.CreateCell(column: columnIndex++).SetCellValue(value.ToString());
                            }
                        }

                        columnIndex = 0;
                    }

                    sheet.AutoSizeColumn(0);

                    workbook.Write(stream);
                }
            }
            catch (IOException)
            {
                Console.WriteLine($"The file is open in another process");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                stream?.Close();
            }

            return stream;
        }
    }
}
