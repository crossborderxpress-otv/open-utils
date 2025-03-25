using entities.models;
using System.Text;
using System.Text.Json;

var g_projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

try
{
    var verticalSeparator = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "verticalSeparator.png"));
    var horizontalSeparator = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "horizontalSeparator.png"));
    var horizontalFooterSeparator = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "horizontalFooterSeparator.png"));

    var pdfBytes = await GeneratePdfFromApiAsync(verticalSeparator, horizontalSeparator, horizontalFooterSeparator);

    if (pdfBytes != null && pdfBytes.Length > 0)
    {
        File.WriteAllBytes("C:\\Users\\achavez\\Desktop\\mypdf_2.pdf", pdfBytes);
        Console.WriteLine("Document saved successfully.");
    }
    else
    {
        Console.WriteLine("No valid document received.");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

async Task<byte[]> GeneratePdfFromApiAsync(byte[] verticalSeparator, byte[] horizontalSeparator, byte[] horizontalFooterSeparator)
{
    using (var client = new HttpClient())
    {
        var requestData = new
        {
            documents = generateDocuments(),
            sections = generateSections(),
            contents = generateContents(),
            horizontalFooterSeparator,
            verticalSeparator,
            horizontalSeparator
        };

        var jsonContent = JsonSerializer.Serialize(requestData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7094/document/generatePdf", content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }
        else
        {
            Console.WriteLine($"Error in POST request: {response.StatusCode}");
            return null;
        }
    }
}

List<documentModel> generateDocuments()
{
    return new List<documentModel>()
    {
       new documentModel()
       {
           mainImage = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "mainImage.png")),
           subtitle = "LoremIpsumDolor",
           title = "LoremIpsumDolor"
        }
    };
}

List<sectionModel> generateSections()
{
    return new List<sectionModel>()
    {
        new sectionModel()
        {
            id = 1,
            order = 1,
            image = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "header.png"))
        },
        new sectionModel()
        {
            id = 5,
            order = 5,
            image = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "bodyRightTop.png"))
        },
        new sectionModel()
        {
            id = 6,
            order = 6,
            image = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "bodyRightBottom.png"))
        },
        new sectionModel()
        {
            id = 7,
            order = 7,
            image = File.ReadAllBytes(Path.Combine(g_projectDirectory, "images", "footer.png"))
        }
    };
}

List<contentModel> generateContents()
{
    return new List<contentModel>()
    {
        new contentModel()
        {
            id = 1,
            sectionId = 2,
            content = string.Empty,
            order = 1,
        },
        new contentModel()
        {
            id = 2,
            sectionId = 3,
            content = string.Empty,
            order = 1,
        },
        new contentModel()
        {
            id = 3,
            sectionId = 3,
            content = "LoremIpsumDolor",
            order = 2,
        },
        new contentModel()
        {
            id = 4,
            sectionId = 3,
            content = string.Empty,
            order = 3,
        },
        new contentModel()
        {
            id = 7,
            sectionId = 4,
            content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin vel feugiat ligula. Vestibulum ut augue ipsum. Phasellus nec libero nec ipsum suscipit feugiat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aliquam mattis mollis justo vitae ornare. Sed vel mauris rhoncus, tristique neque sit amet, porttitor sapien. Donec at posuere nunc. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fermentum, elit eget luctus commodo, elit mauris porttitor orci, quis blandit purus mauris vitae arcu.",
            order = 3,
        }
    };
}